CREATE TABLE [dbo].[cart]
(
 [cart_id]        int NOT NULL ,
 [created_at]     datetime NOT NULL ,
 [cart_status_id] int NOT NULL ,


 CONSTRAINT [PK_cart] PRIMARY KEY CLUSTERED ([cart_id] ASC),
 CONSTRAINT [FK_66] FOREIGN KEY ([cart_status_id])  REFERENCES [dbo].[cart_status]([cart_status_id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_67] ON [dbo].[cart] 
 (
  [cart_status_id] ASC
 )

GO
