SELECT [prescription].*
FROM [PPOK].[dbo].[prescription]
WHERE [prescription].[patient_id] = @patient_id