using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _emp;
        private readonly IDepartmentRepository _dep;
        

        public EmployeeController(IEmployeeRepository employee,IDepartmentRepository department)
        {
            _emp = employee;
            _dep = department;
        }

        // GET: Employees
        public IActionResult Index()
        {
            ViewData["DeptName"] = new SelectList(_dep.SelectDepartments(), "DepartmentID", "Name");
            return View(_emp.SelectAllEmployees());
        }


        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewBag.DeptName = _dep.SelectDepartments();
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public IActionResult Create( Employee employee)
        {
            _emp.AddEmployee(employee);
            return RedirectToAction("Index");

        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.DeptName = _dep.SelectDepartments();
            Employee employee = _emp.getEmployeeById(id);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            try
            {
                _emp.EditEmployee(id,employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int id)
        {
            Employee employee = _emp.getEmployeeById(id);
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _emp.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}
