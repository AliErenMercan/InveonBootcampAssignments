using LibraryManagement.Models;
using LibraryManagement.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class RoleController(RoleManager<AppRole> roleManager) : Controller
    {
        [HttpGet]
        public IActionResult Choice()
        {
            return View();
        }
    }
}
