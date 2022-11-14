using System.Linq.Expressions;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.Book;
using BookLibrary.WebApi.Dtos.BorrowRequest;
using BookLibrary.WebApi.Dtos.User;
using BookLibrary.WebApi.Services.Implements;
using Common.Constants;
using Common.Enums;
using Common.Wrappers;
using Moq;
using NUnit.Framework;

namespace BookLibrary.UnitTests.ServiceTests;

public class BorrowRequestServiceTests
{
    private const int _borrowRequestId = 1;
    private const RequestStatus _borrowRequestStatus = RequestStatus.Waiting;
    private const int _normalUserId = 1;
    private const int _superUserId = 2;
    private const string _userUsername = "Username";
    private const string _userPassword = "Password";
    private const string _userName = "Name";
    private const string _bookName = "Book";
    private const string _dateTimeFormat = "HH:mm:ss dd/MM/yyyy";
    private static readonly DateTime _fakeDateTime = DateTime.UtcNow;

    private static readonly BorrowRequest _fakeBorrowRequest = new()
    {
        Id = _borrowRequestId,
        Status = _borrowRequestStatus,
        RequestedAt = _fakeDateTime,
        RequestedBy = _normalUserId,
        Requester = new User
        {
            Id = _normalUserId,
            Username = _userUsername,
            Password = _userPassword,
            Name = _userName,
            Role = Role.NormalUser
        },
        ApprovedAt = _fakeDateTime,
        ApprovedBy = _superUserId,
        Approver = new User
        {
            Id = _superUserId,
            Username = _userUsername,
            Password = _userPassword,
            Name = _userName,
            Role = Role.SuperUser
        },
        Books = new List<Book>
        {
            new() {Id = 1, Name = _bookName},
            new() {Id = 2, Name = _bookName}
        }
    };

    private Mock<IBookRepository> _bookRepository;
    private Mock<IBorrowRequestRepository> _borrowRequestRepository;
    private BorrowRequestService _borrowRequestService;
    private Mock<IUnitOfWork> _unitOfWork;

    [SetUp]
    public void Setup()
    {
        _borrowRequestRepository = new Mock<IBorrowRequestRepository>();
        _bookRepository = new Mock<IBookRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _borrowRequestService = new BorrowRequestService(
            _unitOfWork.Object,
            _borrowRequestRepository.Object,
            _bookRepository.Object);
    }

    [Test]
    public async Task GetByIdAsync_NullId_ReturnsNull()
    {
        var input = new GetBorrowRequestRequest
        {
            Id = null
        };

        var result = await _borrowRequestService.GetByIdAsync(input);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetByIdAsync_InvalidInput_ReturnsNull()
    {
        var input = new GetBorrowRequestRequest
        {
            Id = _borrowRequestId,
            Requester = new UserModel
            {
                Id = _normalUserId,
                Role = Role.NormalUser
            }
        };

        _borrowRequestRepository
            .Setup(brr => brr.GetSingleAsync(It.IsAny<Expression<Func<BorrowRequest, bool>>>()))
            .ReturnsAsync(null as BorrowRequest);

        var result = await _borrowRequestService.GetByIdAsync(input);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetByIdAsync_ValidInput_ReturnsGetBorrowRequestResponse()
    {
        var input = new GetBorrowRequestRequest
        {
            Id = _borrowRequestId,
            Requester = new UserModel
            {
                Id = _normalUserId,
                Role = Role.NormalUser
            }
        };

        _borrowRequestRepository
            .Setup(brr => brr.GetSingleAsync(It.IsAny<Expression<Func<BorrowRequest, bool>>>()))
            .ReturnsAsync(_fakeBorrowRequest);

        var result = await _borrowRequestService.GetByIdAsync(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<GetBorrowRequestResponse>());

            Assert.That(result?.Id, Is.EqualTo(_borrowRequestId));

            Assert.That(result?.Status, Is.EqualTo(_borrowRequestStatus.ToString()));

            Assert.That(result?.RequestedAt, Is.EqualTo(_fakeDateTime.ToLocalTime().ToString(_dateTimeFormat)));

            Assert.That(result?.ApprovedAt, Is.EqualTo(_fakeDateTime.ToLocalTime().ToString(_dateTimeFormat)));

            Assert.That(result?.Requester.Id, Is.EqualTo(_normalUserId));

            Assert.That(result?.Requester.Username, Is.EqualTo(_userUsername));

            Assert.That(result?.Requester.Name, Is.EqualTo(_userName));

            Assert.That(result?.Requester.Role, Is.EqualTo(Role.NormalUser.ToString()));

            Assert.That(result?.Approver, Is.Not.Null);

            Assert.That(result?.Approver?.Id, Is.EqualTo(_superUserId));

            Assert.That(result?.Approver?.Username, Is.EqualTo(_userUsername));

            Assert.That(result?.Approver?.Name, Is.EqualTo(_userName));

            Assert.That(result?.Approver?.Role, Is.EqualTo(Role.SuperUser.ToString()));

            Assert.That(result?.Books, Is.InstanceOf<List<BookModel>>());

            Assert.That(result?.Books, Has.Count.EqualTo(_fakeBorrowRequest.Books.Count));
        });
    }

