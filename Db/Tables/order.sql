CREATE TABLE [dbo].[order]
(
 [order_id]        int NOT NULL ,
 [created_at]      datetime NOT NULL ,
 [cart_id]         int NOT NULL ,
 [order_status_id] int NOT NULL ,


 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED ([order_id] ASC),
 CONSTRAINT [FK_dbo_order_dbo_cart] FOREIGN KEY ([cart_id])  REFERENCES [dbo].[cart]([cart_id]),
 CONSTRAINT [FK_dbo_order_dbo_order_status] FOREIGN KEY ([order_status_id])  REFERENCES [dbo].[order_status]([order_status_id])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_order_cart_id] ON [dbo].[order] 
 (
  [cart_id] ASC
 )
GO

CREATE NONCLUSTERED INDEX [IX_dbo_order_order_status_id] ON [dbo].[order] 
 (
  [order_status_id] ASC
 )
GO
