using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using CRMProject.Models;

namespace CRMProject.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;
        public List<Student> Students = new List<Student>();

        public IndexModel(IConfiguration config)
        {
            _config = config;
        }

        // Loads all students from database
        public void OnGet()
        {
            string conn = _config.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string query = "SELECT s.StudentId, s.FullName, s.Email, s.RollNumber, d.DepartmentName FROM Students s JOIN Departments d ON s.DepartmentId = d.DepartmentId";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Students.Add(new Student
                    {
                        StudentId = (int)reader["StudentId"],
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        RollNumber = reader["RollNumber"].ToString(),
                        DepartmentName = reader["DepartmentName"].ToString()
                    });
                }
            }
        }
    }
}