INSERT INTO [PPOK].[dbo].[emailotp]
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
	@Code,
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)