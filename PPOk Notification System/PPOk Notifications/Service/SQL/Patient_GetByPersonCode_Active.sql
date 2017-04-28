SELECT [patient].*
FROM [PPOK].[dbo].[patient]
WHERE [patient].[person_code] = @person_code
	AND [patient].[pharmacy_id] = @pharmacy_id
	AND [patient].[object_active] = 1