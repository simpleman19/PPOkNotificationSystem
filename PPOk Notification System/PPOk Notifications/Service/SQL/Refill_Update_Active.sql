UPDATE [PPOK].[dbo].[refill]
SET
	[prescription_id] = @prescription_id, 
	[refill_date] = @refill_date, 
	[refill_filled] = @refill_filled
WHERE [refill].[refill_id] = @refill_id
	AND	[refill].[object_active] = 1