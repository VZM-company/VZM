CREATE TABLE [dbo].[cart]
(
 [cart_id]        int NOT NULL ,
 [created_at]     datetime NOT NULL ,
 [cart_status_id] int NOT NULL ,


 CONSTRAINT [PK_cart] PRIMARY KEY CLUSTERED ([cart_id] ASC),
 CONSTRAINT [FK_dbo_cart_dbo_cart_status] FOREIGN KEY ([cart_status_id])  REFERENCES [dbo].[cart_status]([cart_status_id])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_cart_cart_status_id] ON [dbo].[cart] 
 (
  [cart_status_id] ASC
 )
GO
