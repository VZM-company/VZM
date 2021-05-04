CREATE TABLE [dbo].[Discount]
(
 [DiscountId] uniqueidentifier NOT NULL,
 [Value] float NOT NULL,
 [CreatedAt] datetime NOT NULL,
 [ExpiredAt] datetime NOT NULL,
 [ProductId] uniqueidentifier NOT NULL,


 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED ([DiscountId] ASC),
 CONSTRAINT [FK_dbo_Discount_dbo_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product]([ProductId]),
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_Discount_ProductId] ON [dbo].[Discount] 
 (
  [ProductId] ASC
 )
GO
