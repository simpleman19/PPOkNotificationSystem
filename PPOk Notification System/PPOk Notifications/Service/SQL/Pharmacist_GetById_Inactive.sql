SELECT [pharmacist].*
FROM [PPOK].[dbo].[pharmacist]
WHERE [pharmacist].[pharmacist_id] = @pharmacist_id
	AND [pharmacist_id].[object_active] = 0