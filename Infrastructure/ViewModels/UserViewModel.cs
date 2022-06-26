using System.ComponentModel.DataAnnotations;

namespace Infrastructure.ViewModels;

public class UserViewModel
{
    public UserViewModel()
    {
    }

    public UserViewModel(string firstName, string lastName, string email, string password, bool rememberMe)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        RememberMe = rememberMe;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
    public bool RememberMe { get; set; } = false;
}