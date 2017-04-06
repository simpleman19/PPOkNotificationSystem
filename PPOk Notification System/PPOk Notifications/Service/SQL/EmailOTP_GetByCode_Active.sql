SELECT [emailotp].*
FROM [PPOK].[dbo].[emailotp]
WHERE [emailotp].[emailotp_code] = @emailotp_code
	AND [emailotp].[object_active] = 1