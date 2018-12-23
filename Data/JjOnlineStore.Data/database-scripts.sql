/***************************************************************************************
								Basic Initializing
****************************************************************************************/

-- Categories

INSERT INTO [dbo].[Categories]
           ([CreatedOn]
           ,[ModifiedOn]
           ,[IsDeleted]
           ,[DeletedOn]
           ,[Name]
           ,[StoreCategory])
     VALUES
           (GETDATE (),
            GETDATE (),
            0,
            NULL,
            'PC',
			2);

INSERT INTO [dbo].[Categories]
           ([CreatedOn]
           ,[ModifiedOn]
           ,[IsDeleted]
           ,[DeletedOn]
           ,[Name]
           ,[StoreCategory])
     VALUES
           (GETDATE (),
            GETDATE (),
            0,
            NULL,
            'All T-shirts',
			4);

INSERT INTO [dbo].[Categories]
           ([CreatedOn]
           ,[ModifiedOn]
           ,[IsDeleted]
           ,[DeletedOn]
           ,[Name]
           ,[StoreCategory])
     VALUES
           (GETDATE (),
            GETDATE (),
            0,
            NULL,
            'Gifts for programmers',
			32);

-- Products

INSERT INTO [dbo].[Products]
           ([CreatedOn]
           ,[ModifiedOn]
           ,[IsDeleted]
           ,[DeletedOn]
           ,[Name]
           ,[Description]
           ,[Price]
           ,[Base64Image]
           ,[IsAvailable]
           ,[Size]
           ,[Color]
           ,[Type]
           ,[Details]
           ,[CategoryId])
     VALUES
           (GETDATE ( )
           ,GETDATE ( )
           ,0
           ,NULL
           ,'CSS Hat'
           ,'Do it like a web developer - with a lot of style!
Hat-pistol for CSS Styles. Seriously! This is the best alternative of StackOverflow!!!'
           ,12
		   , 'img'
		   ,1
           ,1
           ,'white'
           ,1
           ,'approved;very cool;fantastic'
           ,2);

INSERT INTO [dbo].[Products]
           ([CreatedOn]
           ,[ModifiedOn]
           ,[IsDeleted]
           ,[DeletedOn]
           ,[Name]
           ,[Description]
           ,[Price]
           ,[Base64Image]
           ,[IsAvailable]
           ,[Size]
           ,[Color]
           ,[Type]
           ,[Details]
           ,[CategoryId])
     VALUES
           (GETDATE ( )
           ,GETDATE ( )
           ,0
           ,NULL
           ,'Brain content'
           ,'Do it like a web developer - with a lot of style!
Hat-pistol for CSS Styles. Seriously! This is the best alternative of StackOverflow!!!'
           ,12
		   , ''
		   ,1
           ,1
           ,'white'
           ,1
           ,'approved;very cool;fantastic'
           ,2);

INSERT INTO [dbo].[Products]
           ([CreatedOn]
           ,[ModifiedOn]
           ,[IsDeleted]
           ,[DeletedOn]
           ,[Name]
           ,[Description]
           ,[Price]
           ,[Base64Image]
           ,[IsAvailable]
           ,[Size]
           ,[Color]
           ,[Type]
           ,[Details]
           ,[CategoryId])
     VALUES
           (GETDATE ( )
           ,GETDATE ( )
           ,0
           ,NULL
           ,'CSS Is Awesome T-Shirt'
           ,'CSS is awesome and you know it!

This unique t-shirt is a must-have piece for all SoftUni students. Seriously! You can get up to 20% higher exam score just by wearing it.
You can fail your exams as well, but at least you will have a cool t-shirt.'
           ,22.5
		   , 'pic'
		   ,1
           ,1
           ,'white'
           ,1
           ,'approved;very cool;fantastic'
           ,2);

/***************************************************************************************
								TRIGGERS
****************************************************************************************/
CREATE TRIGGER TR_CartItems_DLT 
ON [CartItems] AFTER DELETE AS
BEGIN
	INSERT INTO [dbo].[CartItemsOnDeleteReport]
           ([CreatedOn]
           ,[ModifiedOn]
           ,[ProductId]
           ,[Quantity]
           ,[CartId])
	SELECT GETUTCDATE(),
		    GETUTCDATE(),
			ProductId,
			Quantity,
			CartId
	FROM deleted
END;