using System.ComponentModel.DataAnnotations;

namespace Employee_Management_and_Vacation_System.Models
{
    public class VacationType
    {
        [Key]
        [MaxLength(1)]
        public char VacationTypeCode { get; set; } 

        [Required]
        [MaxLength(20)]
        public string VacationTypeName { get; set; } = string.Empty;

        // علاقة مع طلبات الإجازة
        public ICollection<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>();
    }
}
