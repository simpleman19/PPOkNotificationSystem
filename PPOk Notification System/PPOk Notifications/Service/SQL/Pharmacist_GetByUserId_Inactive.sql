SELECT [pharmacist].*
FROM [PPOK].[dbo].[pharmacist]
WHERE [pharmacist].[user_id] = @user_id
	AND [pharmacist_id].[object_active] = 0