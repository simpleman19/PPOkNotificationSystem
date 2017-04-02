INSERT INTO [PPOK].[dbo].[refill]
(
	[prescription_id], 
	[refill_date], 
	[refill_filled], 
	[object_active]
)
VALUES
(
	@PrescriptionId, 
	@RefillDate, 
	@Refilled, 
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)