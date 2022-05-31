CREATE TABLE [dbo].[BasketBook] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Quantity]    INT           NOT NULL,
    [Price]       REAL          NOT NULL,
    [BasketId]    INT           NOT NULL,
    [BookId]      INT           NOT NULL,
    [DateCreated] DATETIME2 (7) NOT NULL,
    [DateUpdated] DATETIME2 (7) NULL,
    [DateDeleted] DATETIME2 (7) NULL,
    CONSTRAINT [PK_BasketBook] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasketBook_Basket_BasketId] FOREIGN KEY ([BasketId]) REFERENCES [dbo].[Basket] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_BasketBook_Book_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_BasketBook_BasketId]
    ON [dbo].[BasketBook]([BasketId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BasketBook_BookId]
    ON [dbo].[BasketBook]([BookId] ASC);

