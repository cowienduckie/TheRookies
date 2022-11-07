using System.Linq.Expressions;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Dtos.Category;
using BookLibrary.WebApi.Dtos.User;
using BookLibrary.WebApi.Filters;
using BookLibrary.WebApi.Helpers;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;
using Common.DataType;
using Common.Enums;

namespace BookLibrary.WebApi.Services.Implements;

public class BorrowRequestService : IBorrowRequestService
{
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BorrowRequestService(IUnitOfWork unitOfWork, IBorrowRequestRepository borrowRequestRepository)
    {
        _unitOfWork = unitOfWork;
        _borrowRequestRepository = borrowRequestRepository;
        _bookRepository = _unitOfWork.GetRepository<Book>();
    }

    public async Task<CreateBorrowRequestResponse?> CreateAsync(CreateBorrowRequestRequest requestModel)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var bookIds = requestModel.BookIds.Distinct();

            var books = await _bookRepository
                    .GetAllAsync(book => bookIds.Contains(book.Id))
                as List<Book>;

            if (books == null ||
                books.Count != bookIds.Count())
                return null;

            var newBorrowRequest = new BorrowRequest
            {
                Status = RequestStatus.Waiting,
                Books = books,
                RequestedBy = requestModel.Requester.Id,
                RequestedAt = DateTime.UtcNow
            };

            var createdBorrowRequest = _borrowRequestRepository.Create(newBorrowRequest);

            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return new CreateBorrowRequestResponse(createdBorrowRequest);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            await transaction.RollbackAsync();

            return null;
        }
    }

    public async Task<IPagedList<GetBorrowRequestResponse>> GetAllAsync(
        GetBorrowRequestRequest request, 
        PagingFilter pagingFilter, 
        SortFilter sortFilter)
    {
        Expression<Func<BorrowRequest, bool>>? predicate = null;

        if (request.Requester.Role == Role.NormalUser) predicate = br => br.Requester.Id == request.Requester.Id;

        var borrowRequests = 
            (await _borrowRequestRepository.GetAllAsync(predicate))
            .AsQueryable();

        var validSortFields = new[]
        {
            SortField.Id,
            SortField.RequestedAt,
            SortField.RequestedBy,
            SortField.ApprovedAt,
            SortField.ApprovedBy,
            SortField.RequestStatus
        };

        var sortedBorrowRequests = borrowRequests
            .SortData(validSortFields, sortFilter.SortField, sortFilter.SortOrder)
            .Select(br => new GetBorrowRequestResponse(br))
            .AsQueryable();

        return new PagedList<GetBorrowRequestResponse>
            (sortedBorrowRequests, pagingFilter.PageIndex, pagingFilter.PageSize);
    }

    public async Task<GetBorrowRequestResponse?> GetByIdAsync(GetBorrowRequestRequest request)
    {
        if (request.Id == null) return null;

        Expression<Func<BorrowRequest, bool>>? predicate = borrowRequest => borrowRequest.Id == request.Id;

        if (request.Requester.Role == Role.NormalUser)
            predicate = br => br.Requester.Id == request.Requester.Id &&
                              br.Id == request.Id;

        var borrowRequest = await _borrowRequestRepository.GetAsync(predicate);

        if (borrowRequest == null) return null;

        return new GetBorrowRequestResponse(borrowRequest);
    }

    public bool IsExist(int id)
    {
        return _borrowRequestRepository.IsExist(borrowRequest => borrowRequest.Id == id);
    }

    public async Task<string> CheckRequestLimit(CreateBorrowRequestRequest request)
    {
        if (request.BookIds.Count < Settings.MinBooksPerRequest) return ErrorMessages.BooksPerRequestLimitNotReached;

        if (request.BookIds.Count > Settings.MaxBooksPerRequest) return ErrorMessages.BooksPerRequestLimitExceeded;

        var currentMonth = DateTime.UtcNow.Month;

        var bookRequestsThisMonth = await _borrowRequestRepository
            .GetAllAsync(br =>
                br.RequestedBy == request.Requester.Id &&
                br.RequestedAt.Month == currentMonth);

        if (bookRequestsThisMonth.Count() >= Settings.MaxBorrowRequestsPerMonth)
            return ErrorMessages.RequestsPerMonthLimitExceeded;

        return string.Empty;
    }

    public async Task<ApproveBorrowRequestResponse?> ApproveAsync(ApproveBorrowRequestRequest requestModel)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var borrowRequest =
                await _borrowRequestRepository.GetAsync(borrowRequest => borrowRequest.Id == requestModel.Id);

            if (borrowRequest == null) return null;

            borrowRequest.Status = requestModel.IsApproved
                ? RequestStatus.Approved
                : RequestStatus.Rejected;
            borrowRequest.ApprovedBy = requestModel.Approver.Id;
            borrowRequest.ApprovedAt = DateTime.UtcNow;

            var updatedBorrowRequest = _borrowRequestRepository.Update(borrowRequest);

            await _unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();

            return new ApproveBorrowRequestResponse(updatedBorrowRequest);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);

            await transaction.RollbackAsync();

            return null;
        }
    }
}