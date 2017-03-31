UPDATE [PPOK].[dbo].[notification]
SET
	[patient_id] = @PatientId, 
	[notification_type] = @NotificationType, 
	[notification_time] = @NotificationTime,
	[notification_response] = @NotificationResponse
WHERE [notification].[notification_id] = @NotificationId
	AND [notification].[object_active] = 1