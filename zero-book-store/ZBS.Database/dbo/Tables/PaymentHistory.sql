CREATE TABLE [dbo].[PaymentHistory] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [PaymentType]   NVARCHAR (30)   NOT NULL,
    [UserId]        INT             NOT NULL,
    [Amount]        DECIMAL (18, 2) NOT NULL,
    [DateOfPayment] DATETIME2 (7)   NOT NULL,
    [CardNumber]    NVARCHAR (MAX)  NULL,
    [CVC]           NVARCHAR (3)    NULL,
    [DateCreated]   DATETIME2 (7)   NOT NULL,
    [DateUpdated]   DATETIME2 (7)   NULL,
    [DateDeleted]   DATETIME2 (7)   NULL,
    CONSTRAINT [PK_PaymentHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentHistory_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PaymentHistory_UserId]
    ON [dbo].[PaymentHistory]([UserId] ASC);

