IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[emailotp] 
	WHERE [emailotp_id] = @EmailOtpId 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[emailotp] ON
INSERT INTO [PPOK].[dbo].[emailotp]
( 
	[emailotp_id],
	[notification_id], 
	[emailotp_time], 
	[emailotp_code], 
	[object_active]
)
VALUES
(
	@EmailOtpId,
	@NotificationId,
	@Time,
	@EmailOtpCode,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[emailotp] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[emailotp]
SET
	[notification_id] = @NotificationId, 
	[emailotp_time] = @Time, 
	[emailotp_code] = @EmailOtpCode
WHERE [emailotp].[emailotp_id] = @EmailOtpId
END