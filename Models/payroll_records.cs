using System.ComponentModel.DataAnnotations;

namespace ErpApi.Models
{

    public class payroll_records
    {
        [Key]
        public int Id { get; set; }
        public int CycleId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deductions { get; set; }
        public decimal? NetPay { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
