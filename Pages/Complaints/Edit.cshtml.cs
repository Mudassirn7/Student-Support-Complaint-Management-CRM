using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using CRMProject.Models;

namespace CRMProject.Pages.Complaints
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _config;

        [BindProperty]
        public Complaint Complaint { get; set; }
        public Dictionary<int, string> Students = new Dictionary<int, string>();
        public Dictionary<int, string> Categories = new Dictionary<int, string>();

        public EditModel(IConfiguration config)
        {
            _config = config;
        }

        // Loads complaint data for editing
        public void OnGet(int id)
        {
            LoadDropdowns();
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Complaints WHERE ComplaintId = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Complaint = new Complaint
                    {
                        ComplaintId = (int)reader["ComplaintId"],
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        Status = reader["Status"].ToString(),
                        StudentId = (int)reader["StudentId"],
                        CategoryId = (int)reader["CategoryId"]
                    };
                }
            }
        }

        // Updates complaint record in database
        public IActionResult OnPost()
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = "UPDATE Complaints SET Title=@Title, Description=@Description, Status=@Status, StudentId=@StudentId, CategoryId=@CategoryId WHERE ComplaintId=@ComplaintId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Title", Complaint.Title);
                cmd.Parameters.AddWithValue("@Description", Complaint.Description);
                cmd.Parameters.AddWithValue("@Status", Complaint.Status);
                cmd.Parameters.AddWithValue("@StudentId", Complaint.StudentId);
                cmd.Parameters.AddWithValue("@CategoryId", Complaint.CategoryId);
                cmd.Parameters.AddWithValue("@ComplaintId", Complaint.ComplaintId);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage("Index");
        }

        private void LoadDropdowns()
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT StudentId, FullName FROM Students", con);
                SqlDataReader r1 = cmd1.ExecuteReader();
                while (r1.Read())
                    Students.Add((int)r1["StudentId"], r1["FullName"].ToString());
                r1.Close();

                SqlCommand cmd2 = new SqlCommand("SELECT CategoryId, CategoryName FROM ComplaintCategories", con);
                SqlDataReader r2 = cmd2.ExecuteReader();
                while (r2.Read())
                    Categories.Add((int)r2["CategoryId"], r2["CategoryName"].ToString());
            }
        }
    }
}