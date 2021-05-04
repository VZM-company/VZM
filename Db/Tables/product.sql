CREATE TABLE [dbo].[Product]
(
 [ProductId] uniqueidentifier NOT NULL,
 [Title] varchar(100) NOT NULL,
 [MetaTitle] varchar(50) NOT NULL,
 [Price] float NOT NULL,
 [CreatedAt] datetime NOT NULL,
 [Description] varchar(MAX) NOT NULL,
 [DescriptionShort] varchar(50) NOT NULL,
 [ImageUrl] varchar(200) NULL,
 [CartId] uniqueidentifier NULL,
 [SellerId] uniqueidentifier NOT NULL,


 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductId] ASC),
 CONSTRAINT [FK_dbo_Product_dbo_Cart] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Cart]([CartId]),
 CONSTRAINT [FK_dbo_Product_dbo_User] FOREIGN KEY ([SellerId]) REFERENCES [dbo].[User]([UserId])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_Product_dbo_CartId] ON [dbo].[Product] 
 (
  [CartId] ASC
 )
GO

CREATE NONCLUSTERED INDEX [IX_dbo_Product_dbo_SellerId] ON [dbo].[Product] 
 (
  [SellerId] ASC
 )
GO
