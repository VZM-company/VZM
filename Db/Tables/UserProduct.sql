CREATE TABLE [dbo].[UserProduct]
(
 [ProductId] uniqueidentifier NOT NULL,
 [UserId] uniqueidentifier NOT NULL,


 CONSTRAINT [PK_UserProduct] PRIMARY KEY CLUSTERED ([ProductId] ASC, [UserId] ASC),
 CONSTRAINT [FK_dbo_UserProduct_dbo_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product]([ProductId]),
 CONSTRAINT [FK_dbo_UserProduct_dbo_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_UserProduct_ProductIdd] ON [dbo].[UserProduct] 
 (
  [ProductId] ASC
 )
GO

CREATE NONCLUSTERED INDEX [IX_dbo_UserProduct_UserId] ON [dbo].[UserProduct] 
 (
  [UserId] ASC
 )
GO
