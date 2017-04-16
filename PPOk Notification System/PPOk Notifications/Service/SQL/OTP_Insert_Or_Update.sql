IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[otp] 
	WHERE [otp_id] = @Id 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[otp] ON
INSERT INTO [PPOK].[dbo].[otp]
(
	[user_id], 
	[otp_time], 
	[otp_code], 
	[object_active]
)
VALUES
(
	@UserId,
	@Time,
	@Code,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[otp] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[otp]
SET
	[user_id] = @UserId, 
	[otp_time] = @Time, 
	[otp_code] = @Code
WHERE [otp].[otp_id] = @Id
END