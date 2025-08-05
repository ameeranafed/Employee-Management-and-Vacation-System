namespace Employee_Management_and_Vacation_System.Models
{
    public class VacationRequestDto
    {
        public string EmployeeNumber { get; set; }
        public char VacationTypeCode { get; set; }
        public string Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
