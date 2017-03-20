UPDATE [PPOK].[dbo].[refill]
SET
	[object_active] = 0
WHERE [refill].[refill_id] = @refill_id