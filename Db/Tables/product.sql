CREATE TABLE [dbo].[product]
(
 [product_id]        int NOT NULL ,
 [title]             varchar(100) NOT NULL ,
 [meta_title]        varchar(50) NOT NULL ,
 [price]             float NOT NULL ,
 [created_at]        datetime NOT NULL ,
 [description]       text NOT NULL ,
 [description_short] varchar(50) NOT NULL ,
 [cart_id]           int NOT NULL ,
 [seller_id]         int NOT NULL ,


 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED ([product_id] ASC),
 CONSTRAINT [FK_58] FOREIGN KEY ([cart_id])  REFERENCES [dbo].[cart]([cart_id]),
 CONSTRAINT [FK_95] FOREIGN KEY ([seller_id])  REFERENCES [dbo].[user]([user_id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_59] ON [dbo].[product] 
 (
  [cart_id] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_96] ON [dbo].[product] 
 (
  [seller_id] ASC
 )

GO
