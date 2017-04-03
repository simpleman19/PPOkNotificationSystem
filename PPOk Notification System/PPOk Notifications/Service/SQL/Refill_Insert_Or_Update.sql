IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[refill] 
	WHERE [refill_id] = @RefillId 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[refill] ON
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
SET IDENTITY_INSERT [PPOK].[dbo].[refill] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[refill]
SET
	[prescription_id] = @PrescriptionId, 
	[refill_date] = @RefillDate, 
	[refill_filled] = @Refilled,
	[refill_it] = @RefillIt
WHERE [refill].[refill_id] = @RefillId
END
