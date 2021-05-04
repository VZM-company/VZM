CREATE TABLE [dbo].[ProductCategory]
(
 [CategoryId] uniqueidentifier NOT NULL,
 [ProductId] uniqueidentifier NOT NULL,


 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED ([CategoryId] ASC, [ProductId] ASC),
 CONSTRAINT [FK_dbo_ProductCategory_dbo_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category]([CategoryId]),
 CONSTRAINT [FK_dbo_ProductCategory_dbo_Product] FOREIGN KEY ([ProductId])  REFERENCES [dbo].[Product]([ProductId])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_ProductCategory_CategoryId] ON [dbo].[ProductCategory] 
 (
  [CategoryId] ASC
 )
GO

CREATE NONCLUSTERED INDEX [IX_dbo_ProductCategory_ProductId] ON [dbo].[ProductCategory] 
 (
  [ProductId] ASC
 )
GO
