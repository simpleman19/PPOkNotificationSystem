SELECT [emailotp].*
FROM [PPOK].[dbo].[emailotp]
WHERE [emailotp].[emailotp_id] = @emailotp_id
	AND [emailotp].[object_active] = 0