using kolo2.Data;
using kolo2.DTOs;
using kolo2.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace kolo2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly CustomDBContext _dbContext;

        public EmployeesController(CustomDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Employee FindEmployeeById(int id)
        {
            try
            {
                var emps = _dbContext.employees.Where(emp => emp.id == id);
                return emps.Any() ? emps.First() : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // GET /Employees
        [HttpGet]
        public ActionResult GetEmployees()
        {
            try
            {
                var employees = _dbContext.employees.ToList().Select(it => DTOFormatter.Convert(it));
                return StatusCode(200, employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetEmployee(int id)
        {
            EmployeeDTO emp = DTOFormatter.Convert(FindEmployeeById(id));
            return emp is null ? NotFound() : StatusCode(200, emp);
        }

        [HttpPost("add-employee")]
        public ActionResult<EmployeeDTO> AddEmployee([FromBody] SetEmployeeDTO emp)
        {
            Employee newEmp = new()
            {
                title_before_name = emp.title_before_name,
                first_name = emp.first_name,
                last_name = emp.last_name,
                title_after_name = emp.title_after_name,
                email = emp.email,
                phone = emp.phone
            };
            try
            {
                _dbContext.employees.Add(newEmp);
                _dbContext.SaveChanges();
                return StatusCode(200, DTOFormatter.Convert(newEmp));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("modify-employee/{id}")]
        public ActionResult UpdateEmployee(int id, [FromBody] SetEmployeeDTO memp)
        {
            Employee emp = FindEmployeeById(id);
            if (emp is null) return NotFound();
            emp.title_before_name = memp.title_before_name;
            emp.title_after_name = memp.title_after_name;
            emp.first_name = memp.first_name;
            emp.last_name = memp.last_name;
            emp.email = memp.email;
            emp.phone = memp.phone;
            try
            {
                _dbContext.employees.Update(emp);
                _dbContext.SaveChanges();
                return Ok(DTOFormatter.Convert(emp));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete("remove-employee/{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                Employee emp = FindEmployeeById(id);
                if (emp is null) return NotFound();
                _dbContext.employees.Remove(emp);
                _dbContext.SaveChanges();
                return Ok(DTOFormatter.Convert(emp));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
