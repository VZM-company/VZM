CREATE TABLE [dbo].[category]
(
 [category_id]       int NOT NULL ,
 [title]             varchar(50) NOT NULL ,
 [meta_title]        varchar(25) NOT NULL ,
 [description]       varchar(250) NOT NULL ,
 [Parentcategory_id] int NULL ,


 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED ([category_id] ASC),
 CONSTRAINT [FK_dbo_category_dbo_category] FOREIGN KEY ([Parentcategory_id])  REFERENCES [dbo].[category]([category_id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_113] ON [dbo].[category] 
 (
  [Parentcategory_id] ASC
 )

GO
