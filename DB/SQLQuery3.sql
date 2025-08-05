USE [EmployeeVacationDB]
GO

INSERT INTO [dbo].[VacationRequests]
           ([RequestSubmissionDate]
           ,[Description]
           ,[EmployeeNumber]
           ,[VacationTypeCode]
           ,[StartDate]
           ,[EndDate]
           ,[TotalVacationDays]
           ,[RequestStateId]
           ,[ApprovedByEmployeeNumber]
           ,[DeclinedByEmployeeNumber])
     VALUES
           ('2025-06-01','Family event','EMP011','A','2025-06-10','2025-06-12',3,2,'EMP001',NULL),
('2025-06-05','Medical leave','EMP012','S','2025-06-15','2025-06-17',3,2,'EMP002',NULL),
('2025-06-07','Trip abroad','EMP013','A','2025-06-20','2025-06-30',11,1,NULL,NULL),
('2025-06-08','Personal reason','EMP014','A','2025-06-18','2025-06-20',3,3,NULL,'EMP001'),
('2025-06-10','Rest','EMP015','A','2025-06-22','2025-06-24',3,2,'EMP002',NULL),
('2025-06-12','Study leave','EMP016','S','2025-06-25','2025-06-27',3,1,NULL,NULL),
('2025-06-14','Family commitment','EMP017','A','2025-07-01','2025-07-03',3,2,'EMP001',NULL),
('2025-06-15','Medical operation','EMP018','S','2025-07-05','2025-07-10',6,1,NULL,NULL),
('2025-06-16','Wedding leave','EMP019','A','2025-07-08','2025-07-15',8,2,'EMP003',NULL),
('2025-06-18','Conference','EMP020','A','2025-07-12','2025-07-14',3,3,NULL,'EMP001'),
('2025-06-20','Urgent family travel','EMP021','A','2025-07-16','2025-07-20',5,2,'EMP001',NULL),
('2025-06-22','Short vacation','EMP022','A','2025-07-21','2025-07-23',3,1,NULL,NULL),
('2025-06-24','Sick leave','EMP023','S','2025-07-25','2025-07-28',4,2,'EMP001',NULL),
('2025-06-26','Study prep','EMP024','S','2025-07-29','2025-07-31',3,3,NULL,'EMP001'),
('2025-06-28','Weekend trip','EMP025','A','2025-08-01','2025-08-03',3,1,NULL,NULL)
GO


