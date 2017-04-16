SELECT [otp].*
FROM [PPOK].[dbo].[emailotp]
WHERE [otp].[otp_code] = @otp_code
	AND [otp].[object_active] = 0