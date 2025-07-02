using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers;

public class DeveloperController : Controller
{
    // GET: Developer/BecomeDeveloper
    public IActionResult BecomeDeveloper()
    {
        return View();
    }
    
    // POST: Developer/BecomeDeveloper
    [HttpPost]
    public IActionResult BecomeDeveloper(Developer developer)
    {
        if (ModelState.IsValid)
            return RedirectToAction("Confirmed", "Developer", routeValues: developer);
        return View(developer);
    }
    
    // GET: Developer/Confirmed
    public IActionResult Confirmed(Developer developer)
    {
        return View(developer);
    }
}