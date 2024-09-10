using FleetHQ.Server.Configuration;
using FleetHQ.Server.Domains.Company;
using FleetHQ.Server.Domains.User;
using FleetHQ.Server.Helpers;
using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;

using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Domains.Auth;

public interface IAuthService
{
    Task<IXResult> Register(RegisterDto dto);
    Task<IXResult> Login(LoginDto dto);
    Task<IXResult> CurrentUser(Guid userId);
}

public class AuthService(RepositoryContext repository, IJwtTokenManager jwtTokenManager) : IAuthService
{

    private readonly RepositoryContext _repository = repository;
    private readonly IJwtTokenManager _jwtTokenManager = jwtTokenManager;

    public async Task<IXResult> Register(RegisterDto dto)
    {
        var isEmailTaken = await _repository.Users.AnyAsync(u => u.Email == dto.Email);

        if (isEmailTaken) return XResult.Fail(["email is registered to an existing account"]);

        var validationResult = new AuthValidator().Validate(dto);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return XResult.Fail(errors);
        }

        var newUser = new UserModel
        {
            Email = dto.Email,
            FullName = dto.FullName,
            ContactNumber = dto.ContactNumber,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            OnBoarding = OnBoarding.Company,
            Role = RoleFactory.Manager()
        };


        await _repository.Users.AddAsync(newUser);
        await _repository.SaveChangesAsync();

        var response = new AuthResponse
        {
            AccessToken = _jwtTokenManager.GenerateAccessToken(newUser.Id, newUser.RoleId)
        };

        return XResult.Ok(response, "account created!");
    }

    public async Task<IXResult> Login(LoginDto dto)
    {
        var user = await _repository.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (user == null) return XResult.Fail(["bad credentials"]);

        var authenticationResult = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!authenticationResult)
        {
            return XResult.Fail(["bad credentials"]);
        }

        var response = new AuthResponse
        {
            AccessToken = _jwtTokenManager.GenerateAccessToken(user.Id, user.RoleId)
        };
        return XResult.Ok(response, "signed-in!");
    }

    public async Task<IXResult> CurrentUser(Guid userId)
    {
        var user = await _repository.Users.Select(u => new UserProfileDto
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email,
            OnBoarding = u.OnBoarding,
            Role = new RoleDto
            {
                Name = u.Role.Name,
                Permissions = u.Role.Permissions
            },
            Company = u.Company == null ? null : new CompanyProfileDto
            {
                Id = u.Company.Id,
                Name = u.Company.Name
            }
        }).FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            XResult.Fail(["user not found"]);
        }

        return XResult.Ok(user);

    }

}