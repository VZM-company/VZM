CREATE TABLE [dbo].[user]
(
 [user_id]       int NOT NULL ,
 [name]          varchar(50) NOT NULL ,
 [username]      varchar(50) NOT NULL ,
 [password_hash] varchar(200) NOT NULL ,
 [email]         varchar(256) NOT NULL ,
 [created_at]    datetime NOT NULL ,
 [info]          varchar(250) NOT NULL ,
 [confirmed]     bit NOT NULL ,
 [role_id]       int NOT NULL ,


 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([user_id] ASC),
 CONSTRAINT [FK_dbo_user_dbo_role] FOREIGN KEY ([role_id])  REFERENCES [dbo].[role]([role_id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_93] ON [dbo].[user] 
 (
  [role_id] ASC
 )

GO