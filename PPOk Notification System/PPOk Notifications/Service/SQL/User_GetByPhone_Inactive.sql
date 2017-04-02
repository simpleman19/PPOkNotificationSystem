SELECT [user].*
FROM [PPOK].[dbo].[user]
WHERE [user].[user_phone] = @user_phone
	AND [user].[object_active] = 0