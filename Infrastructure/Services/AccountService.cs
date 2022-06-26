using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Dtos;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Specifications;
using Infrastructure.ViewModels;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserSignInResult> UserPasswordSignInAsync(UserViewModel model) 
    {
        var user = await GetCurrentUserAsync(model.Email);

        if (user != null)
        {
            using var hmac = new HMACSHA512(user.PasswordSalt);
            
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            
            for (int i = 0; i < user.PasswordHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) return new UserSignInResult(false, null);
            }

            var claims = SetClaims(user);

            var claimsIdentity = new ClaimsIdentity(claims, "user");
            var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

            return new UserSignInResult(true, claimsPrinciple);
        }

        return new UserSignInResult(false, null);
    }

    public async Task<User> GetCurrentUserAsync(string email)
    {
        var spec = new UserSpecification(email);
        return await _unitOfWork.Repository<User>().GetItemWithSpecificAsync(spec);
    }
    public async Task<bool> IsUserExists(string email)
    {
        return await GetCurrentUserAsync(email) != null;
    }

    private List<Claim> SetClaims(User user)
    {
        return new List<Claim> 
        {
            new Claim(ClaimTypes.Sid, user.Id),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Name,user.FirstName + " " + user.LastName),
            new Claim(ClaimTypes.Email, user.Email)
        };
    }
}
