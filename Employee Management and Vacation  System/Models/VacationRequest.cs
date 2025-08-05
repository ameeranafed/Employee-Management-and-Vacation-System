using System.ComponentModel.DataAnnotations;

namespace Employee_Management_and_Vacation_System.Models
{
    public class VacationRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public DateTime RequestSubmissionDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        // Foreign Keys
        [MaxLength(6)]
        public string EmployeeNumber { get; set; } = string.Empty;

        [MaxLength(1)]
        public char VacationTypeCode { get; set; } 

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public int TotalVacationDays { get; set; }

        public int RequestStateId { get; set; }

        [MaxLength(6)]
        public string? ApprovedByEmployeeNumber { get; set; }

        [MaxLength(6)]
        public string? DeclinedByEmployeeNumber { get; set; }

        // علاقات
        public Employee Employee { get; set; } = null!;
        public VacationType VacationType { get; set; } = null!;
        public RequestState RequestState { get; set; } = null!;
        public Employee? ApprovedByEmployee { get; set; }
        public Employee? DeclinedByEmployee { get; set; }
    }
}