CREATE TABLE [dbo].[category]
(
 [category_id]       int NOT NULL ,
 [title]             varchar(50) NOT NULL ,
 [meta_title]        varchar(25) NOT NULL ,
 [description]       varchar(250) NOT NULL ,
 [Parentcategory_id] int NOT NULL ,


 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED ([category_id] ASC),
 CONSTRAINT [FK_112] FOREIGN KEY ([Parentcategory_id])  REFERENCES [dbo].[category]([category_id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_113] ON [dbo].[category] 
 (
  [Parentcategory_id] ASC
 )

GO
