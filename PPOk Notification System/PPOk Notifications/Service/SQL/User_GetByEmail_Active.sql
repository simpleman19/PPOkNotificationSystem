SELECT [user].*
FROM [PPOK].[dbo].[user]
WHERE [user].[user_email] = @user_email
	AND [user].[object_active] = 1