CREATE TRIGGER TR_Audit_Products ON [dbo].[Product]
    FOR INSERT, UPDATE, DELETE
AS
BEGIN
        INSERT INTO [dbo].[Journal] (
            JournalId,
            Description ,
            Type ,
            CreatedAt
        ) VALUES (
            NEWID(),
            'Product was changed',
            'Update',
            GETDATE()
        )
END
GO