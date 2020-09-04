using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [StringLength(20)]
        [Required]
        public string Name { get; set; }

        [StringLength(20)]
        [Required]
        public string Surname { get; set; }

        [StringLength(100)]
        [Required]
        public string Address { get; set; }

        [Required]
        public string Qualification { get; set; }

        
        [Required]
        public long ContactNumber { get; set; }

        [Required]
        public int DepartmentID { get; set; }
        public string DName { get; set; }
        public Department department { get; set; }
    }
}
