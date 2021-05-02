CREATE TABLE [dbo].[product_category]
(
 [category_id] int NOT NULL ,
 [product_id]  int NOT NULL ,


 CONSTRAINT [PK_product_category] PRIMARY KEY CLUSTERED ([category_id] ASC, [product_id] ASC),
 CONSTRAINT [FK_dbo_product_category_dbo_category] FOREIGN KEY ([category_id])  REFERENCES [dbo].[category]([category_id]),
 CONSTRAINT [FK_dbo_product_category_dbo_product] FOREIGN KEY ([product_id])  REFERENCES [dbo].[product]([product_id])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_product_category_category_id] ON [dbo].[product_category] 
 (
  [category_id] ASC
 )
GO

CREATE NONCLUSTERED INDEX [IX_dbo_product_category_product_id] ON [dbo].[product_category] 
 (
  [product_id] ASC
 )
GO
