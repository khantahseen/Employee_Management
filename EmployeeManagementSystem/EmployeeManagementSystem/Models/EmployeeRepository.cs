using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        SqlConnection con;
        private List<Employee> employeeList = new List<Employee>();
        private Employee employee;
        public EmployeeRepository()
        {
            string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EMDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con = new SqlConnection(cs);
            con.Open();
        }
       

        public Employee getEmployeeById(int id)
        {
            //return employeeList.Find(m => m.EmployeeId == id);
            List<Employee> getId = con.Query<Employee>("Select * from Employee").ToList();
            return getId.Find(l => l.EmployeeId == id);
        }

        public List<Employee> SelectAllEmployees()
        {
            string query = "select * from Employee inner join Department on Employee.DepartmentID = Department.DepartmentID";
            return con.Query<Employee>(query).ToList();
            //return employeeList;
        }

        public void AddEmployee(Employee employee)
        {
            List<Employee> getMaxId = con.Query<Employee>("select * from Employee").ToList();
            string query = "INSERT INTO Employee(Name,Surname,Address,Qualification,ContactNumber,DepartmentID) VALUES(@Name,@Surname,@Address,@Qualification,@ContactNumber,@DepartmentID)";
            DynamicParameters parameters = new DynamicParameters();
            // parameters.Add("@Id", employee.Id);
            parameters.Add("@Name", employee.Name);
            parameters.Add("@Surname", employee.Surname);
            parameters.Add("@Address", employee.Address);
            parameters.Add("@Qualification", employee.Qualification);
            parameters.Add("@ContactNumber", employee.ContactNumber);
            parameters.Add("@DepartmentID", employee.DepartmentID);
            con.Execute(query, parameters);
            con.Close();
        }

        public void EditEmployee(int id,Employee employee)
        {
            
            string query = "UPDATE Employee SET Name = '" + employee.Name + "',Surname = '"+employee.Surname+"',Address = '"+employee.Address+ "',Qualification = '"+employee.Qualification+ "',ContactNumber = "+employee.ContactNumber+ ",DepartmentID = "+employee.DepartmentID+" WHERE Id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Execute(query);
            con.Close();
        }

        public void DeleteEmployee(int id)
        {
            //con.Open();
            string query = "DELETE FROM Employee WHERE EmployeeId = " + id;
            //SqlCommand cmd = new SqlCommand(query, con);
            con.Execute(query);
            con.Close();
        }
    }
}
