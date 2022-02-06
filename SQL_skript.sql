CREATE DATABASE kolo2;
GO

USE kolo2;
GO

CREATE TABLE employees (
	id int identity(1,1) not null,
	title_before_name nvarchar(12),
	title_after_name nvarchar(12),
	first_name nvarchar(48) not null,
	last_name nvarchar(48) not null,
	phone nvarchar(24),
	email nvarchar(128),
	constraint employee_pk primary key clustered(id)
);

CREATE TABLE nodes (
	id int identity(1,1) not null,
	node_name nvarchar(256) not null,
	node_type int not null check (node_type BETWEEN 1 AND 4),
	node_super int,
	node_boss int,
	constraint node_pk primary key clustered(id),
	constraint boss_fk foreign key(node_boss) references employees(id),
	constraint super_fk foreign key(node_super) references nodes(id)
);
GO

insert into employees (title_before_name, title_after_name, first_name, last_name, phone, email)
select 'Ing.', null, 'Mária', 'Slivková', '+421957164981', 'marry.slivka@azet.sk'
union all
select null, 'Ph.D.', 'Kevin', 'Stolièka', '+421913662519', 'kevin.sadni@gmail.com'
union all
select null, null, 'Šimon', 'Janoviè', '+421948251395', 'smnjanovic@gmail.com'
union all
select 'Bc.', null, 'Mariana', 'Baková', '+421923417484', 'kremnanohy@gmail.com'
union all
select 'Ing.', null, 'Zuzana', 'Pribrzdená', '+421918225101', 'brzdi-zuza@azet.sk'
union all
select 'Bc.', null, 'Olívia', 'Záborská', '+421949101005', 'zabolivia@centrum.sk'
union all
select 'Bc.', null, 'Romana', 'Pierková', '+421914793525', 'rompier@azet.sk'
union all
select 'RNDr.', null, 'Eva', 'Slobodová', '+421950354341', 'zuriva-civava@gmail.com'
union all
select 'Ing.', null, 'Michal', 'Striebro', '+421949574138', 'silvermike@gmail.com'
union all
select null, 'Ph.D.', 'Ronald', 'Metalista', '+421948074477', 'ronmetal@centrum.sk'
union all
select 'Ing.', null, 'Richard', 'Stolár', '+421913662520', 'kevin.sadni@gmail.com'
union all
select null, 'Ph.D.', 'Ivan', 'Klapaèka', '+421913662521', 'ivan.sklapni@azet.sk'
;

insert into nodes (node_name, node_type, node_boss)
select 'CompanyA', 1, 12 union all
select 'DivisionA', 2, 10 union all
select 'DivisionB', 2, 2 union all
select 'ProjectA', 3, 1 union all
select 'ProjectB', 3, 1 union all
select 'ProjectC', 3, 5 union all
select 'PartitionA', 4, 9 union all
select 'PartitionB', 4, 5 union all
select 'PartitionC', 4, 3 union all
select 'PartitionD', 4, 6 union all
select 'PartitionE', 4, 4 union all
select 'PartitionF', 4, 7;

update nodes set node_super = 1 where node_type = 2;
update nodes set node_super = 2 where id between 4 and 5;
update nodes set node_super = 3 where id = 6;
update nodes set node_super = 4 where id between 7 and 8;
update nodes set node_super = 5 where id between 9 and 10;
update nodes set node_super = 6 where id between 11 and 12;