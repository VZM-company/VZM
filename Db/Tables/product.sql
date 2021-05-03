CREATE TABLE [dbo].[product]
(
 [product_id]        int NOT NULL ,
 [title]             varchar(100) NOT NULL ,
 [meta_title]        varchar(50) NOT NULL ,
 [price]             float NOT NULL ,
 [created_at]        datetime NOT NULL ,
 [description]       text NOT NULL ,
 [description_short] varchar(50) NOT NULL ,
 [cart_id]           int NULL ,
 [seller_id]         int NULL ,


 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED ([product_id] ASC),
 CONSTRAINT [FK_dbo_product_dbo_cart] FOREIGN KEY ([cart_id])  REFERENCES [dbo].[cart]([cart_id]),
 CONSTRAINT [FK_dbo_product_dbo_user] FOREIGN KEY ([seller_id])  REFERENCES [dbo].[user]([user_id])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_product_cart_id] ON [dbo].[product] 
 (
  [cart_id] ASC
 )
GO

CREATE NONCLUSTERED INDEX [IX_dbo_product_seller_id] ON [dbo].[product] 
 (
  [seller_id] ASC
 )
GO
