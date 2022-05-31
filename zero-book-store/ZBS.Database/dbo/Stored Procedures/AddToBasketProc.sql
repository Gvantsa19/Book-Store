
create procedure AddToBasketProc
@UserId int,
@BookId int,
@Quantity int
as 
begin 
 
declare @BookQuantity int 
declare @BasketId int 
declare @Percent decimal(5,2) 
declare @Price decimal(5,2) 

		if exists(select UserId  from Basket where DateDeleted is null and UserId = @UserId) 
	
			begin
					BEGIN TRANSACTION  N1
					
					select @BookQuantity = Quantity 
									from Book 
									where Book.Id = @BookId

						
	 
						if @Quantity <= @BookQuantity 

							  if exists(select Quantity from BasketBook where BasketId =  (select Id from Basket where UserId = @UserId and DateDeleted is null) and BookId = @BookId)
										
										begin  
											
											ROLLBACK  TRANSACTION  N1  
										
										end 
							 else
									begin
										 if exists(select Sales.[Percent]  from Sales where Sales.Id = (select Sales.Id from Sales  
																   inner join BookCategory on Sales.CategoryID = BookCategory.Id 
																   inner join Book on BookCategory.Id = Book.CategoryId
																   where Book.Id = @BookId))
													 begin
														 select @Percent = Sales.[Percent]  from Sales where Sales.Id = (select Sales.Id from Sales  
														 inner join BookCategory on Sales.CategoryID = BookCategory.Id 
														 inner join Book on BookCategory.Id = Book.CategoryId
														 where Book.Id = @BookId)

														 select @Price = Book.Price  from Book
														 where Book.Id = @BookId 
																				 
														 insert into BasketBook(Quantity, Price ,BasketId, BookId, DateCreated)
														 values(@Quantity,((@Price - (@Price * @Percent/100)) * @Quantity ),(select Id from Basket where UserId = @UserId and DateDeleted is null), @BookId, GETDATE())
								 
														 COMMIT  TRANSACTION  N1 
														 set @BasketId = (select Id from Basket where Basket.UserId = @UserId and Basket.DateDeleted is null)

														 select Book.Title, BasketBook.Price, BasketBook.Quantity  
														 from BasketBook
														 inner join Book on BasketBook.BookId = Book.Id
														 inner join Basket on BasketBook.BasketId = Basket.Id
														 where BasketBook.BookId = @BookId and BasketBook.DateDeleted is null and BasketBook.BasketId = @BasketId
														end
											else 

											begin
														 select @Price = Book.Price  from Book
														 where Book.Id = @BookId 
																				 
														 insert into BasketBook(Quantity, Price ,BasketId, BookId, DateCreated)
														 values(@Quantity,(@Price * @Quantity),(select Id from Basket where UserId = @UserId and DateDeleted is null), @BookId, GETDATE())
								 
														 COMMIT  TRANSACTION  N1 
														 set @BasketId = (select Id from Basket where Basket.UserId = @UserId and Basket.DateDeleted is null)

														 select Book.Title, BasketBook.Price, BasketBook.Quantity  
														 from BasketBook
														 inner join Book on BasketBook.BookId = Book.Id
														 inner join Basket on BasketBook.BasketId = Basket.Id
														 where BasketBook.BookId = @BookId and BasketBook.DateDeleted is null and BasketBook.BasketId = @BasketId

											end

									end
							  
						else 
						 
							 begin  
								 ROLLBACK  TRANSACTION  N1 
							 end
								 
			end  
		else 
			begin
			
					insert into Basket(UserId, DateCreated)
					values(@UserId, GETDATE())
  
					BEGIN TRANSACTION  N2
						 
							set @BookQuantity = (select Quantity 
												from Book 
												where Book.Id = @BookId)
							
	 
					if @Quantity <= @BookQuantity 
							 if exists(select Quantity from BasketBook where BasketId =  (select Id from Basket where UserId = @UserId and DateDeleted is null) and BookId = @BookId)
								begin 
								 
									ROLLBACK  TRANSACTION  N2 
								end 
							else
								begin
									 if exists(select Sales.[Percent]  from Sales where Sales.Id = (select Sales.Id from Sales  
																   inner join BookCategory on Sales.CategoryID = BookCategory.Id 
																   inner join Book on BookCategory.Id = Book.CategoryId
																   where Book.Id = @BookId))
													 begin
														 select @Percent = Sales.[Percent]  from Sales where Sales.Id = (select Sales.Id from Sales  
														 inner join BookCategory on Sales.CategoryID = BookCategory.Id 
														 inner join Book on BookCategory.Id = Book.CategoryId
														 where Book.Id = @BookId)

														 select @Price = Book.Price  from Book
														 where Book.Id = @BookId 
																				 
														 insert into BasketBook(Quantity, Price ,BasketId, BookId, DateCreated)
														 values(@Quantity,((@Price - (@Price * @Percent/100)) * @Quantity ),(select Id from Basket where UserId = @UserId and DateDeleted is null), @BookId, GETDATE())
								 
														 COMMIT  TRANSACTION  N1 
														 set @BasketId = (select Id from Basket where Basket.UserId = @UserId and Basket.DateDeleted is null)

														 select Book.Title, BasketBook.Price, BasketBook.Quantity  
														 from BasketBook
														 inner join Book on BasketBook.BookId = Book.Id
														 inner join Basket on BasketBook.BasketId = Basket.Id
														 where BasketBook.BookId = @BookId and BasketBook.DateDeleted is null and BasketBook.BasketId = @BasketId
														end
											else 

											begin
														 select @Price = Book.Price  from Book
														 where Book.Id = @BookId 
																				 
														 insert into BasketBook(Quantity, Price ,BasketId, BookId, DateCreated)
														 values(@Quantity,(@Price * @Quantity),(select Id from Basket where UserId = @UserId and DateDeleted is null), @BookId, GETDATE())
								 
														 COMMIT  TRANSACTION  N1 
														 set @BasketId = (select Id from Basket where Basket.UserId = @UserId and Basket.DateDeleted is null)

														 select Book.Title, BasketBook.Price, BasketBook.Quantity  
														 from BasketBook
														 inner join Book on BasketBook.BookId = Book.Id
														 inner join Basket on BasketBook.BasketId = Basket.Id
														 where BasketBook.BookId = @BookId and BasketBook.DateDeleted is null and BasketBook.BasketId = @BasketId

											end

									end
							   
						 
					else 
							if exists(select BasketId  from BasketBook where BasketId = (select Id from Basket where UserId = @UserId and DateDeleted is null) and DateDeleted is null) 
								
								begin
									ROLLBACK  TRANSACTION  N2
									Update Basket
									set DateDeleted = GETDATE()
									where Basket.UserId = @UserId 
								end  
								else
								begin  
									ROLLBACK  TRANSACTION  N2 
								end

	end
 
end  