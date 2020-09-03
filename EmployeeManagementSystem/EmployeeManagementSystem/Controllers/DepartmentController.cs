using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _dep;
       
        public DepartmentController(IDepartmentRepository dep)
        {
            _dep = dep;
        }

        // GET: Departments
        public IActionResult Index()
        {
            return View(_dep.SelectDepartments());
        }


        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        public IActionResult Create(Department department)
        {
            _dep.AddDepartment(department);
            return RedirectToAction("Index");
        }

        //GET: Departments/Edit/5
        public IActionResult Edit(int id)
        {
            Department department = _dep.getDepartmentById(id);
            return View(department);
        }

        ////POST: Departments/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Department department)
        {
            _dep.EditDepartment(id,department);
            return RedirectToAction("Index");
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int id)
        {
            Department department = _dep.getDepartmentById(id);
            return View(department);
        }

        ////// POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _dep.DeleteDepartment(id);
            return RedirectToAction("Index");
        }
    }
}
