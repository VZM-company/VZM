CREATE TABLE [dbo].[order_status]
(
 [order_status_id] int NOT NULL ,
 [name]            varchar(30) NOT NULL ,
 [name_short]      varchar(10) NOT NULL ,


 CONSTRAINT [PK_order_status] PRIMARY KEY CLUSTERED ([order_status_id] ASC)
);
GO
