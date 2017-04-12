SELECT [patient].*
FROM [PPOK].[dbo].[patient]
WHERE [patient].[person_code] = @person_code
	AND [patient].[object_active] = 0