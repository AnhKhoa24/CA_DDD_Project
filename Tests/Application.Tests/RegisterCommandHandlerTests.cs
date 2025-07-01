using Application.Services.Authentication.Commands.Register;
using Application.Services.Authentication.Commmon;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using FluentAssertions;
using Moq;
public class RegisterCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock = new();

    [Fact]
    public async Task Handle_EmailAlreadyExists_ReturnsDuplicateEmailError()
    {
        // Arrange: giả lập user đã tồn tại
        var existingUser = new User { Email = "test@example.com" };
        _userRepositoryMock.Setup(r => r.GetUserByEmail("test@example.com"))
            .Returns(existingUser);

        var handler = new RegisterCommandHandler(
            _jwtTokenGeneratorMock.Object,
            _userRepositoryMock.Object);

        var command = new RegisterCommand(
            "First", "Last", "test@example.com", "password123");

        // Act
        ErrorOr<AuthenticationResult> result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors.Should().Contain(Errors.User.DuplicateEmail);

        // Đảm bảo AddUser không được gọi
        _userRepositoryMock.Verify(r => r.AddUser(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task Handle_EmailDoesNotExist_AddsUserAndReturnsAuthenticationResult()
    {
        // Arrange: giả lập email chưa tồn tại
        _userRepositoryMock.Setup(r => r.GetUserByEmail("new@example.com"))
            .Returns((User?)null);

        // Setup token giả
        _jwtTokenGeneratorMock.Setup(t => t.GenerateToken(It.IsAny<User>()))
            .Returns("fake-jwt-token");

        var handler = new RegisterCommandHandler(
            _jwtTokenGeneratorMock.Object,
            _userRepositoryMock.Object);

        var command = new RegisterCommand(
            "First", "Last", "new@example.com", "password123");

        // Act
        ErrorOr<AuthenticationResult> result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.token.Should().Be("fake-jwt-token");
        result.Value.user.Should().NotBeNull();
        result.Value.user.Email.Should().Be("new@example.com");

        // Đảm bảo AddUser được gọi đúng 1 lần
        _userRepositoryMock.Verify(r => r.AddUser(It.Is<User>(u => u.Email == "new@example.com")), Times.Once);

        // Đảm bảo GenerateToken được gọi đúng 1 lần
        _jwtTokenGeneratorMock.Verify(t => t.GenerateToken(It.IsAny<User>()), Times.Once);
    }
}
