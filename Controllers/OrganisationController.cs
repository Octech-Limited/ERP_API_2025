using ErpApi.Logic;
using ErpApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Xml;

namespace ErpApi.Controllers
{
    //[ValidateAntiForgeryToken]
    [Route("api/[action]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly InternalLogic _internalLogic;
        private readonly JwtTokenService _jwtService;
        private readonly LoggerService Logger;
        private readonly ErpContext _context;

        public OrganisationController(JwtTokenService jwtService)
        {
            _jwtService = jwtService;
        }


        // [HttpPost("RegisterOrganisation")]
        [HttpPost]
        public IActionResult RegisterOrganisation([FromBody] Logic.RegisterOrgRequest request)
        {
            string apiUser = Request.Headers["AUTH_USERNAME"];
            string _bearer_token = Request.Headers[HeaderNames.Authorization].ToString();
            var response = new Response();
            ValidateTokenResponse principal = new ValidateTokenResponse();
            Organisation org = new Organisation();

            if (string.IsNullOrEmpty(_bearer_token))
            {
                response.StatusCode = 99;
                response.StatusDescription = "API User Not Provided";
                Logger.LogInformation(request.OrganisationBRN, "RegisterOrganisation", "Response", request.OrganisationBRN, response.StatusCode + ":" + response.StatusDescription, DateTime.Now.ToString(), response);

            }
            else
            {
                principal = _jwtService.ValidateToken(_bearer_token);
                org.UniqueId = _internalLogic.uniqueCode();
                if (string.IsNullOrEmpty(org.UniqueId))
                {
                    response.StatusCode = 99;
                    response.StatusDescription = "Failed to generate uniqueCode";
                }
                else
                {
                    if (principal != null && principal.IsValid == true)
                    {
                        org.UniqueId = org.UniqueId;
                        org.Enabled = "0";
                        org.Email = request.email;
                        org.ApprovedOn = DateTime.Now;
                        org.AccountNo = request.AccountNo;
                        org.AccountName = request.AccountName;
                        org.ApprovalStatus = "PENDING";
                        org.Status = "Inactive";
                        org.BankBranch = request.BankBranch;
                        org.BankName = request.BankName;
                        org.PhoneNumber = request.phoneNumber;
                        org.Cif = request.AccountNo;
                        org.CreatedBy = request.InputterUsername;
                        org.Created = DateTime.Now;
                        org.DepartmentId = request.DepartmentId;
                        org.Modified = DateTime.Now;
                        org.ModifiedBy = request.InputterUsername;

                        try
                        {
                            _context.organisation.Add(org);
                            _context.SaveChanges();

                            response.StatusDescription = "Success";
                            response.StatusCode = 0;
                        }
                        catch (Exception ex)
                        {
                            response.StatusCode = 99;
                            response.StatusDescription = "Failed";
                        }
                    }
                }
            }

            return Ok(response);
        }


        [HttpGet]
        public IActionResult GetActiveOrganisations()
        {
            var response = new GetOrganisationResponse();

            try
            {
                var organisations = _context.organisation
                    .Where(x => x.Status == "Active")
                    .Select(x => new Datum
                    {
                        id = x.Id,
                        organisationName = x.OrganisationName,
                        organisationTin = x.OrganisationTin,
                        status = x.Status,
                        email = x.Email
                    })
                    .ToList();

                response.StatusCode = 0;
                response.StatusDescription = "Success";
                response.data = organisations;
            }
            catch (Exception ex)
            {
                response.StatusCode = 99;
                response.StatusDescription = "Failed to retrieve organisations";
                response.data = null;
            }

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetActiveDepartments()
        {
            var response = new GetDepartmentResponse();

            try
            {
                var departments = _context.department
                    .Where(d => d.Status == "Active")
                    .Select(d => new DepartmentDatum
                    {
                        id = d.Id,
                        departmentName = d.DepartmentName,
                        description = d.Description,
                        status = d.Status
                    })
                    .ToList();

                response.StatusCode = 0;
                response.StatusDescription = "Success";
                response.data = departments;
            }
            catch (Exception ex)
            {
                response.StatusCode = 99;
                response.StatusDescription = "Failed to retrieve departments";
                response.data = null;
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult RegisterDepartment([FromBody] RegisterDepartmentRequest request)
        {
            var response = new GetDepartmentResponse();

            try
            {
                if (string.IsNullOrEmpty(request.DepartmentName))
                {
                    response.StatusCode = 99;
                    response.StatusDescription = "Department name is required";
                    response.data = null;
                    return Ok(response);
                }

                // Optional: Check for duplicates
                var existing = _context.department
                                       .FirstOrDefault(d => d.DepartmentName == request.DepartmentName);
                if (existing != null)
                {
                    response.StatusCode = 99;
                    response.StatusDescription = "Department already exists";
                    response.data = null;
                    return Ok(response);
                }

                // Create department object
                var department = new Department
                {
                    DepartmentName = request.DepartmentName,
                    Description = request.Description,
                    Status = "Active",  // Default to Active
                    Created = DateTime.Now,
                    CreatedBy = request.CreatedBy
                };

                _context.department.Add(department);
                _context.SaveChanges();

                response.StatusCode = 0;
                response.StatusDescription = "Department successfully created";
                response.data = new List<DepartmentDatum>
        {
            new DepartmentDatum
            {
                id = department.Id,
                departmentName = department.DepartmentName,
                description = department.Description,
                status = department.Status
            }
        };
            }
            catch (Exception ex)
            {
                response.StatusCode = 99;
                response.StatusDescription = $"Failed to register department: {ex.Message}";
                response.data = null;
            }

            return Ok(response);
        }


    }
}
