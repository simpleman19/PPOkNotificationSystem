SELECT [otp].*
FROM [PPOK].[dbo].[otp]
WHERE [otp].[otp_id] = @otp_id
	AND [otp].[object_active] = 0