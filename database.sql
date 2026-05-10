-- =============================================
-- Student Support & Complaint Management CRM
-- Database Setup Script
-- FAST-NUCES | BL3004 | Muhammad Mudassir
-- =============================================

-- Create Database
CREATE DATABASE CRMProjectDB;
GO
USE CRMProjectDB;
GO

-- Departments Table
CREATE TABLE Departments (
    DepartmentId INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);
GO

-- Students Table
CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    RollNumber NVARCHAR(20) NOT NULL,
    DepartmentId INT FOREIGN KEY REFERENCES Departments(DepartmentId)
);
GO

-- ComplaintCategories Table
CREATE TABLE ComplaintCategories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL
);
GO

-- Complaints Table
CREATE TABLE Complaints (
    ComplaintId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(50) DEFAULT 'Pending',
    SubmittedDate DATETIME DEFAULT GETDATE(),
    StudentId INT FOREIGN KEY REFERENCES Students(StudentId),
    CategoryId INT FOREIGN KEY REFERENCES ComplaintCategories(CategoryId)
);
GO

-- Insert Departments
INSERT INTO Departments (DepartmentName) VALUES
('Computer Science'),
('Business Administration'),
('Electrical Engineering'),
('Mathematics');
GO

-- Insert Students
INSERT INTO Students (FullName, Email, RollNumber, DepartmentId) VALUES
('Ali Hassan', 'ali@fast.edu.pk', '23F-0001', 1),
('Sara Khan', 'sara@fast.edu.pk', '23F-0002', 2),
('Ahmed Raza', 'ahmed@fast.edu.pk', '23F-0003', 1),
('Hina Noor', 'hina@fast.edu.pk', '23F-0004', 3),
('Bilal Ahmed', 'bilal@fast.edu.pk', '23F-0005', 2),
('Ayesha Malik', 'ayesha@fast.edu.pk', '23F-0006', 4),
('Usman Tariq', 'usman@fast.edu.pk', '23F-0007', 1),
('Ahmed Raza', 'ahmed@fast.edu.pk', '23F-0008', 1),
('Hina Noor', 'hina@fast.edu.pk', '23F-0009', 3),
('Bilal Ahmed', 'bilal@fast.edu.pk', '23F-0010', 2),
('Ayesha Malik', 'ayesha@fast.edu.pk', '23F-0011', 4),
('Usman Tariq', 'usman@fast.edu.pk', '23F-0012', 1);
GO

-- Insert ComplaintCategories
INSERT INTO ComplaintCategories (CategoryName) VALUES
('Academic'),
('Hostel'),
('Fee'),
('IT Support'),
('IT Support'),
('Administration');
GO

-- Insert Complaints
INSERT INTO Complaints (Title, Description, Status, SubmittedDate, StudentId, CategoryId) VALUES
('Exam Issue', 'Result not updated', 'Resolved', '2026-05-09 16:17:33.480', 1, 1),
('Fee Challan', 'Duplicate fee charged', 'Pending', '2026-05-09 16:17:33.483', 2, 3),
('WiFi Issue', 'Internet is not working in Lab 2', 'Pending', '2026-05-09 19:09:18.363', 3, 4),
('Hostel Water Problem', 'No water supply in hostel block A', 'In Progress', '2026-05-09 19:09:18.363', 4, 2),
('LMS Login Error', 'Unable to login to LMS portal', 'Resolved', '2026-05-09 19:09:18.363', 5, 4),
('Fee Voucher Error', 'Fee voucher showing incorrect dues', 'Pending', '2026-05-09 19:09:18.363', 6, 3),
('Classroom Projector Issue', 'Projector not working in room 301', 'Pending', '2026-05-09 19:09:18.363', 7, 5),
('Exam Attendance Missing', 'Attendance not marked in final exam', 'Resolved', '2026-05-09 19:09:18.363', 3, 1);
GO