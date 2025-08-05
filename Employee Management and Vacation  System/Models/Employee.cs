using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management_and_Vacation_System.Models
{
    public class Employee
    {
        public Employee() { }
        public Employee(string employeeNumber, string employeeName, int departmentId,
            int positionId, string genderCode, string ?reportedTo, decimal salary)
        {
            EmployeeNumber = employeeNumber;
            EmployeeName = employeeName;
            DepartmentId = departmentId;
            PositionId = positionId;
            GenderCode = genderCode;
            ReportedToEmployeeNumber = reportedTo;
            Salary = salary;
            VacationDaysLeft = 24; // Default value

            
        }
        [Key]
        [MaxLength(6)]
        public string EmployeeNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string EmployeeName { get; set; } = string.Empty;

        // Foreign Keys
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }

        [MaxLength(1)]
        public string GenderCode { get; set; } = string.Empty; // M or F

        [MaxLength(6)]
        public string? ReportedToEmployeeNumber { get; set; } // يمكن يكون فارغ

        [Range(0, 24)]
        public int VacationDaysLeft { get; set; } = 24;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        // علاقات
        public Department Department { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public Employee? ReportedTo  { get; set; }
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
        public ICollection<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>();
    }
}
