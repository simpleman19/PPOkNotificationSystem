UPDATE [PPOK].[dbo].[pharmacist]
SET
	[object_active] = 0
WHERE [pharmacist].[pharmacist_id] = @pharmacist_id