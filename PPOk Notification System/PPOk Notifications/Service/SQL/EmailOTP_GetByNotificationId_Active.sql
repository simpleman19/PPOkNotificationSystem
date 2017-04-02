SELECT [emailotp].*
FROM [PPOK].[dbo].[emailotp]
WHERE [emailotp].[notification_id] = @notification_id
	AND [emailotp].[object_active] = 1