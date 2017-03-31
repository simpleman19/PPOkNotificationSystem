IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[refill] 
	WHERE [refill_id] = @refill_id 
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
	@prescription_id, 
	@refill_date, 
	@refill_filled, 
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[refill] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[refill]
SET
	[prescription_id] = @prescription_id, 
	[refill_date] = @refill_date, 
	[refill_filled] = @refill_filled
WHERE [refill].[refill_id] = @refill_id
END