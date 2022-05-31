CREATE TABLE [dbo].[Book] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (60)   NOT NULL,
    [CategoryId]    INT             NOT NULL,
    [Price]         DECIMAL (18, 2) NOT NULL,
    [Quantity]      INT             NOT NULL,
    [DateofPublish] DATETIME2 (7)   NOT NULL,
    [Publisher]     NVARCHAR (MAX)  NOT NULL,
    [NumberOfPages] INT             NOT NULL,
    [Description]   NVARCHAR (300)  NOT NULL,
    [Language]      INT             NOT NULL,
    [DateCreated]   DATETIME2 (7)   NOT NULL,
    [DateUpdated]   DATETIME2 (7)   NULL,
    [DateDeleted]   DATETIME2 (7)   NULL,
    CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Book_BookCategory_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[BookCategory] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Book_CategoryId]
    ON [dbo].[Book]([CategoryId] ASC);

