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
            SqlCommand cmd = new SqlCommand("select * from Employee inner join Department on Employee.DepartmentID = Department.DepartmentID", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employee = new Employee();
                employee.department = new Department();
                employee.EmployeeId= Convert.ToInt32(reader[0]);
                employee.Name = reader[1].ToString();
                employee.Surname = reader[2].ToString();
                employee.Address = reader[3].ToString();
                employee.Qualification = reader[4].ToString();
                employee.ContactNumber = long.Parse(Convert.ToString(reader[5]));
                employee.DepartmentID= Convert.ToInt32(reader[6]);
                employee.department.DepartmentID = Convert.ToInt32(reader[7]);
                employee.department.Name = reader[8].ToString();
                employeeList.Add(employee);
            }
            con.Close();
        }
       

        public Employee getEmployeeById(int id)
        {
            return employeeList.Find(m => m.EmployeeId == id);
        }

        public List<Employee> SelectAllEmployees()
        {
            return employeeList;
        }

        public void AddEmployee(Employee employee)
        {
            con.Open();
          
            string query = "INSERT INTO Employee(EmployeeId,Name,Surname,Address,Qualification,Contact_number,DepartmentId) VALUES(@EmployeeId,@Name,@Surname,@Address,@Qualification,@ContactNumber,@DepartmentID)";
            SqlCommand cmd = new SqlCommand(query, con);
            employee = new Employee();
            employee.EmployeeId = employeeList.Max(x => x.EmployeeId) + 1;
            cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Surname", employee.Surname);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@Qualification", employee.Qualification);
            cmd.Parameters.AddWithValue("@ContactNumber", employee.ContactNumber);
            cmd.Parameters.AddWithValue("@DepartmentID", employee.DepartmentID);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void EditEmployee(int id,Employee employee)
        {
            con.Open();
            string query = "UPDATE Employee SET Name = '" + employee.Name + "',Surname = '"+employee.Surname+"',Address = '"+employee.Address+ "',Qualification = '"+employee.Qualification+ "',ContactNumber = "+employee.ContactNumber+ ",DepartmentID = "+employee.DepartmentID+" WHERE Id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteEmployee(int id)
        {
            con.Open();
            string query = "DELETE FROM Employee WHERE Id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
