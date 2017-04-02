SELECT [prescription].*
FROM [PPOK].[dbo].[prescription]
WHERE [prescription].[patient_id] = @patient_id
	AND [prescription].[object_active] = 0