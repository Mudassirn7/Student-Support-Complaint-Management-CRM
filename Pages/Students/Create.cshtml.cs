using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using CRMProject.Models;

namespace CRMProject.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _config;

        [BindProperty]
        public Student Student { get; set; }
        public Dictionary<int, string> Departments = new Dictionary<int, string>();

        public CreateModel(IConfiguration config)
        {
            _config = config;
        }

        // Loads departments for dropdown
        public void OnGet()
        {
            LoadDepartments();
        }

        // Inserts new student into database
        public IActionResult OnPost()
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = "INSERT INTO Students (FullName, Email, RollNumber, DepartmentId) VALUES (@FullName, @Email, @RollNumber, @DepartmentId)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FullName", Student.FullName);
                cmd.Parameters.AddWithValue("@Email", Student.Email);
                cmd.Parameters.AddWithValue("@RollNumber", Student.RollNumber);
                cmd.Parameters.AddWithValue("@DepartmentId", Student.DepartmentId);
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage("Index");
        }

        private void LoadDepartments()
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT DepartmentId, DepartmentName FROM Departments", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Departments.Add((int)reader["DepartmentId"], reader["DepartmentName"].ToString());
                }
            }
        }
    }
}