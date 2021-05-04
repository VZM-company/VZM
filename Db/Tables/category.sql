CREATE TABLE [dbo].[Category]
(
 [CategoryId] uniqueidentifier NOT NULL,
 [Title] varchar(50) NOT NULL,
 [MetaTitle] varchar(25) NOT NULL,
 [Description] varchar(250) NOT NULL,
 [ParentCategoryId] uniqueidentifier NOT NULL,


 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([CategoryId] ASC),
 CONSTRAINT [FK_dbo_Category_dbo_Category] FOREIGN KEY ([ParentCategoryId]) REFERENCES [dbo].[Category]([CategoryId])
);
GO


CREATE NONCLUSTERED INDEX [IX_dbo_Category_ParentCategoryId] ON [dbo].[Category] 
 (
  [ParentCategoryId] ASC
 )
GO
