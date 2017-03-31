UPDATE [PPOK].[dbo].[refill]
SET
	[prescription_id] = @PrescriptionId, 
	[refill_date] = @RefillDate, 
	[refill_filled] = @RefillFilled
WHERE [refill].[refill_id] = @RefillId