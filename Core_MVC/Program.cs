using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MVC
{
	public class Program
	{

		/// <summary>
		/// Entrypoint of the ASP.NET Core App
		/// Confogure the Hosting Env. with required dependencies
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}
		/// <summary>
		/// Default Kestral Configuration
		/// Use The 'Startup' class
		/// 1. Load and read appsettings.json using 'IConfiguration' Contract Interface
		/// 2. Create a Map of depednencies using IServiceCollections Contract interface
		/// 3. Start Http Request Processing by loading and using Various Middlewares in HTTP Request pipeline
		///	The Middlewares will be loading using IApplicationBuilder Contract Interface
		///	The HTTP Request processing will be managed by using IWebHostENvironment Contract Interface
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
