using Dapper;
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
        //private List<Department> departmentList= new List<Department>();
        //private Department dep;
        public DepartmentRepository()
        {
            string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EMDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con = new SqlConnection(cs);
            con.Open();
        }
        
        public List<Department> SelectDepartments()
        {
            return con.Query<Department>("select * from Department").ToList();
            //return departmentList;
        }

        public void AddDepartment(Department department)
        {
            List<Department> getMaxId = con.Query<Department>("select * from Employee").ToList();
            string query = "INSERT INTO Department(DName) VALUES(@Name)";
            DynamicParameters parameters = new DynamicParameters();
            //parameters.Add("@DepartmentID", department.DepartmentID);
            parameters.Add("@Name", department.DName);
            con.Execute(query, parameters);
            con.Close();
        }
        public void EditDepartment(int id,Department department)
        {
            string query = "UPDATE Department SET DepartName = '" + department.DName + "' WHERE DepartmentID = " + id;
            con.Execute(query);
            con.Close();
        }
    
        public void DeleteDepartment(int id)
        {
            string query = "DELETE FROM Department WHERE DepartmentID = " + id;
            con.Execute(query);
            con.Close();
        }

        public Department getDepartmentById(int id)
        {
            List<Department> getMaxIdo= con.Query<Department>("select * from Department").ToList();
            return getMaxIdo.Find(m => m.DepartmentID == id);
            //return departmentList.Find(m => m.DepartmentID == id); 
        }
    }
}
 