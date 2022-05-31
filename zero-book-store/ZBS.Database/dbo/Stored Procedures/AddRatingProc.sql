CREATE PROCEDURE [dbo].[AddRatingProc]
	@Rating int,
    @UserId int,
    @Id int
AS
	                        begin   
                            declare @countrating decimal(10,2)
                            insert into [Ratings](Rating, UserId, Id, DateCreated)
                            values(@Rating, @UserId, @Id, GETDATE())
                                begin 
                                    set  @countrating = (select cast(avg(r.Rating)  as  decimal(10,2) )
					                                    from [Ratings] r
					                                    where r.Id = @Id )

                                    select  @countrating as Rating , b.Title , bc.[Name] [Genre],
                                    b.Price, b.DateofPublish, b.Publisher, b.NumberOfPages, b.[Description], b.[Language]
                                    from BookCategory bc 
                                    inner join Book b on bc.Id = b.CategoryId 
                                    inner join [Ratings] r on b.Id = r.Id
                                    where b.Id = @Id
                                    group by  b.Title , bc.[Name], b.Price, b.DateofPublish, b.Publisher, b.NumberOfPages, b.[Description], b.[Language]
                            
                                end
                        end