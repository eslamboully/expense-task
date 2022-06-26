using System.Security.Cryptography;
using System.Text;
using Infrastructure.Models;

namespace Infrastructure;

public static class DatabaseContextSeed
{
    public static void SeedData(DatabaseContext context)
    {
        using var hmac = new HMACSHA512();

        if (!context.Users.Any())
        {
            var admin = new User();
                admin.FirstName = "Abdelrahman";
                admin.LastName = "Osama";
                admin.Email = "user@user.com";
                admin.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("user"));
                admin.PasswordSalt = hmac.Key;

            context.Users.Add(admin);
            context.SaveChanges();
        }
        
    }
}