using FleetHQ.Server.Helpers;
using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;

using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Domains.Auth;

public interface IAuthService
{
    Task<XResult> Register(RegisterDto dto);
    Task<XResult> Login(LoginDto dto);
}

public class AuthService(RepositoryContext repository, IJwtTokenManager jwtTokenManager) : IAuthService
{

    private readonly RepositoryContext _repository = repository;
    private readonly IJwtTokenManager _jwtTokenManager = jwtTokenManager;

    public async Task<XResult> Register(RegisterDto dto)
    {
        var isEmailTaken = await _repository.Users.AnyAsync(u => u.Email == dto.Email);

        if (isEmailTaken) return XResult.Failure(["email is registered to an existing account"]);

        var validationResult = new AuthValidator().Validate(dto);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return XResult.Failure(errors);
        }

        var newUser = new UserModel
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            ContactNumber = dto.ContactNumber,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            OnBoarding = OnBoarding.Company,
            Role = RoleFactory.Manager()
        };


        await _repository.Users.AddAsync(newUser);
        await _repository.SaveChangesAsync();

        var response = new AuthResponseDto
        {
            AccessToken = _jwtTokenManager.GenerateAccessToken(newUser.Id, newUser.RoleId)
        };

        return XResult.Success("account created!", response);
    }

    public async Task<XResult> Login(LoginDto dto)
    {
        var user = await _repository.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (user == null) return XResult.Failure(["bad credentials"]);

        var authenticationResult = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!authenticationResult)
        {
            return XResult.Failure(["bad credentials"]);
        }

        var response = new AuthResponseDto
        {
            AccessToken = _jwtTokenManager.GenerateAccessToken(user.Id, user.RoleId)
        };

        return XResult.Success("signed-in!", response);
    }
}