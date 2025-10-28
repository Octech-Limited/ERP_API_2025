using ErpApi.Logic;
using ErpApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ErpApi.Controllers
{
    //[ValidateAntiForgeryToken]
    [Route("api/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly InternalLogic _internalLogic;
        private readonly JwtTokenService _jwtService;
        private readonly LoggerService Logger;
        private readonly ErpContext _context;

        public EmployeeController(JwtTokenService jwtService)
        {
            _jwtService = jwtService;
        }
        [HttpPost]
        public IActionResult RegisterEmployee([FromBody] RegisterEmployeeRequest request)
        {
            var response = new RegisterEmployeeResponse();

            try
            {
                // ✅ Validate required fields
                if (string.IsNullOrEmpty(request.FirstName) || string.IsNullOrEmpty(request.LastName) || string.IsNullOrEmpty(request.Email))
                {
                    response.StatusCode = 99;
                    response.StatusDescription = "First name, last name, and email are required";
                    return Ok(response);
                }

                // ✅ Check for duplicate email or phone
                var exists = _context.employee.Any(e => e.Email == request.Email || e.PhoneNumber == request.PhoneNumber);
                if (exists)
                {
                    response.StatusCode = 99;
                    response.StatusDescription = "Employee already exists with this email or phone number";
                    return Ok(response);
                }

                // ✅ Create employee record
                var employee = new Employee
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    OtherNames = request.OtherNames,
                    Salutation = request.Salutation,
                    DepartmentName = request.DepartmentName,
                    OrganisationName = request.OrganisationName,
                    Role = request.Role,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    BankName = request.BankName,
                    BankBranch = request.BankBranch,
                    AccountNo = request.AccountNo,
                    AccountName = request.AccountName,
                    NextOfKinRelationship = request.NextOfKinRelationship,
                    NextOfKinName = request.NextOfKinName,
                    NextOfKinPhoneNumber = request.NextOfKinPhoneNumber,
                    NextOfKinEmail = request.NextOfKinEmail,
                    Status = "Active",
                    Created = DateTime.Now,
                    CreatedBy = request.InputterUsername
                };

                // ✅ Save to database
                _context.employee.Add(employee);
                _context.SaveChanges();

                response.StatusCode = 0;
                response.StatusDescription = "Employee registered successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = 99;
                response.StatusDescription = "Failed to register employee: " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetActiveEmployees()
        {
            var response = new
            {
                StatusCode = 0,
                StatusDescription = "",
                Data = new List<object>()
            };

            try
            {
                var employees = _context.employee
                    .Where(e => e.Status == "Active")
                    .Select(e => new
                    {
                        e.Id,
                        e.FirstName,
                        e.LastName,
                        e.OtherNames,
                        e.Salutation,
                        e.DepartmentName,
                        e.OrganisationName,
                        e.Role,
                        e.Email,
                        e.PhoneNumber,
                        e.BankName,
                        e.BankBranch,
                        e.AccountNo,
                        e.AccountName,
                        e.NextOfKinName,
                        e.NextOfKinRelationship,
                        e.NextOfKinPhoneNumber,
                        e.NextOfKinEmail,
                        e.Status
                    })
                    .ToList();

                return Ok(new
                {
                    StatusCode = 0,
                    StatusDescription = "Success",
                    Data = employees
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    StatusCode = 99,
                    StatusDescription = "Failed to retrieve employees: " + ex.Message,
                    Data = (object)null
                });
            }
        }

        [HttpGet]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var employee = _context.employee
                    .Where(e => e.Id == id)
                    .Select(e => new
                    {
                        e.Id,
                        e.FirstName,
                        e.LastName,
                        e.OtherNames,
                        e.Salutation,
                        e.DepartmentName,
                        e.OrganisationName,
                        e.Role,
                        e.Email,
                        e.PhoneNumber,
                        e.BankName,
                        e.BankBranch,
                        e.AccountNo,
                        e.AccountName,
                        e.NextOfKinName,
                        e.NextOfKinRelationship,
                        e.NextOfKinPhoneNumber,
                        e.NextOfKinEmail,
                        e.Status,
                        e.Created,
                        e.CreatedBy
                    })
                    .FirstOrDefault();

                if (employee == null)
                {
                    return Ok(new
                    {
                        StatusCode = 99,
                        StatusDescription = "Employee not found",
                        Data = (object)null
                    });
                }

                return Ok(new
                {
                    StatusCode = 0,
                    StatusDescription = "Success",
                    Data = employee
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    StatusCode = 99,
                    StatusDescription = "Failed to retrieve employee: " + ex.Message,
                    Data = (object)null
                });
            }
        }



    }
}
