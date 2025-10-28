using System.ComponentModel.DataAnnotations;

namespace ErpApi.Models
{
    public class payslips
    {
        [Key]
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int EmployeeId { get; set; }
        public int CycleId { get; set; }
        public string PayslipUrl { get; set; }
        public DateTime? GeneratedAt { get; set; }
    }
}
