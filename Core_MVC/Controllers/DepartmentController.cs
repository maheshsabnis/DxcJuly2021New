using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_MVC.Models;
using Core_MVC.Services;
using Core_MVC.CustomFilters;
namespace Core_MVC.Controllers
{
 //   [RequestLogFilter]
    public class DepartmentController : Controller
    {
        private readonly IService<Department, int> deptServ;
        /// <summary>
        /// DI for nDe[artmentService
        /// </summary>
        /// <param name=""></param>
        public DepartmentController(IService<Department, int> serv)
        {
            deptServ = serv;
        }
        public async Task<IActionResult> Index()
        {
            var res = await deptServ.GetAsync();

            return View(res);
        }

        /// <summary>
        /// Render the View with Empty UI for Acceping the Department Data
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var res = new Department();
            return View(res);
        }

        /// <summary>
        /// The Action Method That will be executed with the Post Request of HTML Form
        /// that is bound with the Department Object
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Department dept)
        {
            //try
            //{
                // Check for Model Validation, the Postefd data from Form
                if (ModelState.IsValid)
                {
                    if (dept.DeptNo < 0) throw new Exception("DeptNo cannot be -ve");
                    var res = await deptServ.CreateAsync(dept);
                    return RedirectToAction("Index");
                }
                else
                {
                    // stey on same view with Error Pages
                    return View(dept);
                 }
            //}
            //catch (Exception ex)
            //{
            //    return View("Error", new ErrorViewModel()
            //    {
            //         ControllerName = this.RouteData.Values["controller"].ToString(),
            //          ActionName = this.RouteData.Values["action"].ToString(),
            //          ErrorMessage = ex.Message
            //    });
            //}
        }
        /// <summary>
        /// Record to be edited
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var res = await deptServ.GetAsync(id);
            return View(res);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int id, Department dept)
        {
            // Check for Model Validation, the Postefd data from Form
            if (!ModelState.IsValid)
            {
                var res = await deptServ.UpdateAsync(id, dept);
                return RedirectToAction("Index");
            }
            else
            {
                // stey on same view with Error Pages
                return View(dept);
            }
        }

    }

}
