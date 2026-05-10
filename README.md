# Student Support & Complaint Management CRM

A web-based CRM system built with ASP.NET Core Razor Pages and SQL Server.

## Technologies Used
- ASP.NET Core Razor Pages (C#)
- SQL Server & ADO.NET
- Bootstrap 5
- Microsoft.Data.SqlClient

## Modules
- **Students Module** — Add, List, Edit, Delete students
- **Complaints Module** — Add, List, Edit, Delete complaints

## Database Tables
- Departments
- Students
- ComplaintCategories
- Complaints

## How to Run
1. Create database `CRMProjectDB` in SQL Server
2. Run SQL scripts from `Database/` folder
3. Update connection string in `appsettings.json`
4. Run `dotnet run` in terminal
5. Open `http://localhost:5077`

## Project Structure
CRMProject/
├── Models/         → Student.cs, Complaint.cs
├── Pages/
│   ├── Students/   → Index, Create, Edit, Delete
│   └── Complaints/ → Index, Create, Edit, Delete
├── appsettings.json
└── Program.cs
