using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Services.Interfaces;
using Common.Constants;
using Common.Enums;

namespace BookLibrary.WebApi.Services.Implements;

public class BorrowRequestService : IBorrowRequestService
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IBaseRepository<Book> _bookRepository;
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
            {
                return null;
            }

            var newBorrowRequest = new BorrowRequest
            {
                Status = RequestStatuses.Waiting,
                Books = books,
                RequestedBy = 1, // TODO: Get current user ID
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

    public async Task<IEnumerable<GetBorrowRequestResponse>> GetAllAsync()
    {
        return (await _borrowRequestRepository.GetAllAsync())
            .Select(borrowRequest => new GetBorrowRequestResponse(borrowRequest));
    }

    public async Task<GetBorrowRequestResponse?> GetByIdAsync(int id)
    {
        var borrowRequest = await _borrowRequestRepository.GetAsync(borrowRequest => borrowRequest.Id == id);

        if (borrowRequest == null) return null;

        return new GetBorrowRequestResponse(borrowRequest);
    }

    public bool IsExist(int id)
    {
        return _borrowRequestRepository.IsExist(borrowRequest => borrowRequest.Id == id);
    }

    public async Task<string> CheckRequestLimit(int userId, CreateBorrowRequestRequest request)
    {
        if (request.BookIds.Count > SystemConstants.MaxBooksPerRequest)
        {
            return ErrorMessages.BooksPerRequestLimitExceeded;
        }

        var currentMonth = DateTime.UtcNow.Month;

        var bookRequestsThisMonth = await _borrowRequestRepository
            .GetAllAsync(br =>
                br.RequestedBy == userId &&
                br.RequestedAt.Month == currentMonth);

        if (bookRequestsThisMonth.Count() >= SystemConstants.MaxBorrowRequestsPerMonth)
        {
            return ErrorMessages.RequestsPerMonthLimitExceeded;
        }

        return string.Empty;
    }

    public async Task<ApproveBorrowRequestResponse?> ApproveAsync(ApproveBorrowRequestRequest requestModel)
    {
        using var transaction = _unitOfWork.GetDatabaseTransaction();

        try
        {
            var borrowRequest = await _borrowRequestRepository.GetAsync(borrowRequest => borrowRequest.Id == requestModel.Id);

            if (borrowRequest == null)
            {
                return null;
            }

            borrowRequest.Status = requestModel.IsApproved
                ? RequestStatuses.Approved
                : RequestStatuses.Rejected;
            borrowRequest.ApprovedBy = 4;  // TODO: Get current super user approved this
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