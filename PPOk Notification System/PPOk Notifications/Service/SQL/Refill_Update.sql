UPDATE [PPOK].[dbo].[refill]
SET
	[prescription_id] = @PrescriptionId, 
	[refill_date] = @RefillDate, 
	[refill_filled] = @Refilled,
	[refill_it] = @RefillIt
WHERE [refill].[refill_id] = @RefillId
