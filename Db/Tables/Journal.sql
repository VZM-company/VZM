CREATE TABLE [dbo].[Journal]
(
 [JournalId] uniqueidentifier NOT NULL,
 [Type] varchar(50) NOT NULL,
 [Description] varchar(50) NOT NULL,
 [CreatedAt] datetime NOT NULL,
);
GO
