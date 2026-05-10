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
## How to Run
1. Clone the repository
2. Open `database.sql` in SSMS and run it
3. Update connection string in `appsettings.json`
4. Run `dotnet run` in terminal
5. Open `http://localhost:5077`

## Project Structure

```
CRMProject/
├── Models/         → Student.cs, Complaint.cs
├── Pages/
│   ├── Students/   → Index, Create, Edit, Delete
│   └── Complaints/ → Index, Create, Edit, Delete
├── appsettings.json
└── Program.cs
```

## Course
BL3004 – Database Systems in Business | FAST-NUCES BSBA-6A
