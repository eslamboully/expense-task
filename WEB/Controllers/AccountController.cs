using System.Security.Claims;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Dashboard.Controllers;

[Authorize(AuthenticationSchemes = "user")]
public class AccountController : BaseController
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("login")]
    [AllowAnonymous]
    public ActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index","Home");
        }
        return View();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login(UserViewModel model)
    {
        var result = await _accountService.UserPasswordSignInAsync(model);

        if (result.Succeeded)
        {
            await HttpContext.SignInAsync("user", result.ClaimsPrincipal,
                new AuthenticationProperties{IsPersistent = model.RememberMe});
                
            if (!string.IsNullOrEmpty(model.ReturnUrl)) return Redirect(model.ReturnUrl); 
            return RedirectToAction("Index","Home");
        }

        ViewData["msg"] = "Invalid Email or Password";
        return View(model);
    }

    [HttpGet("logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync("admin");
        return RedirectToAction(nameof(Login));
    }    
}