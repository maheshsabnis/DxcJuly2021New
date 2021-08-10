using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_API.Models;
using Microsoft.EntityFrameworkCore;
using SharedLib;

namespace Core_API.Services
{
    public class DepartmentService : IService<Department, int>
    {
        private readonly CompanyContext context;
        /// <summary>
        /// Injecting the DbContext in Service 
        /// </summary>
        /// <param name="ctx"></param>
        public DepartmentService(CompanyContext ctx)
        {
            context = ctx;
        }


        public async Task<Department> CreateAsync(Department entity)
        {
            var result = await context.Departments.AddAsync(entity);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public Task<Department> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Department>> GetAsync()
        {
            return await context.Departments.ToListAsync();
        }

        public async Task<Department> GetAsync(int id)
        {
            return await context.Departments.FindAsync(id);
        }

        public Task<Department> UpdateAsync(int id, Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
