SELECT [otp].*
FROM [PPOK].[dbo].[otp]
WHERE [otp].[otp_code] = @otp_code
	AND [otp].[object_active] = 1