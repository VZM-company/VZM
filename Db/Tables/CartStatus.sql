CREATE TABLE [dbo].[CartStatus]
(
 [CartStatusId] uniqueidentifier NOT NULL,
 [Name] varchar(30) NOT NULL,
 [NameShort] varchar(10) NOT NULL,


 CONSTRAINT [PK_CartStatus] PRIMARY KEY CLUSTERED ([CartStatusId] ASC)
);
GO
