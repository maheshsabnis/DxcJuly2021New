using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Core_API.Services
{
    public class EmployeeService : IService<Employee, int>
    {
        private readonly CompanyContext context;
        /// <summary>
        /// Injecting the DbContext in Service 
        /// </summary>
        /// <param name="ctx"></param>
        public EmployeeService(CompanyContext ctx)
        {
            context = ctx;
        }


        public async Task<Employee> CreateAsync(Employee entity)
        {
            var result = await context.Employees.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public Task<Employee> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAsync()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await context.Employees.FindAsync(id);
        }

        public Task<Employee> UpdateAsync(int id, Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
