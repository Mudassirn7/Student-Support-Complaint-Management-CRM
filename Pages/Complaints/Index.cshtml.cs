using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using CRMProject.Models;

namespace CRMProject.Pages.Complaints
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        public List<Complaint> Complaints = new List<Complaint>();

        public IndexModel(IConfiguration config)
        {
            _config = config;
        }

        // Loads all complaints with student and category names
        public void OnGet()
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = @"SELECT c.ComplaintId, c.Title, c.Status, s.FullName AS StudentName, cc.CategoryName
                                 FROM Complaints c
                                 JOIN Students s ON c.StudentId = s.StudentId
                                 JOIN ComplaintCategories cc ON c.CategoryId = cc.CategoryId";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Complaints.Add(new Complaint
                    {
                        ComplaintId = (int)reader["ComplaintId"],
                        Title = reader["Title"].ToString(),
                        Status = reader["Status"].ToString(),
                        StudentName = reader["StudentName"].ToString(),
                        CategoryName = reader["CategoryName"].ToString()
                    });
                }
            }
        }
    }
}