using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using CRMProject.Models;

namespace CRMProject.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _config;

        [BindProperty]
        public Student Student { get; set; }

        public DeleteModel(IConfiguration config)
        {
            _config = config;
        }

        // Loads student info for confirmation
        public void OnGet(int id)
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE StudentId = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Student = new Student
                    {
                        StudentId = (int)reader["StudentId"],
                        FullName = reader["FullName"].ToString()
                    };
                }
            }
        }

        // Deletes student from database
        public IActionResult OnPost()
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE StudentId = @id", con);
                cmd.Parameters.AddWithValue("@id", Student.StudentId);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage("Index");
        }
    }
}