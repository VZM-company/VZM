INSERT INTO Product
           ([ProductId],[Title],[MetaTitle],[Price],[CreatedAt],[Description],[DescriptionShort],[SellerId])
     VALUES
           (NEWID(),'Title1','MetaTitle1',100,GETDATE(),'Description1','DescriptionShort1','2E94E9A5-21A4-4466-BCB2-33C9E1559E46'),
		   (NEWID(),'Title2','MetaTitle2',100,GETDATE(),'Description2','DescriptionShort2','2E94E9A5-21A4-4466-BCB2-33C9E1559E46'),
		   (NEWID(),'Title3','MetaTitle3',100,GETDATE(),'Description3','DescriptionShort3','2E94E9A5-21A4-4466-BCB2-33C9E1559E46')