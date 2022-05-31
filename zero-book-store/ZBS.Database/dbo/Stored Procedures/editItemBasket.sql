
create proc [editItemBasket]

@UserId int,
@BookId int,
@Quantity int
  as 

  begin 
   
					declare @BookQuantity int
					declare @Percent decimal(5,2) 
					declare @Price decimal(5,2) 
                    set @BookQuantity = (select Quantity from Book where Book.Id = @BookId)

                    if exists(select UserId from Basket
		                    inner join BasketBook
		                    on Basket.Id = BasketBook.BasketId
		                    where basket.UserId = @UserId and BasketBook.DateDeleted is null and BasketBook.BookId = @BookId)
	                    if @Quantity<=@BookQuantity
							 
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
											
										
												Update BasketBook
												set Quantity = @Quantity, Price = ((@Price - (@Price * @Percent/100)) * @Quantity )
												where BasketBook.DateDeleted is null and BasketBook.BookId = @BookId
												select Book.Title, BasketBook.Price, BasketBook.Quantity  
												from BasketBook
												inner join Book on BasketBook.BookId = Book.Id
												where BasketBook.BookId = @BookId and BasketBook.DateDeleted is null
											end    

									else


										begin 
	                                    Update BasketBook
	                                    set Quantity = @Quantity, Price = ((select Price from Book where Book.Id = @BookId) * @Quantity)
	                                    where BasketBook.DateDeleted is null and BasketBook.BookId = @BookId
	                                    select Book.Title, BasketBook.Price, BasketBook.Quantity  
					                    from BasketBook
					                    inner join Book on BasketBook.BookId = Book.Id
					                    where BasketBook.BookId = @BookId and BasketBook.DateDeleted is null
                                    end 

                    end