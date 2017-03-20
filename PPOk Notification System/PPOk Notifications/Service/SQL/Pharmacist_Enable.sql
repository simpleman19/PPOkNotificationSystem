UPDATE [PPOK].[dbo].[pharmacist]
SET
	[object_active] = 1
WHERE [pharmacist].[pharmacist_id] = @pharmacist_id