using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using CRMProject.Models;

namespace CRMProject.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _config;

        [BindProperty]
        public Student Student { get; set; }
        public Dictionary<int, string> Departments = new Dictionary<int, string>();

        public EditModel(IConfiguration config)
        {
            _config = config;
        }

        // Loads existing student data for editing
        public void OnGet(int id)
        {
            LoadDepartments();
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
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        RollNumber = reader["RollNumber"].ToString(),
                        DepartmentId = (int)reader["DepartmentId"]
                    };
                }
            }
        }

        // Updates student record in database
        public IActionResult OnPost()
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = "UPDATE Students SET FullName=@FullName, Email=@Email, RollNumber=@RollNumber, DepartmentId=@DepartmentId WHERE StudentId=@StudentId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FullName", Student.FullName);
                cmd.Parameters.AddWithValue("@Email", Student.Email);
                cmd.Parameters.AddWithValue("@RollNumber", Student.RollNumber);
                cmd.Parameters.AddWithValue("@DepartmentId", Student.DepartmentId);
                cmd.Parameters.AddWithValue("@StudentId", Student.StudentId);
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