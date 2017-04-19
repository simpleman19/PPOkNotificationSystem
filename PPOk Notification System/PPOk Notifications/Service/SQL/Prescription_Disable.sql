UPDATE [PPOK].[dbo].[prescription]
SET
	[object_active] = 0
WHERE [prescription].[prescription_id] = @PrescriptionId