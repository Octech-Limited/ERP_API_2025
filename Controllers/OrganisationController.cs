using ErpApi.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace ErpApi.Controllers
{
    //[ValidateAntiForgeryToken]
    [Route("api/[action]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly InternalLogic _internalLogic;
        private readonly JwtTokenService _jwtService;

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

            if (string.IsNullOrEmpty(_bearer_token))
            {
                response.StatusCode = 99;
                response.StatusDescription = "API User Not Provided";
                Logger.LogInformation(request.channel, "EarlyRepayment", "Response", request.memberId, resp.StatusCode + ":" + resp.StatusDescription, DateTime.Now.ToString(), resp);

            }
            if (request.Username == "admin" && request.Password == "12345")
            {
                response = _jwtService.GenerateJwtToken(request.Username, "Admin");

                if (string.IsNullOrEmpty(response.Token))
                {
                    response.StatusCode = 99;
                    response.StatusDescription = "Token generation failed";
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                }

                response.StatusCode = 0;
                response.StatusDescription = "Success";
                response.Token = response.Token;
                return Ok(response);
            }

            response.StatusCode = 401;
            response.StatusDescription = "Invalid username or password";
            return Unauthorized(response);
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
