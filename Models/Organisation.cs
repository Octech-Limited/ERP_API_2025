using System.ComponentModel.DataAnnotations;

namespace ErpApi.Models
{
    public partial class Organisation
    {
        [Key]
        public int Id { get; set; }
        public string? OrganisationName { get; set; }
        public string? OrganisationTin { get; set; }
        public string? OrganisationBRN { get; set; }
        public string? OrganisationNssfNo { get; set; }
        public string? DepartmentId { get; set; }
        public DateTime? OnboardedDate { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string? Status { get; set; }
        public DateTime? RequestTime { get; set; }

        public string? Enabled { get; set; }

        public string? AccountName { get; set; }

        public string? UniqueId { get; set; }

        public string? AccountNo { get; set; }

        public string? Cif { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? BankName { get; set; }
        public string? BankBranch { get; set; }
        public DateTime? Modified { get; set; }
        public string? ModifiedBy { get; set; }

        
    }
}
