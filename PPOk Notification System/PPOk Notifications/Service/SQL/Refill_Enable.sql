UPDATE [PPOK].[dbo].[refill]
SET
	[object_active] = 1
WHERE [refill].[refill_id] = @refill_id