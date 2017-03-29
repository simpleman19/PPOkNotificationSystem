SELECT [patient].*
FROM [PPOK].[dbo].[patient]
WHERE [patient].[user_id] = @user_id
	AND [patient].[object_active] = 0