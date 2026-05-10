namespace CRMProject.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RollNumber { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}