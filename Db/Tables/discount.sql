CREATE TABLE [dbo].[discount]
(
 [discount_id] int NOT NULL ,
 [value]       float NOT NULL ,
 [created_at]  datetime NOT NULL ,
 [expired_at]  datetime NOT NULL ,
 [product_id]  int NOT NULL ,


 CONSTRAINT [PK_discount] PRIMARY KEY CLUSTERED ([discount_id] ASC),
 CONSTRAINT [FK_dbo_discount_dbo_product] FOREIGN KEY ([product_id])  REFERENCES [dbo].[product]([product_id]),
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_discount_product_id] ON [dbo].[discount] 
 (
  [product_id] ASC
 )
GO
