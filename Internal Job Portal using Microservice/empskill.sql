drop table Skill
create table Skill(
SkillId char(4) Primary key,
SkillName varchar(15) Not null)




insert into Skill values('2001','Java')
insert into Skill values('2002','C#')
insert into Skill values('2003','DevOps')
insert into Skill values('2004','Azure Cloud')
insert into Skill values('2005','Datascience')
insert into Skill values('2006','Cyber security')


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



create table EmpSkill(
EmpId char(6)  ,
SkillId char(4) ,
SkillExperience decimal Not null,
Primary Key(EmpId,SkillId),
Foreign Key(EmpId) References Employee(EmpId),
Foreign Key(SkillId) References Skill(SkillId))





drop table EmpSkill