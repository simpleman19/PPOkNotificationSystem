SELECT [user].*
FROM [PPOK].[dbo].[user]
WHERE [user].[user_id] = @user_id
	AND [user].[object_active] = 1