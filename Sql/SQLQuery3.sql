create function GetSumSummantion()
returns decimal(18,2) as
begin
declare @salary decimal(18,2)
select @salary=sum(Salary) from Instructor
return @salary
end 

create function GetInstracyureDate()
returns @Instractors table (Id int,ENameAr nvarchar(200),ENameEn nvarchar(200))
as
begin
insert into @Instractors
select InsId,ENameAr,ENameEn from Instructor
return
end
select dbo.GetSumSummantion() as netSalary
select * from dbo.GetInstracyureDate() 