INSERT INTO [dbo].[User]
           ([UserId],[Name],[Username],[PasswordHash],[Email],[CreatedAt],[Info],[Confirmed],[RoleId])
     VALUES ('2E94E9A5-21A4-4466-BCB2-33C9E1559E46', 'Company', 'Company','123456','company@email.com',GETDATE(),'Info',1,'A095AD38-2A56-4626-AEA1-8369CCA2B21D')