CREATE TABLE [dbo].[OrderStatus]
(
 [OrderStatusId] uniqueidentifier NOT NULL,
 [Name] varchar(30) NOT NULL,
 [NameShort] varchar(10) NOT NULL,


 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED ([OrderStatusId] ASC)
);
GO
