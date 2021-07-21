using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_EF_DbFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CS_EF_DbFirst.Services
{
	public class CategoryService : IService<Category, int>
	{
		private readonly CitusTrainingContext context;

		public CategoryService(CitusTrainingContext context)
		{
			this.context = context;
		}
		public async Task<Category> CreateAsync(Category entity)
		{
			try
			{
				var result = await context.Categories.AddAsync(entity);
				await context.SaveChangesAsync();
				return result.Entity; // return a newly created entity
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<Category> DeleteAsync(int id)
		{
			try
			{
				var result = await context.Categories.FindAsync(id);
				if (result == null) throw new Exception("Delete failed because the Record not found");
				context.Categories.Remove(result);
				await context.SaveChangesAsync();
				return result;
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public async Task<IEnumerable<Category>> GetAsync()
		{
			return await context.Categories.ToListAsync();
		}

		public async Task<Category> GetAsync(int id)
		{
			try
			{
				var result = await context.Categories.FindAsync(id);
				if (result == null) throw new Exception("Read failed because the Record not found");
				return result;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<Category> UpdateAsync(int id, Category entity)
		{
			try
			{
				var result = await context.Categories.FindAsync(id);
				if (result == null) throw new Exception("Update failed because the Record not found");
				// detach the entity record from the cursor
				//context.Entry(result).State = EntityState.Detached;
				// Modify the record
				///context.Update<Category>(entity);
				///

				result.CategoryName = entity.CategoryName;
				result.BasePrice = entity.BasePrice;
				result.SubCategoryName = entity.SubCategoryName;

				await context.SaveChangesAsync();
				return entity;
			}
			catch (Exception ex)
			{

				throw;
			}
		}
	}
}
