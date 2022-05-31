CREATE PROCEDURE [dbo].[DeleteItemProc]

@UserId int,
@BookId int

AS
	begin

                    if exists(select UserId from Basket
		                    inner join BasketBook
		                    on Basket.Id = BasketBook.BasketId
		                    where basket.UserId = @UserId and BasketBook.DateDeleted is null and BasketBook.BookId = @BookId)
 
		                    if exists(select BasketBook.BasketId 
						                    from BasketBook
						                    inner join Basket on Basket.Id = BasketBook.BasketId
						                    where Basket.UserId = @UserId and  BasketBook.DateDeleted is null)
					                     begin
						                    Update BasketBook
						                    set DateDeleted = GETDATE()
						                    where BasketBook.BookId = @BookId and BasketBook.DateDeleted is null 

							                    if not exists(select BasketBook.BasketId 
								                    from BasketBook
								                    inner join Basket on Basket.Id = BasketBook.BasketId
								                    where Basket.UserId = @UserId and  BasketBook.DateDeleted is null)
									
									                    Update Basket
									                    set DateDeleted = GETDATE()
									                    where Basket.UserId = @UserId and Basket.DateDeleted is null 

								 


						                    end
      
                    end