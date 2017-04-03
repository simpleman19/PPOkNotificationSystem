INSERT INTO [PPOK].[dbo].[refill]
(
	[prescription_id], 
	[refill_date], 
	[refill_filled],
	[refill_it],
	[object_active]
)
VALUES
(
	@PrescriptionId, 
	@RefillDate, 
	@Refilled, 
	@RefillIt,
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)