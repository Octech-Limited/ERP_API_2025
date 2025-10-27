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


        [HttpPost("RegisterOrganisation")]
        public IActionResult Login([FromBody] Logic.RegisterOrgRequest request)
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


        [HttpPost("ValidateToken")]
        public IActionResult ValidateToken([FromBody] TokenValidationRequest request)
        {
            ValidateTokenResponse principal = new ValidateTokenResponse();
            if (request == null || string.IsNullOrEmpty(request.token))
            {
                return BadRequest("Token is required");
            }
            else
            {
                principal = _jwtService.ValidateToken(request.token);
                if (principal != null && principal.IsValid == true)
                {
                    principal.Identity = new System.Security.Principal.GenericIdentity("User"); //
                    principal.IsValid = true;
                    principal.StatusCode = 0;
                    principal.StatusDescription = "Token is valid.";
                    return Ok(principal);
                }
                else
                {
                    principal.Identity = new System.Security.Principal.GenericIdentity("User"); //
                    principal.IsValid = false;
                    principal.StatusCode = 99;
                    principal.StatusDescription = "Token is invalid or Expired";
                    return Ok(principal);
                }
            }

        }

    }
}
