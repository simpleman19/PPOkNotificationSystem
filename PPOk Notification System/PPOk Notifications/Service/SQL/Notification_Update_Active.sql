UPDATE [PPOK].[dbo].[notification]
SET
	[patient_id] = @patient_id, 
	[notification_type] = @notification_type, 
	[notification_time] = @notification_time,
	[notification_response] = @notification_response
WHERE [notification].[notification_id] = @notification_id
	AND [notification].[object_active] = 1