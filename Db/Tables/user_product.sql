CREATE TABLE [dbo].[user_product]
(
 [product_id] int NOT NULL ,
 [user_id]    int NOT NULL ,


 CONSTRAINT [PK_user_product] PRIMARY KEY CLUSTERED ([product_id] ASC, [user_id] ASC),
 CONSTRAINT [FK_dbo_user_product_dbo_product] FOREIGN KEY ([product_id])  REFERENCES [dbo].[product]([product_id]),
 CONSTRAINT [FK_dbo_user_product_dbo_user] FOREIGN KEY ([user_id])  REFERENCES [dbo].[user]([user_id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_117] ON [dbo].[user_product] 
 (
  [product_id] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_121] ON [dbo].[user_product] 
 (
  [user_id] ASC
 )

GO
