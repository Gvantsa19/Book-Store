CREATE TABLE [dbo].[Order] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [UserId]        INT             NOT NULL,
    [Quantity]      INT             NOT NULL,
    [Address]       NVARCHAR (120)  NOT NULL,
    [Phone]         NVARCHAR (MAX)  NOT NULL,
    [PaymentType]   NVARCHAR (30)   NOT NULL,
    [TotalPrice]    DECIMAL (18, 2) NOT NULL,
    [ShippingPrice] DECIMAL (18, 2) NOT NULL,
    [DeliveryId]    INT             NOT NULL,
    [Status]        NVARCHAR (20)   NOT NULL,
    [DateCreated]   DATETIME2 (7)   NOT NULL,
    [DateUpdated]   DATETIME2 (7)   NULL,
    [DateDeleted]   DATETIME2 (7)   NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Order_UserId]
    ON [dbo].[Order]([UserId] ASC);

