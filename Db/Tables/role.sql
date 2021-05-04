CREATE TABLE [dbo].[Role]
(
 [RoleId] uniqueidentifier NOT NULL,
 [Name] varchar(30) NOT NULL,
 [NameShort] varchar(10) NOT NULL,


 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);
GO