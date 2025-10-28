using System.ComponentModel.DataAnnotations;

namespace ErpApi.Models
{
    public class payroll_cycles
    {
        [Key]
        public int Id { get; set; }
        public string CycleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
       

}
