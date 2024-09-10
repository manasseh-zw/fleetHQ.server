using FleetHQ.Server.Domains.Auth;
using FleetHQ.Server.Helpers;
using FleetHQ.Server.Repository;
using FleetHQ.Server.Repository.Models;

using Microsoft.EntityFrameworkCore;

namespace FleetHQ.Server.Domains.Company;

public interface ICompanyService
{
    Task<IXResult> Create(Guid userId, CreateCompanyDto dto);
}
public class CompanyService(RepositoryContext repository, IJwtTokenManager jwtTokenManager) : ICompanyService
{
    private readonly RepositoryContext _repository = repository;
    private readonly IJwtTokenManager _jwtTokenManager = jwtTokenManager;

    public async Task<IXResult> Create(Guid userId, CreateCompanyDto dto)
    {
        var user = await _repository.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
        {
            return XResult.Fail(["User not found"]);
        }

        var company = new CompanyModel
        {
            Name = dto.Name,
            Email = dto.Email,
            Address = dto.Address,
            ContactNumber = dto.ContactNumber
        };

        var validationResult = new CompanyValidator().Validate(company);

        if (!validationResult.IsValid)
        {
            return XResult.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }


        await _repository.Companies.AddAsync(company);

        user.OnBoarding = OnBoarding.Vehicle;
        user.CompanyId = company.Id;

        await _repository.SaveChangesAsync();

        var response = new AuthResponse
        {
            AccessToken = _jwtTokenManager.GenerateAccessToken(user.Id, user.RoleId, company.Id)
        };
        return XResult.Ok(response, "Company created");
    }
}