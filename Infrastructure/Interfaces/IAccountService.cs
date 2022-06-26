using Infrastructure.Dtos;
using Infrastructure.Models;
using Infrastructure.ViewModels;

namespace Infrastructure.Interfaces;

public interface IAccountService
{
    Task<UserSignInResult> UserPasswordSignInAsync(UserViewModel model);
    Task<User> GetCurrentUserAsync(string email);
    Task<bool> IsUserExists(string email);
}
