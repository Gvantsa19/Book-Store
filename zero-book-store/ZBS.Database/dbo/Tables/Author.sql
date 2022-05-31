CREATE TABLE [dbo].[Author] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50) NOT NULL,
    [LastName]    NVARCHAR (50) NOT NULL,
    [DateOfBirth] DATETIME2 (7) NOT NULL,
    [DateCreated] DATETIME2 (7) NOT NULL,
    [DateUpdated] DATETIME2 (7) NULL,
    [DateDeleted] DATETIME2 (7) NULL,
    CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED ([Id] ASC)
);

