drop table ApplyJob

drop table Employee

create table Employee(
EmpId char(6) Primary Key,
EmpName varchar(20))

insert into Employee values('ZE5001','Rajesh')
insert into Employee values('ZE5002','Suresh')
insert into Employee values('ZE5003','Anitha')
insert into Employee values('ZE5004','Partiban')
insert into Employee values('ZE5005','Chitra')
insert into Employee values('ZE5006','Nazeem')
insert into Employee values('ZE5007','Logesh')
insert into Employee values('ZE5008','Selvi')
insert into Employee values('ZE5009','Sanjay')
insert into Employee values('ZE5010','Priya')
insert into Employee values('ZE5011','Ranjith')
insert into Employee values('ZE5012','Rajkumar')
insert into Employee values('ZE5013','Manikandan')
insert into Employee values('ZE5014','Aravind')


create table ApplyJob(
PostId int ,
EmpId char(6) ,
AppliedDate DateTime Not null,
ApplicationStatus varchar(15),
Primary Key(PostId,EmpId),
Foreign Key(PostId) References JobPost(PostId),
Foreign Key(EmpId) References Employee(EmpId))