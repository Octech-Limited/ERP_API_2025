using ErpApi.Logic;
using ErpApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ErpApi.Controllers
{
    //[ValidateAntiForgeryToken]
    [Route("api/[action]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly InternalLogic _internalLogic;
        private readonly JwtTokenService _jwtService;
        private readonly LoggerService Logger;
        private readonly ErpContext _context;

        public PayrollController(JwtTokenService jwtService)
        {
            _jwtService = jwtService;
        }
        [HttpPost]
        public IActionResult AddPayrollRecord([FromBody] PayrollRecord request)
        {
            try
            {
                var record = new payroll_records
                {
                    CycleId = request.CycleId,
                    EmployeeId = request.EmployeeId,
                    BasicSalary = request.BasicSalary,
                    Allowances = request.Allowances ?? 0,
                    Deductions = request.Deductions ?? 0,
                    NetPay = (request.BasicSalary + (request.Allowances ?? 0)) - (request.Deductions ?? 0),
                    Status = "Pending",
                    CreatedAt = DateTime.Now
                };

                _context.payroll_Records.Add(record);
                _context.SaveChanges();

                return Ok(new { StatusCode = 0, StatusDescription = "Payroll record added successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { StatusCode = 99, StatusDescription = "Failed to add payroll record: " + ex.Message });
            }
        }


        [HttpPut]
        public IActionResult ApprovePayrollCycle(int id, [FromBody] string approvedBy)
        {
            try
            {
                var cycle = _context.payroll_Cycles.FirstOrDefault(c => c.Id == id);
                if (cycle == null)
                    return Ok(new { StatusCode = 99, StatusDescription = "Payroll cycle not found" });

                cycle.Status = "Approved";
                cycle.ApprovedBy = approvedBy;
                cycle.ApprovedAt = DateTime.Now;

                _context.SaveChanges();

                return Ok(new { StatusCode = 0, StatusDescription = "Payroll cycle approved successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { StatusCode = 99, StatusDescription = "Failed to approve payroll cycle: " + ex.Message });
            }
        }


        [HttpPost]
        public IActionResult CreatePayrollCycle([FromBody] PayrollCycle request)
        {
            var response = new { StatusCode = 0, StatusDescription = "" };

            try
            {
                var cycle = new payroll_cycles
                {
                    CycleName = request.CycleName,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Status = "Pending",
                    CreatedBy = request.CreatedBy
                };

                _context.payroll_Cycles.Add(cycle);
                _context.SaveChanges();

                response = new { StatusCode = 0, StatusDescription = "Payroll cycle created successfully" };
            }
            catch (Exception ex)
            {
                response = new { StatusCode = 99, StatusDescription = "Failed to create payroll cycle: " + ex.Message };
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult GeneratePayslip([FromBody] GeneratePayslipRequest request)
        {
            try
            {
                var payslip = new payslips
                {
                    EmployeeId = request.EmployeeId,
                    CycleId = request.CycleId,
                    PayslipUrl = request.PayslipUrl, // This can be generated dynamically (PDF or file link)
                    GeneratedAt = DateTime.Now
                };

                _context.Payslips.Add(payslip);
                _context.SaveChanges();

                return Ok(new { StatusCode = 0, StatusDescription = "Payslip generated successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new { StatusCode = 99, StatusDescription = "Failed to generate payslip: " + ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetPayslip(int employeeId, int cycleId)
        {
            try
            {
                var payslip = _context.Payslips
                    .FirstOrDefault(p => p.EmployeeId == employeeId && p.CycleId == cycleId);

                if (payslip == null)
                    return Ok(new { StatusCode = 99, StatusDescription = "Payslip not found" });

                return Ok(new
                {
                    StatusCode = 0,
                    StatusDescription = "Success",
                    Data = payslip
                });
            }
            catch (Exception ex)
            {
                return Ok(new { StatusCode = 99, StatusDescription = "Failed to retrieve payslip: " + ex.Message });
            }
        }



    }
}
