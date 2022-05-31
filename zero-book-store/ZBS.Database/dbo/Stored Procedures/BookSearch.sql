CREATE FUNCTION BookSearch (@Title nvarchar(30), @CategoryName nvarchar(30))
RETURNS TABLE
AS
RETURN
SELECT b.[Id]
      , b.[Title]
      ,b.[CategoryId]
	  ,bc.[Name] as CategoryName
      ,b.[Price]
      ,b.[Quantity]
      ,b.[DateofPublish]
      ,b.[Publisher]
      ,b.[NumberOfPages]
      ,b.[Description]
      ,b.[Language]
	  ,a.Id as AuthorId
	  ,a.FirstName as AuthorFirstName
	  ,a.LastName as AuthorLastName
      ,b.[DateCreated]
      ,b.[DateUpdated]
      ,b.[DateDeleted]
  FROM[dbo].[Book] b
 left join BookCategory bc
 on bc.Id = b.CategoryId
  left join BookAuthor ba
  on ba.BookId = b.Id
  left join Author a
  on ba.AuthorId = a.Id
  where @Title Is null or b.Title Like '%'+@Title+'%'
  And @CategoryName is null or bc.[Name] Like '%'+@CategoryName+'%'