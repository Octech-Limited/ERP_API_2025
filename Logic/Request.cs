namespace ErpApi.Logic
{
    public class Request
    {
       
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime RequestTime { get; set; }
    }

    public class RegisterOrgRequest
    {
        public string OrganisationName { get; set; }
        public string OrganisationTin { get; set; }
        public string OrganisationBRN { get; set; }
        public string OrganisationNssfNo { get; set; }
        public DateTime RequestTime { get; set; }
    }
    public class TokenValidationRequest
    {
        public string token { get; set; }
        public DateTime RequestTime { get; set; }
    }
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; } = 120;
    }
}
