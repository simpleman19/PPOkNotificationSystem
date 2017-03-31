INSERT INTO [PPOK].[dbo].[refill]
(
	[prescription_id], 
	[refill_date], 
	[refill_filled], 
	[object_active]
)
VALUES
(
	@prescription_id, 
	@refill_date, 
	@refill_filled, 
	1
)