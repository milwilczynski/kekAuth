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
    public async Task<AuthenticationResult> Register(string login, string password, string email, string? firstName,
        string? lastName)
    {
        //Validate the user dosen't exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User already exists");
        }

        //Create user (generate unique id) & persist to database
        var user = new User
        {
            Id = Guid.NewGuid(),
            Login = login,
            Password = password,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };
        //generate jwt
        var jwt = _jwtTokenGenerator.GenerateToken(user.Id, user.Login, user.Email, user.FirstName, user.LastName);
        return new AuthenticationResult(jwt);
    }

    public async Task<AuthenticationResult> Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User does not exist");
        }

        //Validate password
        if (!user.Password.Equals(password))
        {
            throw new Exception("Password is incorrect");
        }

        //generate jwt
        var jwt = _jwtTokenGenerator.GenerateToken(user.Id, user.Login, user.Email, user.FirstName, user.LastName);
        return new AuthenticationResult(jwt);
    }
}