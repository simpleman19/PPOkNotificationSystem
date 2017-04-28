SELECT [patient].*
FROM [PPOK].[dbo].[patient]
WHERE [patient].[pharmacy_id] = @pharmacy_id
	AND [patient].[object_active] = 0