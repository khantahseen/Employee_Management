using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        SqlConnection con;
        private List<Department> departmentList= new List<Department>();
        private Department dep;
        public DepartmentRepository()
        {
            string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EMDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Department", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dep = new Department();
                dep.DepartmentID= Convert.ToInt32(reader[0]);
                dep.Name = reader[1].ToString();
                departmentList.Add(dep);
            }
            con.Close();
        }
        
        public List<Department> SelectDepartments()
        {
            return departmentList;
        }

        public void AddDepartment(Department department)
        {
            con.Open();
            string query = "INSERT INTO Department(DepartmentID,Name) VALUES(@DepartmentID, @Name)";
            SqlCommand cmd = new SqlCommand(query, con);
            
            dep = new Department();
            department.DepartmentID = departmentList.Max(x => x.DepartmentID)+1;
            cmd.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
            cmd.Parameters.AddWithValue("@Name", department.Name);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void EditDepartment(int id,Department department)
        {
            con.Open();
            string query = "UPDATE Department SET Name = '"+department.Name+ "' WHERE DepartmentID = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void DeleteDepartment(int id)
        {
            con.Open();
            string query;
            SqlCommand cmd;
            query = "DELETE FROM Department WHERE DepartmentID= " + id;
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            query = "DELETE FROM Employee WHERE DepartmentID = " + id;
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Department getDepartmentById(int id)
        {
            return departmentList.Find(m => m.DepartmentID == id); 
        }
    }
}
 