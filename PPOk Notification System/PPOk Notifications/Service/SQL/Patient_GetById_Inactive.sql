SELECT [patient].*
FROM [PPOK].[dbo].[patient]
WHERE [patient].[patient_id] = @patient_id
	AND [patient].[object_active] = 0