using Microsoft.AspNetCore.Mvc;

namespace Pustok.Controllers;

public class AccountController : Controller
{
    public IActionResult Dashboard()
    {
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
