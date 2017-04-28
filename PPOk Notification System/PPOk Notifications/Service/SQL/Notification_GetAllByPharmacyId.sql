SELECT [notification].*
FROM [PPOK].[dbo].[notification], [PPOK].[dbo].[patient]
WHERE [notification].[patient_id] = [patient].[patient_id]
	AND [patient].[pharmacy_id] = @pharmacy_id