CREATE TABLE [dbo].[User]
(
 [UserId] uniqueidentifier NOT NULL,
 [Name] varchar(50) NOT NULL,
 [Username] varchar(50) NOT NULL,
 [PasswordHash] varchar(200) NOT NULL,
 [Email] varchar(256) NOT NULL,
 [CreatedAt] datetime NOT NULL,
 [Info] varchar(250) NOT NULL,
 [Confirmed] bit NOT NULL,
 [ImageUrl] nvarchar(MAX),
 [RoleId] uniqueidentifier NOT NULL,


 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
 CONSTRAINT [FK_dbo_User_dbo_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role]([RoleId])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_User_RoleId] ON [dbo].[User] 
 (
  [RoleId] ASC
 )
GO