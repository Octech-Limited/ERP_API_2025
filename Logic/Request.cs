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
    public class RegisterDepartmentRequest
    {
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }
    public class GeneratePayslipRequest
    {
        public int EmployeeId { get; set; }
        public int CycleId { get; set; }
        public string PayslipUrl { get; set; } 
        public string InputterUsername { get; set; }  
    }
    public class PayrollCycle
    {
        public string CycleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
    public class PayrollRecord
    {
        public int CycleId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deductions { get; set; }
        public decimal? NetPay { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
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
        public string DepartmentId { get; set; }
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
    public class RegisterEmployeeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string Salutation { get; set; }
        public string DepartmentName { get; set; }
        public string OrganisationName { get; set; }
        public string NIN { get; set; }
        public string TIN { get; set; }
        public string NSSF { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string NextOfKinRelationship { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinPhoneNumber { get; set; }
        public string NextOfKinEmail { get; set; }
        public string InputterUsername { get; set; }
    }

}
