CREATE TABLE [dbo].[BookCategory] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (40) NOT NULL,
    [DateCreated] DATETIME2 (7) NOT NULL,
    [DateUpdated] DATETIME2 (7) NULL,
    [DateDeleted] DATETIME2 (7) NULL,
    CONSTRAINT [PK_BookCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

