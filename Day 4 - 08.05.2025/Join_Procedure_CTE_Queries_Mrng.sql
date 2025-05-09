  
  -- Out parameter example
  
  select * from products where 
  try_cast(json_value(details,'$.spec.cpu') as nvarchar(20)) ='i5'
  go
  create proc proc_FilterProducts(@pcpu varchar(20), @pcount int out)
  as
  begin
      set @pcount = (select count(*) from products where 
	  try_cast(json_value(details,'$.spec.cpu') as nvarchar(20)) =@pcpu)
  end
  go
 begin
  declare @cnt int
 exec proc_FilterProducts 'i5', @cnt out
  print concat('The number of computers is ',@cnt)
  end

  -- Bulk insert using external file

  
sp_help titles

create table people
(id int primary key,
name nvarchar(20),
age int)

-- Written by me
Create or alter Proc proc_InsertFromFile(@filepath nvarchar(max))
as
begin
	declare @insertQuery nvarchar(max)
	set @insertQuery = 'BULK INSERT people from '''+@filepath+''' 
						With (
						FirstRow= 2,
						FieldTerminator = '','',
						rowTerminator = ''\n'')'
	exec sp_executesql @insertQuery
end

proc_InsertFromFile 'C:\Users\srikanthm\Downloads\Data.csv'

select * from people

-- Written by Gayathri Mahadevan mam
create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
   declare @insertQuery nvarchar(max)

   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
   with(
   FIRSTROW =2,
   FIELDTERMINATOR='','',
   ROWTERMINATOR = ''\n'')'
   exec sp_executesql @insertQuery
end

proc_BulkInsert 'D:\Corp\GenSpark\Presidio\2025\Participants\Day3\Data.csv'

select * from people


-- Exception handling - Try/Catch and logging bulk insert

create table BulkInsertLog
(LogId int identity(1,1) primary key,
FilePath nvarchar(1000),
status nvarchar(50) constraint chk_status Check(status in('Success','Failed')),
Message nvarchar(1000),
InsertedOn DateTime default GetDate())


create or alter proc proc_BulkInsertwithExceptionHandling(@filepath nvarchar(500))
as
begin
  Begin try
	   declare @insertQuery nvarchar(max)

	   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
	   with(
	   FIRSTROW =2,
	   FIELDTERMINATOR='','',
	   ROWTERMINATOR = ''\n'')'

	   exec sp_executesql @insertQuery

	   insert into BulkInsertLog(filepath,status,message)
	   values(@filepath,'Success','Bulk insert completed')
  end try
  begin catch
		 insert into BulkInsertLog(filepath,status,message)
		 values(@filepath,'Failed',Error_Message())
  END Catch
end
go
proc_BulkInsertwithExceptionHandling 'C:\Users\srikanthm\Downloads\Data.csv'
go
select * from BulkInsertLog
go
select * from people

truncate table people

-- CTEs - Comman table expressions

with cteAuthors
as
(select au_id, concat(au_fname,' ',au_lname) author_name from authors)

select * from cteAuthors

-- Data Pagination with CTE (old method)
--create a sp that will take the page number and size as param and print the books

create or alter Proc proc_PaginateBook(@page int,@pageSize int)
as
begin
with PaginatedBooks as
( select  title_id,title, price, ROW_Number() over (order by price desc) as RowNum
  from titles)

select * from PaginatedBooks where rowNUm between((@page-1)*@pageSize+1) and (@page*@pageSize)
end

proc_PaginateBook 1, 10

-- Pagination using offset (newer method)
-- syntax 
--		offset {start} rows fetch next {no of rows} only


 select  title_id,title, price
  from titles
  order by price desc
  offset 10 rows fetch next 10 rows only

-- Functions
-- Scalar function
create function  fn_CalculateTax(@baseprice float, @tax float)
returns float
as
begin
    return (@baseprice +(@baseprice*@tax/100))
end

go

select dbo.fn_CalculateTax(1000,10)
go
select title,dbo.fn_CalculateTax(price,12) from titles

-- Table valued function(TVF) (newer way)
  create function fn_tableSample(@minprice float)
  returns table
  as
    return select title,price from titles where price>= @minprice

	select * from dbo.fn_tableSample(10)

-- older way of TVF 
-- slower but supports more logic
create function fn_tableSampleOld(@minprice float)
  returns @Result table(Book_Name nvarchar(100), price float)
  as
  begin
    insert into @Result select title,price from titles where price>= @minprice
    return 
end
select * from fn_tableSampleOld(10)