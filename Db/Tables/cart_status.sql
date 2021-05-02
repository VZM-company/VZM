CREATE TABLE [dbo].[cart_status]
(
 [cart_status_id] int NOT NULL ,
 [name]           varchar(30) NOT NULL ,
 [name_short]     varchar(10) NOT NULL ,


 CONSTRAINT [PK_cart_status] PRIMARY KEY CLUSTERED ([cart_status_id] ASC)
);
GO
