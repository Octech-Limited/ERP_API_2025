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
        public DateTime OnboardedDate { get; set; }
        public string ApprovalStatus { get; set; }
        public string CreatedBy { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string phoneNumber { get; set; }
        public string InputterUsername { get; set; }
        public string email { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedOn { get; set; }
        public string Status { get; set; }
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
