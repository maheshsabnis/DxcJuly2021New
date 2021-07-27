using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_MVC.Models;
using Core_MVC.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core_MVC.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IService<Employee, int> empServ;
        private readonly IService<Department, int> deptServ;

        public EmployeeController(IService<Employee, int> serv, IService<Department, int> srv)
        {
            empServ = serv;
            deptServ = srv;
        }

        public async Task<IActionResult> Index()
        {
            var res = await empServ.GetAsync();
            return View(res);
        }

        public async Task<IActionResult> Create()
        {
            var emp = new Employee();
            ViewBag.Name = "The Employee Create View";
            // get collection of Depratments
            ViewBag.DeptNo = await deptServ.GetAsync();
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid) 
            {
                var res = await empServ.CreateAsync(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }
    }
}
