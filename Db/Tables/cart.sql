CREATE TABLE [dbo].[Cart]
(
 [CartId] uniqueidentifier NOT NULL,
 [CreatedAt] datetime NOT NULL,
 [CartStatusId] uniqueidentifier NOT NULL,


 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([CartId] ASC),
 CONSTRAINT [FK_dbo_Cart_dbo_CartStatus] FOREIGN KEY ([CartStatusId])  REFERENCES [dbo].[CartStatus]([CartStatusId])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_Cart_CartStatusId] ON [dbo].[Cart] 
 (
  [CartStatusId] ASC
 )
GO
