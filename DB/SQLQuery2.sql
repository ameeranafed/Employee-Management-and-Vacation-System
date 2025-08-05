USE [EmployeeVacationDB]
GO

INSERT INTO [dbo].[Employees]
           ([EmployeeNumber]
           ,[EmployeeName]
           ,[DepartmentId]
           ,[PositionId]
           ,[GenderCode]
           ,[ReportedToEmployeeNumber]
           ,[VacationDaysLeft]
           ,[Salary])
     VALUES
           ('EMP011','Alaa Hassan',1,1,'F','EMP001',21,1500.00),
('EMP012','Sami Kanaan',2,3,'M','EMP002',16,2200.00),
('EMP013','Leen Othman',3,4,'F','EMP001',24,1950.00),
('EMP014','Tariq Maher',1,2,'M','EMP001',14,2100.00),
('EMP015','Sara Qassem',2,3,'F','EMP002',10,1800.00),
('EMP016','Omar Barakat',3,4,'M','EMP001',22,2050.00),
('EMP017','Nada Yaseen',1,1,'F','EMP001',13,1600.00),
('EMP018','Hadi Yousef',2,2,'M','EMP002',12,1700.00),
('EMP019','Dina Naji',3,3,'F','EMP003',24,2300.00),
('EMP020','Yousef Fadel',1,1,'M','EMP001',19,1500.00),
('EMP021','Aseel Marwan',1,3,'F','EMP002',18,2100.00),
('EMP022','Adnan Sameer',2,2,'M','EMP002',22,1750.00),
('EMP023','Mona Khaled',3,4,'F','EMP001',20,2400.00),
('EMP024','Rami Saeed',1,2,'M','EMP001',7,1650.00),
('EMP025','Yara Adel',2,3,'F','EMP002',17,1900.00)
GO


