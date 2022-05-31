CREATE TABLE [dbo].[Basket] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [UserId]      INT           NOT NULL,
    [DateCreated] DATETIME2 (7) NOT NULL,
    [DateUpdated] DATETIME2 (7) NULL,
    [DateDeleted] DATETIME2 (7) NULL,
    CONSTRAINT [PK_Basket] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Basket_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Basket_UserId]
    ON [dbo].[Basket]([UserId] ASC);

