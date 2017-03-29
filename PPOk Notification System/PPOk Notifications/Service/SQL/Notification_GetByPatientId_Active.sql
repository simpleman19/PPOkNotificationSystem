SELECT [notification].*
FROM [PPOK].[dbo].[notification]
WHERE [notification].[patient_id] = @patient_id
	AND [notification].[object_active] = 1