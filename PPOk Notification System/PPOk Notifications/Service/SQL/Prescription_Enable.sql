UPDATE [PPOK].[dbo].[prescription]
SET
	[object_active] = 1
WHERE [prescription].[prescription_id] = @PrescriptionId