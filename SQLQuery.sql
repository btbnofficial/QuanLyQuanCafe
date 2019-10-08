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

insert into dbo.account
(
	username ,
	displayName ,
	password ,
	type 
)
values
(
	N'btbn',
	N'btbn_csgo',
	N'1',
	1
)


insert into dbo.account
(
	username ,
	displayName ,
	password ,
	type 
)
values
(
	N'staff',
	N'Nhan Vien 01',
	N'1',
	0
)

select * from dbo.account
go

create proc usp_getAccountByUserName
@username nvarchar(100)
as
begin
	select * from dbo.account where username = @username
end
go

exec dbo.usp_getAccountByUserName @username = N'' or 

select * from dbo.account where username = N'btbn' and password = N'1'

exec dbo.usp_getAccountByUserName @username = N'btbn'
go

create proc usp_login
@username nvarchar(100),
@password nvarchar(100)
as
begin
	Select * from dbo.account where @username = username and @password = password
end

--bai 8: tao danh sach ban an
declare @i int = 1
while @i <= 20
begin
	insert dbo.tableFood
	(
		name
		--status--s nvarchar(100) not null default N'Trống'		--trống - có người
	)
	values
	(
		N'Ban '+cast(@i as nvarchar(100))
		--nvarchar(100) not null default N'bàn chưa có tên',
	)
	set @i = @i+1
end

select * from dbo.tableFood

select * from dbo.account
go

create proc usp_GetTableList
as
select * from tableFood
go

exec usp_GetTableList

update dbo.tableFood set status = N'Có khách' where id = 5
--Bai 9: hien thi hoa don
select * from bill
select * from billInfo
select * from food
select * from foodCategory

--them danh muc
insert 
foodCategory(Name)
values(N'Hải Sản')
insert 
foodCategory(Name)
values(N'Đồ Uống')
insert 
foodCategory(Name)
values(N'Đồ Ăn Nhanh')
insert 
foodCategory(Name)
values(N'Thẻ điện thoại')
insert 
foodCategory(Name)
values(N'Khác')
--them mon an
insert 
dbo.food(Name, idCategory, price)
values(N'Cá Kho làng Vũ Đại', 1, 1200000)
insert 
dbo.food(Name, idCategory, price)
values(N'Đen đá có đường =)))', 2, 15000)
insert 
dbo.food(Name, idCategory, price)
values(N'Hăm bơ gơ', 3, 12000)
insert 
dbo.food(Name, idCategory, price)
values(N'Viettel20', 4, 20000)
insert 
dbo.food(Name, idCategory, price)
values(N'Khă năng code', 5, 999999999999)

--Thêm Bill
insert dbo.Bill
(dateCheckIn,dateCheckOut,idTable,status)
values(getDate(), null, 2, 0)
insert dbo.Bill
(dateCheckIn,dateCheckOut,idTable,status)
values(getDate(), null, 3, 0)
insert dbo.Bill
(dateCheckIn,dateCheckOut,idTable,status)
values(getDate(), null, 4, 0)
insert dbo.Bill
(dateCheckIn,dateCheckOut,idTable,status)
values(getDate(), GETDATE(), 5, 1)
insert dbo.Bill
(dateCheckIn,dateCheckOut,idTable,status)
values(getDate(), null, 4, 0)

--Thêm BillInfo
insert dbo.billInfo
(idBill, idFood, count)
values
(1,1,2)
insert dbo.billInfo
(idBill, idFood, count)
values
(1,3,1)
insert dbo.billInfo
(idBill, idFood, count)
values
(2,4,3)
insert dbo.billInfo
(idBill, idFood, count)
values
(1,2,5)
insert dbo.billInfo
(idBill, idFood, count)
values
(5,2,5)

select * from Bill where idTable = 3 and status = 0

select * from Bill

select * from billInfo where idBill = 1

select f.Name, bi.count, f.price, f.price * bi.count as totalPrice from billinfo as bi, Bill as b, Food as f 
where bi.idBill = b.id and bi.idFood = f.id and b.idTable = 5 and b.status = 0
select * from billInfo 
select * from bill
select * from tableFood