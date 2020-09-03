CREATE TABLE [dbo].[Employee]
(
	[EmployeeId] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [Surname] NVARCHAR(50) NULL, 
    [Address] NVARCHAR(50) NULL, 
    [Qualification] NVARCHAR(50) NULL, 
    [ContactNumber] NVARCHAR(50) NULL, 
    [DepartmentID] INT NOT NULL
)
