using System.Security.Claims;

namespace Infrastructure.Dtos;

public class UserSignInResult
{
    public UserSignInResult(bool succeeded, ClaimsPrincipal claimsPrincipal)
    {
        Succeeded = succeeded;
        ClaimsPrincipal = claimsPrincipal;
    }

    public bool Succeeded { get; set; }
    public ClaimsPrincipal ClaimsPrincipal { get; set; }   
}