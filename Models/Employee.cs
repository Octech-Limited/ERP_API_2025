using System.ComponentModel.DataAnnotations;

namespace ErpApi.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string Salutation { get; set; }
        public string DepartmentName { get; set; }
        public string OrganisationName { get; set; }
        public string Role { get; set; }
        public string NIN { get; set; }
        public string TIN { get; set; }
        public string NSSF { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string NextOfKinRelationship { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinPhoneNumber { get; set; }
        public string NextOfKinEmail { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
