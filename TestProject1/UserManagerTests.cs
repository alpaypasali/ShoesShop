using Xunit;
using Moq;
using ShoeShop.DataAccess.Abstract;
using ShoeShop.Entities.Concrete;
using AutoMapper;
using ShoeShop.Businness.Concrete;
using ShoeShop.Dtos.Concrete;

public class UserManagerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UserManager _userManager;

    public UserManagerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _userManager = new UserManager(_mockUserRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public void AddUser_AddsUserAndHashesPassword()
    {
        // Arrange
        var userDto = new UserDto { Email = "test@example.com", Password = "plaintext" };
        var user = new User { Email = "test@example.com", Password = "hashedpassword" };

        _mockMapper.Setup(m => m.Map<User>(userDto)).Returns(user);

        // Act
        _userManager.AddUser(userDto);

        // Assert
        _mockUserRepository.Verify(u => u.Add(It.Is<User>(x => x.Password != "plaintext")), Times.Once);
    }

    [Fact]
    public void IsExist_ReturnsTrueIfUserExists()
    {
        // Arrange
        string email = "test@example.com";
        _mockUserRepository.Setup(u => u.IsExists(email)).Returns(true);

        // Act
        var result = _userManager.IsExist(email);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetUser_ReturnsUserDtoFromRepository()
    {
        // Arrange
        string email = "test@example.com";
        var user = new User { Email = email };
        var userDto = new UserDto { Email = email };

        _mockUserRepository.Setup(u => u.GetUserByEmail(email)).Returns(user);
        _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

        // Act
        var result = _userManager.GetUser(email);

        // Assert
        Assert.Equal(userDto, result);
        _mockUserRepository.Verify(u => u.GetUserByEmail(email), Times.Once);
        _mockMapper.Verify(m => m.Map<UserDto>(user), Times.Once);
    }

    [Fact]
    public void ValidateUser_ReturnsUserDtoIfValid()
    {
        // Arrange
        string email = "test@example.com";
        string password = "password";
        var user = new User { Email = email, Password = BCrypt.Net.BCrypt.HashPassword(password) };
        var userDto = new UserDto { Email = email };

        _mockUserRepository.Setup(u => u.GetUserByEmail(email)).Returns(user);
        _mockMapper.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

        // Act
        var result = _userManager.ValidateUser(email, password);

        // Assert
        Assert.Equal(userDto, result);
        _mockUserRepository.Verify(u => u.GetUserByEmail(email), Times.Once);
        _mockMapper.Verify(m => m.Map<UserDto>(user), Times.Once);
    }

    // And so on for the other methods...
}
