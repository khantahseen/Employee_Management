using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public interface IEmployeeRepository
    {
        public List<Employee> SelectAllEmployees();
        Employee getEmployeeById(int id);
        void AddEmployee(Employee employee);
        void EditEmployee(int id,Employee employee);
        void DeleteEmployee(int id);
    }
}
