UPDATE [PPOK].[dbo].[notification]
SET
	[object_active] = 0
WHERE [notification].[notification_id] = @notification_id