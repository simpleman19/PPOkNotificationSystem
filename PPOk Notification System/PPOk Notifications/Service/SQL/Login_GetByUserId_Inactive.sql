SELECT [login].*
FROM [PPOK].[dbo].[login]
WHERE [login].[user_id] = @user_id
		AND [login].[object_active] = 0