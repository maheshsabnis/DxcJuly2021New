using System;
using CS_EF_Code_First.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace CS_EF_Code_First
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("The Code First EF Core 5");
			Console.WriteLine("Calling Stored Procedure");

			var ctx = new DxcCompanyDbContext();

			var departments = ctx.Departments.FromSqlRaw("GetDepartments");

			Console.WriteLine($"Data = {JsonSerializer.Serialize(departments)}");

			ctx.Dispose();
			ctx = new DxcCompanyDbContext();
			// Passing the Paraneter to Stoted Procedure
			var deptbylocation = ctx.Departments.FromSqlRaw("GetDepartmentsByLocation 'Pune' ");
			Console.WriteLine($"Data by Location = {JsonSerializer.Serialize(deptbylocation)}");
			ctx.Dispose();
			ctx = new DxcCompanyDbContext();
			var parameter = new SqlParameter("@Location", "Bangalore");
			var deptbyparameter = ctx.Departments.FromSqlRaw("GetDepartmentsByLocation @Location", parameter);
			Console.WriteLine($"Data by Location = {JsonSerializer.Serialize(deptbyparameter)}");


			Console.ReadLine();
		}
	}
}
