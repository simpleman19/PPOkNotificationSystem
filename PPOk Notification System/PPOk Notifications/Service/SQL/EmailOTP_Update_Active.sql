UPDATE [PPOK].[dbo].[emailotp]
SET
	[notification_id] = @NotificationId, 
	[emailotp_time] = @Time, 
	[emailotp_code] = @EmailOtpCode
WHERE [emailotp].[emailotp_id] = @EmailOtpId
	AND [emailotp].[object_active] = 1