SELECT [notification].*
FROM [PPOK].[dbo].[notification], [PPOK].[dbo].[patient]
WHERE [notification].[patient_id] = [patient].[patient_id]
	AND [notification].[notification_time] >= NOW()