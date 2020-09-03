using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public interface IDepartmentRepository
    {
        public List<Department> SelectDepartments();
        Department getDepartmentById(int id);
        void AddDepartment(Department department);
        void EditDepartment(int id,Department department);
        void DeleteDepartment(int id);
    }
}
