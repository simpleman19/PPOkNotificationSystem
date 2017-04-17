SELECT [pharmacist].*
FROM [PPOK].[dbo].[pharmacist]
WHERE [pharmacist].[pharmacy_id] = @pharmacy_id
	AND [pharmacist].[object_active] = 0