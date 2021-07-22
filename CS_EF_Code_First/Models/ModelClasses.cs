using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CS_EF_Code_First.Models
{
	public class Department
	{
		[Key] // Primary Identity Key 
		public int DeptRowId { get; set; }
		[Required]
		[StringLength(50)]
		public string DeptNo { get; set; }
		[Required]
		[StringLength(200)]
		public string DeptName { get; set; }
		[Required]
		[StringLength(200)]
		public string Location { get; set; }
		// adding  a new scalar property
		[Required]
		public int Capacity { get; set; }

		public ICollection<Employee> Employees { get; set; } // One-to-Many Relationship
	}

	public class Employee
	{
		[Key]
		public int EmpRowId { get; set; }
		[Required]
		[StringLength(50)]
		public string EmpNo { get; set; }
		[Required]
		[StringLength(500)]
		public string EmpName { get; set; }
		[Required]
		public int Salary { get; set; }
		[Required]
		public int DeptRowId { get; set; } // Foreign Key
		public Department Department { get; set; } // Referencial INtegritity with one-to-one Relationship
	}
}
