CREATE TABLE [dbo].[Order]
(
 [OrderId] uniqueidentifier NOT NULL,
 [CreatedAt] datetime NOT NULL,
 [CartId] uniqueidentifier NOT NULL,
 [OrderStatusId] uniqueidentifier NOT NULL,


 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderId] ASC),
 CONSTRAINT [FK_dbo_Order_dbo_Cart] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Cart]([CartId]),
 CONSTRAINT [FK_dbo_Order_dbo_OrderStatus] FOREIGN KEY ([OrderStatusId]) REFERENCES [dbo].[OrderStatus]([OrderStatusId])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_Order_CartId] ON [dbo].[Order] 
 (
  [CartId] ASC
 )
GO

CREATE NONCLUSTERED INDEX [IX_dbo_Order_OrderStatusId] ON [dbo].[Order] 
 (
  [OrderStatusId] ASC
 )
GO
