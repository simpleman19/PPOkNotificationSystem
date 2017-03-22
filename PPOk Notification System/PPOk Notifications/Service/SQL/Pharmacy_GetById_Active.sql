SELECT [pharmacy].*
FROM [PPOK].[dbo].[pharmacy]
WHERE [pharmacy].[pharmacy_id] = @pharmacy_id
	AND [pharmacy].[object_active] = 1