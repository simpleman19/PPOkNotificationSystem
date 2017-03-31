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
	[object_active]
)
VALUES
(
	@PrescriptionId, 
	@RefillDate, 
	@RefillFilled, 
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
	[refill_filled] = @RefillFilled
WHERE [refill].[refill_id] = @RefillId
END