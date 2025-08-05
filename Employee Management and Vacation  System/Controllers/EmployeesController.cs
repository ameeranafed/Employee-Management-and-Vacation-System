using Employee_Management_and_Vacation_System.Data;
using Employee_Management_and_Vacation_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_and_Vacation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("SeedDepartments")]
        public async Task<IActionResult> SeedDepartments()
        {
            if (await _context.Departments.AnyAsync())
                return BadRequest("Departments already exist");

            var departments = new List<Department>
    {
        new Department { DepartmentName = "Human Resources" },
        new Department { DepartmentName = "Finance" },
        new Department { DepartmentName = "Information Technology" },
        new Department { DepartmentName = "Marketing" },
        new Department { DepartmentName = "Sales" },
        new Department { DepartmentName = "Operations" },
        new Department { DepartmentName = "Customer Service" },
        new Department { DepartmentName = "Research & Development" },
        new Department { DepartmentName = "Quality Assurance" },
        new Department { DepartmentName = "Administration" },
        new Department { DepartmentName = "Legal" },
        new Department { DepartmentName = "Procurement" },
        new Department { DepartmentName = "Logistics" },
        new Department { DepartmentName = "Public Relations" },
        new Department { DepartmentName = "Training" },
        new Department { DepartmentName = "Engineering" },
        new Department { DepartmentName = "Design" },
        new Department { DepartmentName = "Security" },
        new Department { DepartmentName = "Health & Safety" },
        new Department { DepartmentName = "Facilities Management" }
    };

            await _context.Departments.AddRangeAsync(departments);
            await _context.SaveChangesAsync();

            return Ok("20 departments added successfully");
        }
        [HttpPost("SeedPositions")]
        public async Task<IActionResult> SeedPositions()
        {
            if (await _context.Positions.AnyAsync())
                return BadRequest("Positions already exist");

            var positions = new List<Position>
    {
        new Position { PositionName = "Chief Executive Officer" },
        new Position { PositionName = "General Manager" },
        new Position { PositionName = "Department Manager" },
        new Position { PositionName = "Team Leader" },
        new Position { PositionName = "Senior Developer" },
        new Position { PositionName = "Software Engineer" },
        new Position { PositionName = "Financial Analyst" },
        new Position { PositionName = "HR Specialist" },
        new Position { PositionName = "Marketing Coordinator" },
        new Position { PositionName = "Sales Representative" },
        new Position { PositionName = "Operations Supervisor" },
        new Position { PositionName = "Customer Support" },
        new Position { PositionName = "Research Scientist" },
        new Position { PositionName = "Quality Engineer" },
        new Position { PositionName = "Administrative Assistant" },
        new Position { PositionName = "Legal Advisor" },
        new Position { PositionName = "Procurement Officer" },
        new Position { PositionName = "Logistics Coordinator" },
        new Position { PositionName = "Graphic Designer" },
        new Position { PositionName = "IT Support Technician" }
    };

            await _context.Positions.AddRangeAsync(positions);
            await _context.SaveChangesAsync();

            return Ok("20 positions added successfully");
        }

        [HttpPost("SeedEmployees")]
        public async Task<IActionResult> SeedEmployees()
        {
            try { 
            if (await _context.Employees.AnyAsync())
                return BadRequest("Employees already exist");

            var employees = new List<Employee>
    {
        new Employee("MGR001", "Ahmed Mohammed", 1, 2, "M", null, 15000.00m),
        new Employee("MGR002", "Fatima Ali", 2, 2, "F", null, 14500.00m),
        new Employee("EMP001", "Yousef Khalid", 1, 6, "M", "MGR001", 8500.00m),
        new Employee("EMP002", "Layla Hassan", 1, 7, "F", "MGR001", 8200.00m),
        new Employee("EMP003", "Omar Ibrahim", 3, 4, "M", "MGR001", 10500.00m),
        new Employee("EMP004", "Aisha Salem", 3, 5, "F", "EMP003", 7500.00m),
        new Employee("EMP005", "Khalid Nasr", 2, 6, "M", "MGR002", 8800.00m),
        new Employee("EMP006", "Huda Mahmoud", 4, 8, "F", "MGR002", 9200.00m),
        new Employee("EMP007", "Ziad Farouk", 5, 9, "M", "MGR002", 9500.00m),
        new Employee("EMP008", "Noor Abdullah", 5, 10, "F", "EMP007", 6800.00m)
    };

            await _context.Employees.AddRangeAsync(employees);
            await _context.SaveChangesAsync();

            return Ok("10 employees added successfully");
        }
        catch (Exception ex)
    {
        return StatusCode(500, $"Error: {ex.Message}");
    }
}

        [HttpPut("UpdateEmployee/{empNumber}")]
        public async Task<IActionResult> UpdateEmployee(string empNumber, [FromBody] EmployeeUpdateDto dto)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeNumber == empNumber);

            if (employee == null)
                return NotFound("Employee not found");

            employee.EmployeeName = dto.Name;
            employee.DepartmentId = dto.DepartmentId;
            employee.PositionId = dto.PositionId;
            employee.Salary = dto.Salary;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPut("UpdateVacationDays/{empNumber}")]
        public async Task<IActionResult> UpdateVacationDays(string empNumber, [FromBody] int daysTaken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeNumber == empNumber);

            if (employee == null)
                return NotFound("Employee not found");

            if (daysTaken <= 0)
                return BadRequest("Days taken must be positive");

            if (employee.VacationDaysLeft < daysTaken)
                return BadRequest("Not enough vacation days left");

            employee.VacationDaysLeft -= daysTaken;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Vacation days updated successfully",
                Employee = employee.EmployeeName,
                RemainingDays = employee.VacationDaysLeft
            });
        }
        [HttpPost("SubmitVacationRequest")]
        public async Task<IActionResult> SubmitVacationRequest([FromBody] VacationRequestDto requestDto)
        {
            // التحقق من وجود الموظف
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeNumber == requestDto.EmployeeNumber);

            if (employee == null)
                return NotFound("Employee not found");

            // التحقق من عدم تداخل التواريخ
            bool hasOverlap = await _context.VacationRequests
                .AnyAsync(vr => vr.EmployeeNumber == requestDto.EmployeeNumber &&
                               vr.StartDate <= requestDto.EndDate &&
                               vr.EndDate >= requestDto.StartDate &&
                               vr.RequestStateId != 3); // 3 = مرفوض

            if (hasOverlap)
                return BadRequest("Vacation request overlaps with existing approved/pending requests");

            // حساب عدد أيام الإجازة
            int totalDays = (requestDto.EndDate.DayNumber - requestDto.StartDate.DayNumber) + 1;

            // إنشاء طلب الإجازة
            var vacationRequest = new VacationRequest
            {
                EmployeeNumber = requestDto.EmployeeNumber,
                VacationTypeCode = requestDto.VacationTypeCode,
                Description = requestDto.Description,
                StartDate = requestDto.StartDate,
                EndDate = requestDto.EndDate,
                TotalVacationDays = totalDays,
                RequestStateId = 1, // 1 = مقدم
                RequestSubmissionDate = DateTime.Now
            };

            await _context.VacationRequests.AddAsync(vacationRequest);
            await _context.SaveChangesAsync();

            return Ok(vacationRequest);
        }

        [HttpGet("GetPendingRequests/{empNumber}")]
        public async Task<IActionResult> GetPendingRequests(string empNumber)
        {
            var requests = await _context.VacationRequests
                .Where(vr => vr.Employee.ReportedToEmployeeNumber == empNumber &&
                            vr.RequestStateId == 1) // 1 = مقدم
                .Select(vr => new
                {
                    vr.RequestId,
                    vr.Employee.EmployeeName,
                    vr.VacationType.VacationTypeName,
                    vr.Description,
                    vr.StartDate,
                    vr.EndDate,
                    vr.TotalVacationDays
                })
                .ToListAsync();

            return Ok(requests);
        }
        [HttpPut("ApproveRequest/{requestId}/{approverEmpNumber}")]
        public async Task<IActionResult> ApproveRequest(int requestId, string approverEmpNumber)
        {
            var request = await _context.VacationRequests
                .Include(vr => vr.Employee)
                .FirstOrDefaultAsync(vr => vr.RequestId == requestId);

            if (request == null)
                return NotFound("Request not found");

            // التحقق من أن الموافق هو المدير المباشر
            if (request.Employee.ReportedToEmployeeNumber != approverEmpNumber)
                return Forbid();

            // تحديث حالة الطلب
            request.RequestStateId = 2; // 2 = معتمد
            request.ApprovedByEmployeeNumber = approverEmpNumber;
            request.DeclinedByEmployeeNumber = null;

            // خصم أيام الإجازة من رصيد الموظف
            request.Employee.VacationDaysLeft -= request.TotalVacationDays;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Request approved successfully",
                RemainingVacationDays = request.Employee.VacationDaysLeft
            });
        }
        [HttpPut("DeclineRequest/{requestId}/{declinerEmpNumber}")]
        public async Task<IActionResult> DeclineRequest(int requestId, string declinerEmpNumber)
        {
            var request = await _context.VacationRequests
                .Include(vr => vr.Employee)
                .FirstOrDefaultAsync(vr => vr.RequestId == requestId);

            if (request == null)
                return NotFound("Request not found");

            // التحقق من أن المرفوض هو المدير المباشر
            if (request.Employee.ReportedToEmployeeNumber != declinerEmpNumber)
                return Forbid();

            // تحديث حالة الطلب
            request.RequestStateId = 3; // 3 = مرفوض
            request.DeclinedByEmployeeNumber = declinerEmpNumber;
            request.ApprovedByEmployeeNumber = null;

            await _context.SaveChangesAsync();

            return Ok("Request declined successfully");
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Select(e => new
                {
                    e.EmployeeNumber,
                    e.EmployeeName,
                    Department = e.Department.DepartmentName,
                    e.Salary
                })
                .AsNoTracking() // تحسين الأداء للقراءة فقط
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("GetEmployeeDetails/{empNumber}")]
        public async Task<IActionResult> GetEmployeeDetails(string empNumber)
        {
            var employee = await _context.Employees
                .Where(e => e.EmployeeNumber == empNumber)
                .Select(e => new
                {
                    e.EmployeeNumber,
                    e.EmployeeName,
                    Department = e.Department.DepartmentName,
                    Position = e.Position.PositionName,
                    ReportedTo = e.ReportedTo.EmployeeName ?? "No Manager",
                    e.VacationDaysLeft
                })
                .FirstOrDefaultAsync();

            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpGet("GetEmployeesWithPendingRequests")]
        public async Task<IActionResult> GetEmployeesWithPendingRequests()
        {
            var employees = await _context.Employees
                .Where(e => e.VacationRequests.Any(vr => vr.RequestStateId == 1)) // 1 = Submitted
                .Select(e => new
                {
                    e.EmployeeNumber,
                    e.EmployeeName,
                    PendingRequests = e.VacationRequests
                        .Where(vr => vr.RequestStateId == 1)
                        .Count()
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("GetApprovedVacations/{empNumber}")]
        public async Task<IActionResult> GetApprovedVacations(string empNumber)
        {
            var vacations = await _context.VacationRequests
                .Where(vr => vr.EmployeeNumber == empNumber && vr.RequestStateId == 2) // 2 = Approved
                .Select(vr => new
                {
                    Type = vr.VacationType.VacationTypeName,
                    vr.Description,
                    Duration = $"{vr.TotalVacationDays} days",
                    Period = $"{vr.StartDate:yyyy-MM-dd} to {vr.EndDate:yyyy-MM-dd}",
                    ApprovedBy = vr.ApprovedByEmployee.EmployeeName
                })
                .ToListAsync();

            return Ok(vacations);
        }

        [HttpGet("GetPendingRequestsForApproval/{managerNumber}")]
        public async Task<IActionResult> GetPendingRequestsForApproval(string managerNumber)
        {
            var requests = await _context.VacationRequests
                .Where(vr => vr.Employee.ReportedToEmployeeNumber == managerNumber &&
                            vr.RequestStateId == 1) // 1 = Submitted
                .Select(vr => new
                {
                    vr.Description,
                    EmployeeNumber = vr.Employee.EmployeeNumber,
                    EmployeeName = vr.Employee.EmployeeName,
                    SubmittedDate = vr.RequestSubmissionDate.ToString("yyyy-MM-dd HH:mm"),
                    Duration = vr.TotalVacationDays > 7
                ? $"{vr.TotalVacationDays / 7} weeks and {vr.TotalVacationDays % 7} days"
                : $"{vr.TotalVacationDays} days",
                    Period = $"{vr.StartDate:yyyy-MM-dd} to {vr.EndDate:yyyy-MM-dd}",
                    vr.Employee.Salary
                })
                .ToListAsync();

            return Ok(requests);
        }


    }
    public class EmployeeUpdateDto
        {
            public string Name { get; set; }
            public int DepartmentId { get; set; }
            public int PositionId { get; set; }
            public decimal Salary { get; set; }
        }

}
