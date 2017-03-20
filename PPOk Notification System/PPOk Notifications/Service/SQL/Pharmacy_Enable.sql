UPDATE [PPOK].[dbo].[pharmacy]
SET
	[object_active] = 1
WHERE [pharmacy].[pharmacy_id] = @pharmacy_id