CREATE TABLE [dbo].[EBooks] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [FileName]    NVARCHAR (MAX)  NOT NULL,
    [FileSize]    INT             NOT NULL,
    [FileContent] VARBINARY (MAX) NOT NULL,
    [UploadDate]  DATETIME2 (7)   NOT NULL,
    [UploadedBy]  NVARCHAR (MAX)  NOT NULL,
    [DateCreated] DATETIME2 (7)   NOT NULL,
    [DateUpdated] DATETIME2 (7)   NULL,
    [DateDeleted] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_EBooks] PRIMARY KEY CLUSTERED ([Id] ASC)
);

