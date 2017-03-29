UPDATE [PPOK].[dbo].[notification]
SET
	[patient_id] = @patient_id, 
	[notification_type] = @notification_type, 
	[notification_time] = @notification_time,
	[notification_response] = @notification_response,
	[object_active] = 1
WHERE [notification].[notification_id] = @notification_id