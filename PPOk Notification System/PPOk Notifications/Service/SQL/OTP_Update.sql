UPDATE [PPOK].[dbo].[otp]
SET
	[user_id] = @UserId, 
	[otp_time] = @Time, 
	[otp_code] = @Code
WHERE [otp].[otp_id] = @Id