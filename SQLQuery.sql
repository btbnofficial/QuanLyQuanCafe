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
go

--Bài 11: Thêm bớt món ăn

alter proc usp_insertBill
@idTable int
as
begin
	insert into Bill
	(dateCheckIn, dateCheckOut, idTable, status, discount)
	values
	(
		GETDATE(),
		Null,
		@idTable,
		0,
		0
	)
end
go 

alter proc usp_insertBillInfo
@idBill INT ,
@idFood int ,
@count int
as
begin
	declare @isExistBillInfo int;
	declare @foodCount int = 1;

	select @isExistBillInfo = id , @foodCount = b.count 
	from dbo.billInfo as b
	where idBill = @idBill and idFood = @idFood
	
	--Nếu đã tồn tại BillInfo này rồi thì cập nhật nó
	if(@isExistBillInfo > 0)
	begin
		declare @newCount int = @foodCount + @count;
		if(@newCount>0)
			update billInfo set count = @count + @foodCount where idFood = @idFood
		else
			Delete dbo.billInfo where idBill = @idBill and idFood = @idFood
	end
	else	--Nếu chưa tồn tại BillInfo thì tạo mới bằng cách insert
	begin
		insert into billInfo( idBill , idFood , count)
		values
		(
			@idBill ,
			@idFood ,
			@count
		)
	end

	
end
go

select Max(id) from bill

--Bài 12: Thanh toán hóa đơn
update dbo.bill set status = 1 where id = 1
go


--Trigger mỗi khi thêm hoặc cập nhật hóa đơn
alter trigger UTG_UpdateBillInfo
ON dbo.BillInfo for insert, update
as
begin
	declare @idBill int

	select @idBill = idBill from inserted

	declare @idTable int

	Select @idTable = idTable from dbo.bill where id = @idBill and status = 0 --nếu chưa checkOut

	declare @count int
	select @count = COUNT(*) from billInfo where idBill = @idBill

	if(@count > 0)
	begin
		print @idTable
		print @idBill
		print @count
		update dbo.tableFood set status = N'Có người' where id = @idTable
	end
	else 
	begin
		print @idTable
		print @idBill
		print @count
		update dbo.tableFood set status = N'Trống' where id = @idTable
	end

end
go


alter TRigger UTG_UpdateBill
on dbo.Bill for update
as
begin
	declare @idBill int

	select @idBill = id from inserted

	declare @idTable int

	Select @idTable = idTable from dbo.bill where id = @idBill

	declare @count int = 0

	select @count = Count(*) from Bill where idTable = @idTable and status = 0

	if(@count = 0)
		update tableFood set status = N'Trống' where id = @idTable

end
go

delete billInfo
delete Bill

												--Bài 13: Chuyển bàn
alter table Bill
add Discount int 


update Bill set discount = 0
go

alter proc usp_SwitchTable
@idTable1 int , @idTable2 int
as
begin
	declare @idFirstBill int
	declare @idSecondBill int

	declare @isFirstTableEmpty int = 1
	declare @isSecondTableEmpty int = 1

	Select @idSecondBill = id from bill where idTable = @idTable2 and status = 0
	Select @idFirstBill = id from bill where idTable = @idTable1 and status = 0

	print @idFirstBill
	print @idSecondBill
	print '--------------'

	if(@idFirstBill is null)
	begin
		print '000000000002'
		insert into Bill(dateCheckIn , dateCheckOut , idTable , status)
		values(
		GETDATE() ,
		null ,
		@idTable1 ,
		0
		)

		select @idFirstBill = max(id) from bill where idTable = @idTable1 and status = 0
		
	end

	select @isFirstTableEmpty = count(*) from dbo.BillInfo  where idBill = @idFirstBill

	print @idFirstBill
	print @idSecondBill
	print '--------------'

	if(@idSecondBill is null)
	begin  
		print '000000000001'
		insert into Bill(dateCheckIn , dateCheckOut , idTable , status)
		values(
		GETDATE() ,
		null ,
		@idTable2 ,
		0
		)

		select @idSecondBill = max(id) from bill where idTable = @idTable2 and status = 0

	end

	select @isSecondTableEmpty = count(*) from billInfo where idBill = @idSecondBill

	print @idFirstBill
	print @idSecondBill
	print '--------------'

	select id into IdBillInfoTable from dbo.billInfo where idBill = @idSecondBill

	update billInfo set idBill = @idSecondBill where idBill = @idFirstBill

	update billInfo set idBill = @idFirstBill where id in (select * from IdBillInfoTable)

	Drop table IdBillInfoTable

	if(@isFirstTableEmpty = 0)
		update tableFood set status = N'Trống' where id = @idTable2
	if(@isSecondTableEmpty = 0)
		update tableFood set status = N'Trống' where id = @idTable1
end
go

exec dbo.usp_SwitchTable @idTable1 = 4 , @idTable2 = 8

select * from tableFood

update tableFood set status = N'Trống'

--Bài 14: HIển thị danh sách hóa đơn
select * from bill
select * from billInfo

delete billInfo

update tableFood set status = N'Trống'

	
	--thêm một cột tổng tiền cho bảng Bill
alter table Bill add totalPrice Float 

alter proc ups_getListBillByDate
@checkIn Date, @checkOut date
as
begin
	select tableFood.name as [Tên bàn] , bill.totalPrice as [Tổng giá], Bill.dateCheckIn as [Ngày vào], Bill.dateCheckOut as [Ngày ra], Bill.Discount as [Giảm giá] 
	from Bill, tableFood
	where dateCheckIn >= @checkIn and dateCheckOut <= @checkOut and Bill.status = 1
	and tableFood.id = Bill.idTable 
end
go

--Bài 15: Thay đổi thông tin cá nhân
select * from account
insert account
values (N'Fuck', N'dep trai va tot', N'2', 0)
go

create proc usp_updateAccount
@username nvarchar(100),
@displayName nvarchar(100),
@password nvarchar(100),
@newPassword nvarchar(100)
as
begin
	declare @isRightPass int
	select @isRightPass = count(*) from account where username = @username and password = @password

	if(@isRightPass >= 1 )
	begin
		--Nếu mật khẩu mới là khoảng trống thì không cập nhật mật khẩu
		if(@newPassword = null or @newPassword = N'')
		begin
			update account set displayName = @displayName where username = @username
		end
		else
		begin
			update account set displayName = @displayName, password = @password where username = @username
		end
	end
end
go

-- Bài 16: Hiển thị danh sách thức ăn
--Bài 17: Binding thông tin thức ăn

select * from food
insert dbo.Food(Name, idCategory, price)
values
(
	N'loz',
	5,
	666
)

delete food

select * from food
go

alter Trigger utg_deleteBillInfo
on BillInfo for delete
as
begin
	declare @idBillInfo int
	declare @idBill int

	select @idBillInfo = id , @idBill = deleted.idBill from deleted

	declare @idTable int
	select @idTable = idTable from Bill where id = @idBill

	declare @count int = 0
	select @count = count(*) from billInfo as bi, bill as b where b.id = bi.idBill and b.id = @idBill and b.status = 0

	if(@count = 0)
		update tableFood set status = N'Trống' where id = @idTable
end

--Bài 19: Tìm kiếm gần đúng thức ăn	
--tao cai funtion chuyen chu hoa ve chua thuong
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
go
select * from dbo.Food where dbo.fuConvertToUnsign1(name) like N'%' + dbo.fuConvertToUnsign1(N'ham') + '%'
