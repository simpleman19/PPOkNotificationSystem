INSERT INTO [PPOK].[dbo].[login]
( 
	[notification_id], 
	[emailotp_time], 
	[emailotp_code], 
	[object_active]
)
VALUES
(
	@NotificationId,
	@Time,
	@EmailOtpCode,
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)