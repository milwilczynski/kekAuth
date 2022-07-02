using KekAuth.Application.Interfaces;
using KekAuth.Application.Models;
using KekAuth.Application.Presistances;
using KekAuth.Domain.Entities;

namespace KekAuth.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    //register user
    public async Task<AuthenticationResult> Register(
        string login, string password, string email, string? firstName,
        string? lastName)
    {
        //Validate the user dosen't exist
        if (await _userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User already exists");
        }

        password = "$argon2id$v=19$m=65536,t=3,p=1$MTDqPHeAha3EGDrCxTt+Hw$d313xIgexWPmC/ooti4Sw4DWsACE4RJJ8cTa4QnByK8";
        //Create user (generate unique id) & persist to database
        var user = new User
        {
            UserToken = Guid.NewGuid(),
            Login = login,
            Password = password,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };
        //add user to database
        var dbResponse = await _userRepository.Add(user);
        if (!dbResponse)
        {
            throw new Exception("Failed to add user to database");
        }

        //generate jwt
        var jwt = _jwtTokenGenerator.GenerateToken(user.UserToken, user.Login, user.Email, user.FirstName,
            user.LastName);

        return new AuthenticationResult(jwt);
    }

    public async Task<AuthenticationResult> Login(string email, string password)
    {
        if (await _userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User does not exist");
        }

        password = "$argon2id$v=19$m=65536,t=3,p=1$MTDqPHeAha3EGDrCxTt+Hw$d313xIgexWPmC/ooti4Sw4DWsACE4RJJ8cTa4QnByK8";
        //Validate password
        if (!user.Password.Equals(password))
        {
            throw new Exception("Password is incorrect");
        }

        //generate jwt
        var jwt = _jwtTokenGenerator.GenerateToken(user.UserToken, user.Login, user.Email, user.FirstName,
            user.LastName);
        return new AuthenticationResult(jwt);
    }
}