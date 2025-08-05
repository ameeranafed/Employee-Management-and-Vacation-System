using System.ComponentModel.DataAnnotations;

namespace Employee_Management_and_Vacation_System.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        [MaxLength(30)]
        public string PositionName { get; set; } = string.Empty;

        // علاقة مع الموظفين
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
