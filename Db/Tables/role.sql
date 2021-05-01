CREATE TABLE [dbo].[role]
(
 [role_id]    int NOT NULL ,
 [name]       varchar(30) NOT NULL ,
 [name_short] varchar(10) NOT NULL ,


 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED ([role_id] ASC)
);
GO