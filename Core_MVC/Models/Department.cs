using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace Core_MVC.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }
        [Required(ErrorMessage ="DeptNo is Must")]
        [NumericNonNegative]
        public int DeptNo { get; set; }
        [Required(ErrorMessage = "DeptName is Must")]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "Location is Must")]
        public string Location { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
