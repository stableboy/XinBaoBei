


-- 1.	创建数据库
if not exists(select name from sys.databases where name = 'Baby2')
begin
	create DataBase Baby2
end

GO

use Baby2

-- 2.	建表
-- 2.1	人员表
if not exists(select name from sys.objects where name = 'T_Position')
begin
	create table T_Position (

	ID bigint not null identity(1,1),
	Code varchar(100) ,
	Name varchar(100) ,
	Type varchar(100) ,
	Sex varchar(100) ,
	Level varchar(100) ,
	Address varchar(100) ,
	Photo image ,
	CreateDate datetime ,
	State bit ,
	Intro varchar(4000) ,
	Memo varchar(100) ,
	)
end

-- 2.2	目录表
if not exists(select name from sys.objects where name = 'T_Menu')
begin
	create table T_Menu (

	ID bigint not null identity(1,1),
	Code varchar(100) ,
	Name varchar(100) ,
	Level smallint ,
	UpLevel varchar(100) ,
	Path varchar(100) ,
	IfSee bit ,
	Memo varchar(100) ,
	ParentMenu bigint ,
	AgeGroup bigint ,
	AgeGroupName varchar(100) ,
	)
end


-- 2.3	问题表
if not exists(select name from sys.objects where name = 'T_Question')
begin
	create table T_Question (

	ID bigint not null identity(1,1),
	Code varchar(100) ,
	AboutAge varchar(100) ,
	Title varchar(100) ,
	KeyWords varchar(100) ,
	Questioner varchar(100) ,
	TheDate varchar(100) ,
	Memo varchar(100) ,
	AgeGroup bigint ,
	AgeGroupName varchar(100) ,
	Description varchar(4000) ,
	ParentMenu bigint ,
	ParentMenuName varchar(100) ,
	)

end

-- 2.4	答案表
if not exists(select name from sys.objects where name = 'T_Solution')
begin
	create table T_Solution (

	ID bigint not null identity(1,1),
	AnCode varchar(100) ,
	QuCode varchar(100) ,
	Intro varchar(100) ,
	Answer varchar(100) ,
	TheDate varchar(100) ,
	Memo varchar(100) ,
	SText varchar(4000) ,
	Content1 varchar(4000) ,
	Content2 varchar(4000) ,
	Content3 varchar(4000) ,
	Content4 varchar(4000) ,
	Question bigint ,
	)

end

-- 2.5	用户表
if not exists(select name from sys.objects where name = 'T_User')
begin
	create table T_User (

	ID bigint not null identity(1,1),
	Code varchar(100) ,
	Name varchar(100) ,
	PassWord varchar(100) ,
	Type varchar(100) ,
	Power varchar(100) ,
	IfUse bit ,
	Photo image ,
	Memo varchar(100) ,
	)

end

-- 2.6	生成问题单号的表
if not exists(select name from sys.objects where name = 'S_MaxId')
begin
	CREATE TABLE S_MaxId (	
	[keyId] [int] IDENTITY(1,1) NOT NULL,
	[release] [tinyint] NULL default 1,
	[entertime] [datetime] NOT NULL default getdate(),
	[MaxId] [varchar](50) NULL,
	PRIMARY KEY CLUSTERED 	
	(	
		[keyId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]	
	) ON [PRIMARY]	
		
end



if not exists(select 1 from T_User where Code = 'Admin')
begin
	insert into T_User (
		Code,Name,Type,PassWord,Power
	)values(
		-- 其中密码是密文的
		'Admin','Admin','管理','E10ADC3949BA59ABBE56E057F20F883E',3
	)

end


GO