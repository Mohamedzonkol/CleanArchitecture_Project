create proc ViewDepartmentCountProc
@DID int
as
begin
create table #temp(DID int , DNameEn nvarchar(200),DNameAr nvarchar(200),StudentCount int)
insert into #temp
select d.DID,d.DNameEn,d.DNameAr,count(StudID)as StudentCount
from Departments d left join Students s on d.DID=s.DID
where d.DID=case when @DID=0 then d.DID else @DID end
group by d.DID,d.DNameEn,d.DNameAr
end
select * from #temp