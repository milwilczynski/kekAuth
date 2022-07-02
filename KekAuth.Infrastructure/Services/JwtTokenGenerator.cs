using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KekAuth.Application.Interfaces;
using KekAuth.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KekAuth.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtConfiguration _jwtConfiguration;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtConfiguration> jwtConfiguration)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtConfiguration = jwtConfiguration.Value;
    }

    //generate jwt token
    public string GenerateToken(Guid userId, string login, string email, string firstName, string lastName)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            //claim with user roles
            new Claim(ClaimTypes.Role, "User"),
            new Claim(JwtRegisteredClaimNames.Iat,
                _dateTimeProvider.Now
                    .ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
            new Claim(JwtRegisteredClaimNames.UniqueName, login)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtConfiguration.Issuer,
            _jwtConfiguration.Audience,
            claims,
            expires: _dateTimeProvider.Now.AddMinutes(_jwtConfiguration.ExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}