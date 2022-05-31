CREATE TABLE [dbo].[Sales] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [CategoryID]  INT           NOT NULL,
    [Percent]     SMALLINT      NOT NULL,
    [DateCreated] DATETIME2 (7) NOT NULL,
    [DateUpdated] DATETIME2 (7) NULL,
    [DateDeleted] DATETIME2 (7) NULL,
    CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sales_BookCategory_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[BookCategory] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Sales_CategoryID]
    ON [dbo].[Sales]([CategoryID] ASC);

