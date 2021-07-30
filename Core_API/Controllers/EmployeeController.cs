using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_API.Models;
using Core_API.Services;

namespace Core_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IService<Employee, int> empServ;
        private readonly IService<Department, int> deptServ;
        public EmployeeController(IService<Employee, int> serv, IService<Department, int> serv1)
        {
            empServ = serv;
            deptServ = serv1;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await empServ.GetAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await empServ.GetAsync(id);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Employee emp)
        {
            //try
            //{
                if (ModelState.IsValid)
                {
                    if (emp.Salary < 0) throw new Exception("Salary Cannot Be -ve");
                    var dept = await deptServ.GetAsync(emp.DeptNo);
                    if (dept == null)
                        throw new Exception("Sorry the DeptNo is not found");

                    var res = await empServ.CreateAsync(emp);
                    return Ok(res);
                }
                return BadRequest(ModelState);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest($"Error Occured {ex.Message}");
            //}
        }
    }
}
