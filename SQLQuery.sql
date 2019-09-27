create database quanLyQuanCafeProject
go
use quanLyQuanCafeProject 
go

create table tableFood
(
	id int identity primary key,
	name nvarchar(100) not null default N'bàn chưa có tên',
	status nvarchar(100) not null default N'Trống'		--trống - có người
)
go

create table account
(
	username nvarchar(100) primary key,
	displayName nvarchar(100) not null default N'btbn',
	password nvarchar(1000) not null default 0,
	type int not null default 0		--1: admin, 0: staff
)
go

create table foodCategory
(
	id int identity primary key,
	Name nvarchar(100) not null default N'chưa đặt tên'
)
go

create table food
(
	id int identity primary key,
	Name nvarchar(100) not null default N'chưa đặt tên',
	idCategory int not null,
	price float not null default 0,
	foreign key (idCategory) references FoodCategory(id)
)
go

create table bill
(
	id int identity primary key,
	dateCheckIn date not null default getDate(),
	dateCheckOut date,
	idTable int not null,
	status int not null default 0, --1: đã thanh toán, 0: chưa thanh toán
	foreign key (idTable) references dbo.TableFood(id)
)
go

create table billInfo
(
	id int identity primary key,
	idBill int not null,
	idFood int not null,
	count int not null default 0,
	foreign key (idBill) references dbo.Bill(id),
	foreign key (idFood) references dbo.Food(id),

)
go