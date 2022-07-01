namespace KekAuth.Tests.Systems.Controllers;

public class AuthControllerTest
{
    [Fact]
    public async Task Token_OnSuccess_ShouldReturnStatusCode200()
    {
        //Arrange
        // var tokenController =
        //     new AuthController(
        //         new AuthenticationService(new JwtTokenGenerator(new DateTimeProvider(),
        //             Options.Create(new JwtConfiguration()))));
        // //Act
        // var result = (OkObjectResult) await tokenController.Login(new LoginRequest());
        //
        // //Assert
        // result.StatusCode.Should().Be(200);
        // result.Value.Should().Be("User");
    }

    [Fact]
    public async Task Token_OnSuccess_InvokeUsersService()
    {
        //Arrange
        //var mockUserService = Mock<IUsersService>();
        //var tokenController = new AuthController(mockUserService.Object);
        //Act
        //var result = (OkObjectResult) await tokenController.Token("User");

        //Assert
        //result.StatusCode.Should().Be(200);
        //result.Value.Should().Be("User");
    }
}