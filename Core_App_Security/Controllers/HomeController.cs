using Core_App_Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Core_App_Security.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles="Manager")]
        public IActionResult Privacy()
        {
            ViewBag.RoleName = "The Pahe is for Manager";
            return View();
        }

       // [Authorize(Roles = "Manager,Clerk")]
       [Authorize(Policy = "ManagerClerkPolicy")]
        public IActionResult Clerk()
        {
            ViewBag.RoleName = "The Pahe is for Clerk";
            return View();
        }



       // [Authorize(Roles = "Operator")]
       [Authorize(Policy = "AllRolePolicy")]
        public IActionResult Operator()
        {
            ViewBag.RoleName = "The Pahe is for Operator";
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
