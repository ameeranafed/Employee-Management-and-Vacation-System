using System.ComponentModel.DataAnnotations;

namespace Employee_Management_and_Vacation_System.Models
{
    public class RequestState
    {
        [Key]
        public int StateId { get; set; }

        [Required]
        [MaxLength(10)]
        public string StateName { get; set; } = string.Empty;

        // علاقة مع طلبات الإجازة
        public ICollection<VacationRequest> VacationRequests { get; set; } = new List<VacationRequest>();
    }
}
