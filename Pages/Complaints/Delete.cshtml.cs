using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using CRMProject.Models;

namespace CRMProject.Pages.Complaints
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _config;

        [BindProperty]
        public Complaint Complaint { get; set; }

        public DeleteModel(IConfiguration config)
        {
            _config = config;
        }

        // Loads complaint info for confirmation
        public void OnGet(int id)
        {
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
                        Title = reader["Title"].ToString()
                    };
                }
            }
        }

        // Deletes complaint from database
        public IActionResult OnPost()
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Complaints WHERE ComplaintId = @id", con);
                cmd.Parameters.AddWithValue("@id", Complaint.ComplaintId);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage("Index");
        }
    }
}