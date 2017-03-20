UPDATE [PPOK].[dbo].[pharmacy]
SET
	[object_active] = 0
WHERE [pharmacy].[pharmacy_id] = @pharmacy_id