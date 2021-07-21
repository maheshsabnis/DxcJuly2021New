using System;
using System.Threading.Tasks;
using CS_EF_DbFirst.Models;
using CS_EF_DbFirst.Services;
using System.Text.Json;
namespace CS_EF_DbFirst
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("EF Core  Database First");
			try
			{
				CitusTrainingContext ctx = new CitusTrainingContext();
				IService<Category, int> catServ = new CategoryService(ctx);
				Category cat = new Category()
				{
					CategoryRowId = 23,
				//	CategoryId = "Cat-001",
					//CategoryName = "Electronics",
					BasePrice = 8500,
					//SubCategoryName = "Home Appliances"
				};
				//var result = await catServ.CreateAsync(cat);
				//Console.WriteLine($"New Category Created {JsonSerializer.Serialize(result)}");

				var result = await catServ.UpdateAsync(23,cat);
				Console.WriteLine($"Updated Category {JsonSerializer.Serialize(result)}");

				var categories = await catServ.GetAsync();
				Console.WriteLine($"All Categories {JsonSerializer.Serialize(categories)}");
			}
			catch (Exception ex)
			{

				Console.WriteLine($"{ex.Message} {ex.InnerException}");
			}

			Console.ReadLine();
		}
	}
}
