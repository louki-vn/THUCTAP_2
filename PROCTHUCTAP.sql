create PROC CheckMember
@username varchar(50),
@password varchar(50)
AS
BEGIN
	select *
	from dbo.MEMBER 
	where username = @username and password = @password
END

create PROC AddProduct
@name nvarchar(50),
@category_id int, 
@sale_id int,
@price int, 
@brand varchar(50),
@sold int, 
@size varchar(5),
@content nvarchar(500),
@image_link varchar(50), 
@image_list varchar(500)
AS
BEGIN
	insert into PRODUCT(name, category_id, sale_id, price, brand, sold, size, content, image_link, image_list)
	values(@name, @category_id, @sale_id, @price, @brand, @sold, @size, @content, @image_link, @image_list)
END


CREATE PROC DeleteProduct
@id int
AS 
BEGIN
	delete from PRODUCT where product_id = @id
END

CREATE PROC AddCategory
@name varchar(50),
@group_id int
AS 
BEGIN
	insert into CATEGORY(name, group_id)
	values(@name, @group_id)
END

CREATE PROC AddMember
@name nvarchar(50),
@username varchar(50),
@password varchar(50), 
@address nvarchar(100), 
@phone varchar(20)
AS 
BEGIN
	insert into MEMBER(name, username, password, address, phone_number)
	values(@name, @username, @password, @address, @phone)
END

CREATE PROC DeleteMember
@id int
AS
BEGIN
	delete from CART where member_id = @id
	delete from MEMBER where member_id = @id
END


create trigger Set_cart_id_auto
on MEMBER
after insert
as 
begin
	declare @cart_id int, @member_id int
	set @cart_id = (select member_id from inserted)
	update MEMBER set cart_id = @cart_id where member_id = @cart_id
end

CREATE TRIGGER FinishOrder
on REPORT
after insert
as
BEGIN
	declare @order_id int
	set @order_id = (select transaction_id from inserted)
	update [TRANSACTION] 
	set status = 3
	where transaction_id = @order_id
END

CREATE PROC UpdateTransactionStatus
@id int,
@status int
AS
BEGIN
	update [TRANSACTION] set status = 1 
	where transaction_id = @id
END

CREATE PROC AddReport
@tran_id int,
@employee_id int,
@mem_id int,
@amount int,
@qty int,
@product_id int, 
@date date
AS 
BEGIN
	insert into REPORT(transaction_id, employee_id, member_id, amount, qty, product_id, report_date)
	values(@tran_id, @employee_id, @mem_id, @amount, @qty, @product_id, @date)
END

CREATE PROC CheckReport
@tran_id int
AS
BEGIN
	select *
	from REPORT
	where transaction_id = @tran_id
END

CREATE PROC EditCategory
@id int,
@name nvarchar(50),
@group_id int
AS
BEGIN
	Update  CATEGORY 
	SET name = @name, group_id = @group_id
	Where category_id = @id
END

CREATE PROC FilterTransaction
@type int
AS 
BEGIN
	select *
	from [TRANSACTION]
	where status = @type
END

CREATE trigger create_CART_auto 
on MEMBER 
after insert
as 
begin
	declare @member_id int
	set @member_id = (select member_id from inserted)
	update MEMBER set cart_id = @member_id where member_id = @member_id
	insert into CART
	values (@member_id,0,0,@member_id)
end

ALTER PROC FilterCategory
@type int
AS 
BEGIN
	select *
	from CATEGORY
	where group_id = @type
END

CREATE PROC FilterProduct
@type int
AS 
BEGIN
	select *
	from PRODUCT
	where category_id = @type
END

create PROC DeleteCategory
@id int
as
BEGIN
	Delete from CATEGORY
	where category_id = @id
END

CREATE PROC FilterMember
@id int
as
BEGIN
	SELECT *
	from MEMBER
	where role = @id
END

CREATE PROC EditProduct
@id int,
@name nvarchar(100),
@size varchar(5),
@price int,
@content nvarchar(500),
@sale int
AS
BEGIN
	update PRODUCT
	set name = @name, size = @size, content = @content, price = @price, sale_id = @sale
	Where product_id = @id
END

CREATE PROC CheckProductInCart 
@cart_id int,
@product_id int,
@size varchar(5)
AS
BEGIN
	Select *
	from CART_ITEM
	where cart_id = @cart_id and product_id = @product_id and size = @size
END 

Create PROC UpdateNumberProductInCartItem
@cart_id int, 
@product_id int, 
@size varchar(5)
AS
BEGIN
	update CART_ITEM
	set qty = qty +1 
	where cart_id = @cart_id and product_id = @product_id and size = @size
END 

create PROC [dbo].[remove_CART_ITEM_from_product_id_and_size]
	@product_id INT, 
	@size varchar(5)
AS
BEGIN
	DELETE FROM CART_ITEM WHERE product_id = @product_id and size = @size
END
