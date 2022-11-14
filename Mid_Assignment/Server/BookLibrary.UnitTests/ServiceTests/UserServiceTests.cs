using System.Linq.Expressions;
using BookLibrary.Data.Entities;
using BookLibrary.Data.Interfaces;
using BookLibrary.WebApi.Dtos.User;
using BookLibrary.WebApi.Helpers;
using BookLibrary.WebApi.Services.Implements;
using Common.Enums;
using Moq;
using NUnit.Framework;

namespace BookLibrary.UnitTests.ServiceTests;

public class UserServiceTests
{
    private const int _userId = 1;
    private const string _userUsername = "Username";
    private const string _userPassword = "Password";
    private const string _userName = "Name";
    private const Role _userRole = Role.NormalUser;
    private Mock<IUserRepository> _userRepository;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _userRepository = new Mock<IUserRepository>();
        _userService = new UserService(_userRepository.Object);
    }

    [Test]
    public async Task GetByIdAsync_ValidId_ReturnsUserModel()
    {
        var userEntity = new User
        {
            Id = _userId,
            Username = _userUsername,
            Password = _userPassword,
            Name = _userName,
            Role = _userRole
        };

        var expected = new UserModel
        {
            Id = _userId,
            Role = _userRole
        };

        _userRepository
            .Setup(ur => ur.GetSingleAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(userEntity);

        var result = await _userService.GetByIdAsync(It.IsAny<int>());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<UserModel>());

            Assert.That(result?.Id, Is.EqualTo(expected.Id));

            Assert.That(result?.Role, Is.EqualTo(expected.Role));
        });
    }

    [Test]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        _userRepository
            .Setup(ur => ur.GetSingleAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(null as User);

        var result = await _userService.GetByIdAsync(It.IsAny<int>());

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task AuthenticateAsync_ValidInput_ReturnsAuthenticationResponse()
    {
        var userEntity = new User
        {
            Id = _userId,
            Username = _userUsername,
            Password = _userPassword,
            Name = _userName,
            Role = _userRole
        };

        _userRepository
            .Setup(ur => ur.GetSingleAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(userEntity);

        var result = await _userService.AuthenticateAsync(It.IsAny<AuthenticationRequest>());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);

            Assert.That(result, Is.InstanceOf<AuthenticationResponse>());

            Assert.That(result?.Id, Is.EqualTo(_userId));

            Assert.That(result?.Username, Is.EqualTo(_userUsername));

            Assert.That(result?.Name, Is.EqualTo(_userName));

            Assert.That(result?.Role, Is.EqualTo(_userRole.ToString()));

            Assert.That(result?.Token, Is.Not.Null.And.Not.Empty);

            var userIdFromToken = JwtHelper.ValidateJwtToken(result?.Token);

            Assert.That(userIdFromToken, Is.Not.Null);

            Assert.That(userIdFromToken, Is.EqualTo(_userId));
        });
    }

    [Test]
    public async Task AuthenticateAsync_InvalidInput_ReturnsNull()
    {
        _userRepository
            .Setup(ur => ur.GetSingleAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(null as User);

        var result = await _userService.AuthenticateAsync(It.IsAny<AuthenticationRequest>());

        Assert.That(result, Is.Null);
    }
}