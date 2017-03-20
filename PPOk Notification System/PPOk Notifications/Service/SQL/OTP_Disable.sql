UPDATE [PPOK].[dbo].[otp]
SET
	[object_active] = 0
WHERE [otp].[otp_id] = @otp_id