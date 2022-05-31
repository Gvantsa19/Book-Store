CREATE TABLE [dbo].[User] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50)  NOT NULL,
    [LastName]     NVARCHAR (50)  NOT NULL,
    [Role]         INT            NOT NULL,
    [Email]        NVARCHAR (MAX) NOT NULL,
    [MobileNumber] NVARCHAR (MAX) NULL,
    [DateOfBirth]  DATETIME2 (7)  NULL,
    [Password]     NVARCHAR (250) NOT NULL,
    [Salt]         NVARCHAR (250) NOT NULL,
    [Education]    NVARCHAR (200) NULL,
    [Address]      NVARCHAR (150) NOT NULL,
    [DateCreated]  DATETIME2 (7)  NOT NULL,
    [DateUpdated]  DATETIME2 (7)  NULL,
    [DateDeleted]  DATETIME2 (7)  NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

