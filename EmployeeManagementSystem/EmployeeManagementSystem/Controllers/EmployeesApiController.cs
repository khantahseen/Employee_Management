using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using EmployeeManagementSystem.Hubs;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IHubContext<NotificationHub> hubContext;

        public EmployeesApiController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.hubContext = hubContext;
        }

        // GET: api/EmployeesApi
        
        [HttpGet]
        [Authorize(Roles = "Admin,HR,Employee")]
        public async Task<ActionResult<IEnumerable<Employee>>> Getemployees()
        {
            var appDbContext = _context.employees.Include(e => e.department);
            return await appDbContext.ToListAsync();
        }

        // GET: api/EmployeesApi/5
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/EmployeesApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,HR,Employee")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                var group1 = "Admin";
                var group2 = "HR";
                await this.hubContext.Clients.Groups(group1, group2).SendAsync("employeeEdit", employee.Name + " " + employee.Surname + " edited their Profile");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/EmployeesApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Roles = "Admin,HR")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.employees.Add(employee);
            //await _context.SaveChangesAsync();
            string password = employee.Name.ToString() + "@ABC123";
            var uName = employee.Email;
            var uEmail = employee.Email;
            var user = new IdentityUser { UserName = uName, Email = uEmail };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Employee");
                await _context.SaveChangesAsync();
                var name = employee.Name;
                var surname = employee.Surname;
                var dept = _context.departments.Where(d => d.DepartmentID == employee.DepartmentID).First().Name;
                var grpName = "Employee" + dept;
                await this.hubContext.Clients.Group(grpName).SendAsync("employeeAdded", employee.Name + " " + employee.Surname + " was added as an Employee to your Department");
                //await this.hubContext.Clients.All.SendAsync("employeeAdded", employee.Name +" "+employee.Surname+ " was added as an Employee");
                //return RedirectToAction(nameof(Index));
            }

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/EmployeesApi/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByEmailAsync(employee.Email);
            await userManager.DeleteAsync(user);
            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.employees.Any(e => e.EmployeeId == id);
        }
    }
}
