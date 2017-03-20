SELECT [login].*
FROM [PPOK].[dbo].[login]
WHERE [login].[login_id] = @login_id
	AND [login].[object_active] = 0