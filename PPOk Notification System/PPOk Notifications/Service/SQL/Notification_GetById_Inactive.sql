SELECT [notification].*
FROM [PPOK].[dbo].[notification]
WHERE [notification].[notification_id] = @notification_id
	AND [notification].[object_active] = 0