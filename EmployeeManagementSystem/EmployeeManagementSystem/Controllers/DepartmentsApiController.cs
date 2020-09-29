﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentsApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DepartmentsApi
        [HttpGet]
        [Authorize(Roles = "Admin, HR")]
        public async Task<ActionResult<IEnumerable<Department>>> Getdepartments()
        {
            return await _context.departments.ToListAsync();
        }

        // GET: api/DepartmentsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/DepartmentsApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentID)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DepartmentsApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _context.departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentID }, department);
        }

        // DELETE: api/DepartmentsApi/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.departments.Remove(department);
            await _context.SaveChangesAsync();

            return department;
        }

        private bool DepartmentExists(int id)
        {
            return _context.departments.Any(e => e.DepartmentID == id);
        }
    }
}
