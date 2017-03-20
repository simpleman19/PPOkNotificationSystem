UPDATE [PPOK].[dbo].[otp]
SET
	[object_active] = 1
WHERE [otp].[otp_id] = @otp_id