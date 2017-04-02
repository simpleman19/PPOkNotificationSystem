UPDATE [PPOK].[dbo].[prescription]
SET
	[patient_id] = @PatientId, 
	[prescription_name] = @PrescriptionName, 
	[prescription_supply] = @PrescriptionDateFilled,
	[prescription_refills] = @PrescriptionRefills, 
	[prescription_upc] = @PrescriptionUpc
WHERE [prescription].[prescription_id] = @PrescriptionId
	AND [prescription].[object_active] = 0