SELECT [refill].*
FROM [PPOK].[dbo].[refill], [PPOK].[dbo].[prescription], [PPOK].[dbo].[patient]
WHERE [refill].[prescription_id] = [prescription].[prescription_id]
	AND [prescription].[patient_id] = [patient].[patient_id]
	AND [patient].[pharmacy_id] = @pharmacy_id
	AND [refill].[refill_refill] = 1