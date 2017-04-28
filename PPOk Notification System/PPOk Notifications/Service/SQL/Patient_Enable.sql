UPDATE [PPOK].[dbo].[patient]
SET
	[object_active] = 1
WHERE [patient].[patient_id] = @PatientId