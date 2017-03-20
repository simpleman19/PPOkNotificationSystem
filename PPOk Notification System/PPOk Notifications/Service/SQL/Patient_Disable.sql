UPDATE [PPOK].[dbo].[patient]
SET
	[object_active] = 0
WHERE [patient].[patient_id] = @patient_id