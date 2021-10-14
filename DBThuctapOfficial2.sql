create database THUCTAPNHOMDB2

create table PRODUCT_GROUP(
	group_id int primary key identity(1,1),
	group_name nvarchar(50)
)

create table CATEGORY(
	category_id int primary key,
	name nvarchar(50) not null,
	group_id int not null
	foreign key (group_id ) references PRODUCT_GROUP(group_id)
)
create table SALES(
	sale_id int primary key identity(1,1),
	sale_name nvarchar(50),
	begin_date date,
	end_date date,
	[percent] int
)
create table PRODUCT(
	product_id int primary key identity(1,1),
	category_id int not null,
	sale_id int default 1,
	name nvarchar(100) null,
	price int not null,
	brand varchar(50),
	sold int not null,
	size varchar(5) not null,
	content nvarchar(500) not null,
	image_link nvarchar(150),
	image_list nvarchar(500) 
	foreign key (category_id) references CATEGORY(category_id),
	foreign key (sale_id) references SALES(sale_id)
)

create table MEMBER(
	member_id int primary key identity(1,1),
	username varchar(50) not null,
	name nvarchar(50) not null,
	[password] varchar(50) not null,
	phone_number varchar(20) not null,
	[address] nvarchar(100) not null,
	[role] int default 2,
	cart_id int 	
)

create table REPORT(
	report_id int primary key identity(1,1),
	report_date date,
	qty int not null,
	product_id int,
	employee_id int,
	transaction_id int,
	amount int not null,
	member_id int foreign key (member_id ) references MEMBER(member_id)
)

create table [TRANSACTION](
	transaction_id int primary key identity(1,1),
	[status] tinyint not null,
	member_id int,
	member_name nvarchar(50) not null ,
	product_id int not null,
	qty int not null,
	payment bit not null,
	delivery nvarchar(500),
	member_phone_number varchar(20) not null,
	amount int not null
	foreign key (member_id) references MEMBER(member_id)
)

create table CART(
	cart_id int primary key,
	amount int not null,
	member_id int foreign key (member_id) references MEMBER(member_id),
	cart_status nvarchar(500),
) 

create table CART_ITEM(
	cart_item_id int primary key identity(1,1),
	cart_id int foreign key (cart_id) references CART(cart_id),
	product_id int foreign key (product_id) references PRODUCT(product_id),
	qty int,
	amount int,
	price int,
	size varchar(5)
)

CREATE TABLE BRAND(
	brand_id int primary key identity(1,1),
	brand_name nvarchar(50),
	description nvarchar(100)
)

