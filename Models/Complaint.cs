namespace CRMProject.Models
{
    public class Complaint
    {
        public int ComplaintId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}