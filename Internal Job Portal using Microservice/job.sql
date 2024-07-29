
drop table Job;
create table Job(
JobId char(4) Primary Key,
JobTitle varchar(50))

insert into Job values('1001','Manager')
insert into Job values('1002','HR')
insert into Job values('1003','DotNet Module Lead')
insert into Job values('1004','DotNet Team Lead')
insert into Job values('1005','Senior Software Developer')
insert into Job values('1006','Junior Software Developer')
insert into Job values('1007','Senior Software Tester')
insert into Job values('1008','Junior Software Tester')
insert into Job values('1009','English Trainer')
insert into Job values('1010','Help Desk')
insert into Job values('1011','COO')
insert into Job values('1012','CTO')


create table JobPost(
PostId int IDENTITY(3000,1) NOT NULL primary key,
JobId char(4) ,
PostDate DateTime Not null,
LastDate DateTime Not null,
Vacancies int,

Foreign Key(JobId) References Job(JobId))


drop table JobPost