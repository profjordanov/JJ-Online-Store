/***************************************************************************************
								Basic Initializing
****************************************************************************************/

-- Categories

INSERT INTO [dbo].[Categories]
           ([CreatedOn]
           ,[ModifiedOn]
           ,[IsDeleted]
           ,[DeletedOn]
           ,[Name])
     VALUES
           (GETDATE ( )
           ,GETDATE ( )
           ,0
           ,null
           ,'HATS');

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


