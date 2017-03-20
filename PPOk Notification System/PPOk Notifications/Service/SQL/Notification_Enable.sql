UPDATE [PPOK].[dbo].[notification]
SET
	[object_active] = 1
WHERE [notification].[notification_id] = @notification_id