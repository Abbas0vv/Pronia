using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using Pustok.Services.Abstracts;
using Pustok.Services.Concretes;

namespace Pustok.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly PustokDbContext _dbContext;
    private readonly IUserService _userService;

    public AccountController(PustokDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Dashboard()
    {
        var user1 = _userService.CurrentUser;



        return View();
    }
    public IActionResult Orders()
    {
        return View();
    }

    public IActionResult Addresses()
    {
        return View();
    }

    public IActionResult AccountDetails()
    {
        return View();
    }

    public IActionResult Logout()
    {
        //logic

        return RedirectToAction("index", "home");
    }

}
