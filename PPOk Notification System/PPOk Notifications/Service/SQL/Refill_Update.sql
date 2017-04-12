UPDATE [PPOK].[dbo].[refill]
SET
	[prescription_id] = @PrescriptionId, 
	[refill_date] = @RefillDate, 
	[refill_filled] = @Refilled,
	[refill_refill] = @RefillIt
WHERE [refill].[refill_id] = @RefillId
