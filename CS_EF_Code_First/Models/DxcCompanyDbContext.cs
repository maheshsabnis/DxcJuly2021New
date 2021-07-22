using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_EF_Code_First.Models
{
	public class DxcCompanyDbContext : DbContext
	{
		public DxcCompanyDbContext()
		{

		}

		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }

		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Patient> Patients { get; set; }


		public DbSet<ProductionUnit> ProductionUnit { get; set; }
		public DbSet<Movies> Movies { get; set; }
		public DbSet<WebSeries> WebSeries { get; set; }

		/// <summary>
		/// Define Conectio String to Database
		/// </summary>
		/// <param name="optionsBuilder"></param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DxcCompany;Integrated Security=SSPI");
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Define One-to-Many Relationship across Department-to-Employee
			modelBuilder.Entity<Employee>()
						.HasOne(e => e.Department) // Emp has One Dept, Set the DeptRowId from EMployee class as Foreign Key
						.WithMany(e => e.Employees) // One Department have multiple EMployees
						.HasForeignKey(e => e.DeptRowId);  // the foeign key 		

			// Define Many-To-Many Relationship Across Doctors to Patients
			// Relationships will start from the FIrst Class e.g. Doctor in this case
			modelBuilder.Entity<Doctor>()
						.HasMany(d => d.Patients) // Navigating from Doctors to Patients
						.WithMany(p => p.Doctors) // Navigating from Patients to Doctors
						.UsingEntity(relation=>relation.ToTable("DoctorsPatients")); // Generate new table with FLexible Mapping

			// Define a Sample Seed Data (Seed Data is Default Data)
			var movies = new Movies[]
			{
				new Movies(){Id=1,Name="Dr.No", ReleaseYear=1963, Category="Spy", Collection=300000, Duration=120 },
				new Movies(){Id=2,Name="Golmal", ReleaseYear=1976, Category="Comedy", Collection=60000, Duration=180 }
			};

			var webSeries = new WebSeries[] { 
			   new WebSeries() {Id=3, Name="Ramayan", ReleaseYear=1986, NoOfSeasons=2,EpisodsPerSeason=70 },
				new WebSeries() {Id=4, Name="CID", ReleaseYear=1998, NoOfSeasons=50,EpisodsPerSeason=70 }
			};

			// define a unition
			// ProductionUnit has Movies and WebSeries, so the Movies can be casted to ProductionUnit and Uniion with the WebSeries
			// The Union will contains Null values for Movie row for properties from WebSeries and vice-versa
			var productionUnit = movies.Cast<ProductionUnit>().Union(webSeries).ToList();

			// link the data with entities so that when the table is table is generated it will have the defauilrt data
			modelBuilder.Entity<Movies>().HasData(movies);
			modelBuilder.Entity<WebSeries>().HasData(webSeries);


			base.OnModelCreating(modelBuilder);
		}
	}
}
