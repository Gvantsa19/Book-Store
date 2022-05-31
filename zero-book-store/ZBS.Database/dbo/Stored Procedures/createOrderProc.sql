
create procedure  createOrderProc
@UserId int, 
@Address nvarchar(100),
@Phone nvarchar(100),
@PaymentType nvarchar(50),
@Status nvarchar(50)  
as 
begin
begin tran t1
declare @BookQuantity int
declare @BookPrice decimal(5,2) 
declare @Total_Price decimal(5,2) 
declare @BookId int 
declare @OrderId int  
declare @ItemQuantity int
declare @BookItemQuantity int
declare @Count int
declare @ShippingPrice decimal(5,2) = 5
declare @BasketId int
declare @DeliveryId int 

if exists(select UserId  from Basket where DateDeleted is null and UserId = @UserId)
		begin
				select @BasketId = Basket.Id
				from Basket
				where DateDeleted is null and UserId = @UserId 

				select top 1 @DeliveryId = Id from [User]  where [User].Role = 3  order by newid()

				 

				set @BookQuantity = (select sum(Quantity) from BasketBook where BasketId = @BasketId)
				set @BookPrice = (select sum(Price) as Price from BasketBook where BasketId = @BasketId)
				set @Total_Price = @BookPrice + @ShippingPrice 
				set @Count = (select count(BookId) from BasketBook where BasketId =  @BasketId and DateDeleted is null)	
				 
					while (@count > 0)
						begin
								BEGIN TRANSACTION  tran1
									begin
										set @BookId = (select top 1 BookId from BasketBook where BasketId = @BasketId and  DateDeleted is null)
										set @ItemQuantity = (select Quantity from BasketBook where BasketId = @BasketId  and BookId = @BookId)	
							
										set @BookItemQuantity = (select Quantity from Book  where DateDeleted is null and Id = @BookId)
							
										if @ItemQuantity < @BookItemQuantity
												begin  
													update Book set Quantity =  @BookItemQuantity - @ItemQuantity 
													where DateDeleted is null and Id = @BookId 
											 
													Update BasketBook  set DateDeleted = GETDATE()
													where DateDeleted is null and BookId = @BookId 
											  
												commit  TRANSACTION  tran1  
 
												end
										else
												begin 
												rollback  TRANSACTION  tran1 
												end 
										 
										end
										 
						set @Count = @Count -1
					
							 
						end

						
						if @@trancount > 0
						begin
							
							commit tran t1
							insert into [Order](
							UserId,  Quantity, [Address], Phone, PaymentType,  TotalPrice,  ShippingPrice, DeliveryId,  [Status],  DateCreated) 
							values(@UserId, @BookQuantity,  @Address, @Phone, @PaymentType, @Total_Price,@ShippingPrice , @DeliveryId, @Status, GETDATE())  

							Update Basket  set DateDeleted = GETDATE()
							where DateDeleted is null and UserId = @UserId

							
							select Quantity, [Address], Phone, PaymentType, TotalPrice,  ShippingPrice, DeliveryId,  [Status]
							from [Order]  
									where [Id] = (select IDENT_CURRENT('Order')) and UserId = @UserId and DateDeleted is null 


							
						end
						else 
						begin
							rollback tran t1
						end
						 			 
 	end
	 
end