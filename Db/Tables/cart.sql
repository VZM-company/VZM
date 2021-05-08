CREATE TABLE [dbo].[Cart]
(
 [UserId] uniqueidentifier NOT NULL,
 [ProductId] uniqueidentifier NOT NULL,


 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([UserId] ASC, [ProductId] ASC),
 CONSTRAINT [FK_dbo_Cart_dbo_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]),
 CONSTRAINT [FK_dbo_Cart_dbo_Product] FOREIGN KEY ([ProductId])  REFERENCES [dbo].[Product]([ProductId])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_Cart_UserId] ON [dbo].[Cart] 
 (
  [UserId] ASC
 )
GO

CREATE NONCLUSTERED INDEX [IX_dbo_Cart_ProductId] ON [dbo].[Cart] 
 (
  [ProductId] ASC
 )
GO