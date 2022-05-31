CREATE TABLE [dbo].[BookAuthor] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [BookId]      INT           NOT NULL,
    [AuthorId]    INT           NOT NULL,
    [DateCreated] DATETIME2 (7) NOT NULL,
    [DateUpdated] DATETIME2 (7) NULL,
    [DateDeleted] DATETIME2 (7) NULL,
    CONSTRAINT [PK_BookAuthor] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BookAuthor_Author_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Author] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BookAuthor_Book_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_BookAuthor_AuthorId]
    ON [dbo].[BookAuthor]([AuthorId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BookAuthor_BookId]
    ON [dbo].[BookAuthor]([BookId] ASC);