    [Test]
    public async Task CreateAsync_NullRequester_ReturnsNull()
    {
        var input = new CreateBorrowRequestRequest
        {
            Requester = null
        };

        var result = await _borrowRequestService.CreateAsync(input);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task CreateAsync_InvalidBookIds_ReturnsNull()
    {
        var input = new CreateBorrowRequestRequest
        {
            Requester = new UserModel
            {
                Id = _normalUserId,
                Role = Role.NormalUser
            },
            BookIds = new List<int> {1, 2}
        };

        var bookEntities = new List<Book>
        {
            new() {Id = 1, Name = _bookName}
        };

        _bookRepository
            .Setup(br => br.GetAllAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(bookEntities);

        var result = await _borrowRequestService.CreateAsync(input);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task CreateAsync_ValidInput_ReturnsCreateBorrowRequestResponse()
    {
        var input = new CreateBorrowRequestRequest
        {
            Requester = new UserModel
            {
                Id = _normalUserId,
                Role = Role.NormalUser
            },
            BookIds = new List<int> {1, 2}
        };

        var bookEntities = new List<Book>
        {
            new() {Id = 1, Name = _bookName},
            new() {Id = 2, Name = _bookName}
        };

        var borrowRequestEntity = new BorrowRequest
        {
            Id = _borrowRequestId,
            Status = RequestStatus.Waiting,
            RequestedAt = _fakeDateTime,
            RequestedBy = _normalUserId,
            Requester = new User
            {
                Id = _normalUserId,
                Username = _userUsername,
                Password = _userPassword,
                Name = _userName,
                Role = Role.NormalUser
            },
            Books = bookEntities
        };

        _bookRepository
            .Setup(br => br.GetAllAsync(It.IsAny<Expression<Func<Book, bool>>>()))
            .ReturnsAsync(bookEntities);

        _borrowRequestRepository
            .Setup(brr => brr.Create(It.IsAny<BorrowRequest>()))
            .Returns(borrowRequestEntity);

        var result = await _borrowRequestService.CreateAsync(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<CreateBorrowRequestResponse>());

            Assert.That(result?.Id, Is.EqualTo(_borrowRequestId));

            Assert.That(result?.Status, Is.EqualTo(_borrowRequestStatus.ToString()));

            Assert.That(result?.RequestedAt, Is.EqualTo(_fakeDateTime));

            Assert.That(result?.RequestedBy, Is.EqualTo(_normalUserId));

            Assert.That(result?.Books, Is.InstanceOf<List<BookModel>>());

            Assert.That(result?.Books, Has.Count.EqualTo(2));
        });
    }

    [Test]
    public async Task ApproveAsync_NullApprover_ReturnsNull()
    {
        var input = new ApproveBorrowRequestRequest
        {
            Approver = null
        };

        var result = await _borrowRequestService.ApproveAsync(input);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ApproveAsync_InvalidBorrowRequestId_ReturnsNull()
    {
        var input = new ApproveBorrowRequestRequest
        {
            Approver = new UserModel
            {
                Id = _superUserId,
                Role = Role.SuperUser
            },
            IsApproved = true,
            Id = _borrowRequestId
        };

        _borrowRequestRepository
            .Setup(brr => brr.GetSingleAsync(It.IsAny<Expression<Func<BorrowRequest, bool>>>()))
            .ReturnsAsync(null as BorrowRequest);

        var result = await _borrowRequestService.ApproveAsync(input);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ApproveAsync_ValidInput_ReturnsNull()
    {
        var input = new ApproveBorrowRequestRequest
        {
            Approver = new UserModel
            {
                Id = _superUserId,
                Role = Role.SuperUser
            },
            IsApproved = true,
            Id = _borrowRequestId
        };

        var borrowRequestEntity = new BorrowRequest
        {
            Id = _borrowRequestId,
            Status = RequestStatus.Waiting,
            RequestedAt = _fakeDateTime,
            RequestedBy = _normalUserId,
            Requester = new User
            {
                Id = _normalUserId,
                Username = _userUsername,
                Password = _userPassword,
                Name = _userName,
                Role = Role.NormalUser
            },
            Books = new List<Book>
            {
                new() {Id = 1, Name = _bookName},
                new() {Id = 2, Name = _bookName}
            }
        };

        var updatedBorrowRequestEntity = new BorrowRequest
        {
            Id = _borrowRequestId,
            Status = RequestStatus.Approved,
            RequestedAt = _fakeDateTime,
            RequestedBy = _normalUserId,
            Requester = new User
            {
                Id = _normalUserId,
                Username = _userUsername,
                Password = _userPassword,
                Name = _userName,
                Role = Role.NormalUser
            },
            ApprovedAt = _fakeDateTime,
            ApprovedBy = _superUserId,
            Approver = new User
            {
                Id = _superUserId,
                Username = _userUsername,
                Password = _userPassword,
                Name = _userName,
                Role = Role.SuperUser
            },
            Books = new List<Book>
            {
                new() {Id = 1, Name = _bookName},
                new() {Id = 2, Name = _bookName}
            }
        };

        _borrowRequestRepository
            .Setup(brr => brr.GetSingleAsync(It.IsAny<Expression<Func<BorrowRequest, bool>>>()))
            .ReturnsAsync(borrowRequestEntity);

        _borrowRequestRepository
            .Setup(brr => brr.Update(It.IsAny<BorrowRequest>()))
            .Returns(updatedBorrowRequestEntity);

        var result = await _borrowRequestService.ApproveAsync(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<ApproveBorrowRequestResponse>());

            Assert.That(result?.Id, Is.EqualTo(_borrowRequestId));

            Assert.That(result?.Status, Is.EqualTo(RequestStatus.Approved.ToString()));

            Assert.That(result?.RequestedAt, Is.EqualTo(_fakeDateTime));

            Assert.That(result?.ApprovedAt, Is.EqualTo(_fakeDateTime));

            Assert.That(result?.Requester.Id, Is.EqualTo(_normalUserId));

            Assert.That(result?.Requester.Username, Is.EqualTo(_userUsername));

            Assert.That(result?.Requester.Name, Is.EqualTo(_userName));

            Assert.That(result?.Requester.Role, Is.EqualTo(Role.NormalUser.ToString()));

            Assert.That(result?.Approver, Is.Not.Null);

            Assert.That(result?.Approver?.Id, Is.EqualTo(_superUserId));

            Assert.That(result?.Approver?.Username, Is.EqualTo(_userUsername));

            Assert.That(result?.Approver?.Name, Is.EqualTo(_userName));

            Assert.That(result?.Approver?.Role, Is.EqualTo(Role.SuperUser.ToString()));

            Assert.That(result?.Books, Is.InstanceOf<List<BookModel>>());

            Assert.That(result?.Books, Has.Count.EqualTo(2));
        });
    }

    [Test]
    public async Task IsRequestValid_NoRequester_ReturnFalseValidCheckingWrapper()
    {
        var input = new CreateBorrowRequestRequest
        {
            Requester = null
        };

        var result = await _borrowRequestService.IsRequestValid(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ValidCheckingWrapper>());

            Assert.That(result.IsValid, Is.False);

            Assert.That(result.Message, Is.EqualTo(ErrorMessages.RequestHasNoRequester));
        });
    }

    [Test]
    public async Task IsRequestValid_BookPerRequestLimitExceed_ReturnFalseValidCheckingWrapper()
    {
        var bookIds = new List<int>();

        for (var i = 1; i <= Settings.MaxBooksPerRequest + 1; ++i) bookIds.Add(i);

        var input = new CreateBorrowRequestRequest
        {
            Requester = new UserModel
            {
                Id = _normalUserId,
                Role = Role.NormalUser
            },
            BookIds = bookIds
        };

        var result = await _borrowRequestService.IsRequestValid(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ValidCheckingWrapper>());

            Assert.That(result.IsValid, Is.False);

            Assert.That(result.Message, Is.EqualTo(ErrorMessages.BooksPerRequestLimitExceeded));
        });
    }

    [Test]
    public async Task IsRequestValid_BookPerRequestLimitNotReached_ReturnFalseValidCheckingWrapper()
    {
        var bookIds = new List<int>();

        for (var i = 1; i <= Settings.MinBooksPerRequest - 1; ++i) bookIds.Add(i);

        var input = new CreateBorrowRequestRequest
        {
            Requester = new UserModel
            {
                Id = _normalUserId,
                Role = Role.NormalUser
            },
            BookIds = bookIds
        };

        var result = await _borrowRequestService.IsRequestValid(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ValidCheckingWrapper>());

            Assert.That(result.IsValid, Is.False);

            Assert.That(result.Message, Is.EqualTo(ErrorMessages.BooksPerRequestLimitNotReached));
        });
    }

    [Test]
    public async Task IsRequestValid_RequestsPerMonthLimitExceeded_ReturnFalseValidCheckingWrapper()
    {
        var bookIds = new List<int>();

        for (var i = 1; i <= Settings.MinBooksPerRequest; ++i) bookIds.Add(i);

        var input = new CreateBorrowRequestRequest
        {
            Requester = new UserModel
            {
                Id = _normalUserId,
                Role = Role.NormalUser
            },
            BookIds = bookIds
        };

        var borrowRequestEntities = new List<BorrowRequest>();

        for (var i = 1; i <= Settings.MaxBorrowRequestsPerMonth + 1; ++i)
            borrowRequestEntities.Add(new BorrowRequest
            {
                Id = i,
                Status = RequestStatus.Waiting,
                RequestedAt = _fakeDateTime,
                RequestedBy = _normalUserId
            });

        _borrowRequestRepository
            .Setup(brr => brr.GetAllAsync(It.IsAny<Expression<Func<BorrowRequest, bool>>>()))
            .ReturnsAsync(borrowRequestEntities);

        var result = await _borrowRequestService.IsRequestValid(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ValidCheckingWrapper>());

            Assert.That(result.IsValid, Is.False);

            Assert.That(result.Message, Is.EqualTo(ErrorMessages.RequestsPerMonthLimitExceeded));
        });
    }

    [Test]
    public async Task IsRequestValid_ValidRequest_ReturnTrueValidCheckingWrapper()
    {
        var input = new CreateBorrowRequestRequest
        {
            Requester = new UserModel
            {
                Id = _normalUserId,
                Role = Role.NormalUser
            },
            BookIds = new List<int> {1, 2, 3, 4, 5}
        };

        var borrowRequestEntities = new List<BorrowRequest>();

        _borrowRequestRepository
            .Setup(brr => brr.GetAllAsync(It.IsAny<Expression<Func<BorrowRequest, bool>>>()))
            .ReturnsAsync(borrowRequestEntities);

        var result = await _borrowRequestService.IsRequestValid(input);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<ValidCheckingWrapper>());

            Assert.That(result.IsValid, Is.True);

            Assert.That(result.Message, Is.Null);
        });
    }
}