using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_MVC.Models;
using Core_MVC.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Core_MVC.CustomSessionExtensions;
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
            // show Employees for the selected DeptNo
            // var deptNo = HttpContext.Session.GetInt32("DeptNo");
            //  var dept =  JsonSerializer.Deserialize<Department>(HttpContext.Session.GetString("Dept"));
            // var dept = HttpContext.Session.GetObject<Department>("Dept");
            var deptNo = Convert.ToInt32(TempData["DeptNo"]);
            if (deptNo == 0)
            {
                var res = await empServ.GetAsync();
                TempData.Keep(); // Ask the COntroller and Hence the HttpContext to maintain the State of the Tempdate. TempData.Keep(); will maintain state for all keys 
                // TempData.Keep("[Key]"); will maintain state for only the specific key
                return View(res);
            }
            else
            {
                TempData.Keep();
                var res = await empServ.GetAsync();
                var emps = res.Where(e => e.DeptNo == deptNo).ToList();
                ViewBag.DeptNo = deptNo;
                return View(emps);
            }
         
        }

        public async Task<IActionResult> Create()
        {
            var val = Convert.ToInt32(TempData["DeptNo"]);
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
        public async Task<IActionResult> IndexTag()
        {
            var res = await empServ.GetAsync();
             
            return View(res);
        }
    }
}
