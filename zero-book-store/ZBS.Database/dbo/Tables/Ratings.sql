CREATE TABLE [dbo].[Ratings] (
    [RatingId]    INT           IDENTITY (1, 1) NOT NULL,
    [Rating]      INT           NOT NULL,
    [UserId]      INT           NOT NULL,
    [Id]          INT           NOT NULL,
    [DateCreated] DATETIME2 (7) NOT NULL,
    [DateUpdated] DATETIME2 (7) NULL,
    [DateDeleted] DATETIME2 (7) NULL,
    CONSTRAINT [PK_Ratings] PRIMARY KEY CLUSTERED ([RatingId] ASC),
    CONSTRAINT [FK_Ratings_Book_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Book] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Ratings_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Ratings_Id]
    ON [dbo].[Ratings]([Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Ratings_UserId]
    ON [dbo].[Ratings]([UserId] ASC);

