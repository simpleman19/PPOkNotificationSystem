UPDATE [PPOK].[dbo].[notification]
SET
	[patient_id] = @PatientId, 
	[notification_type] = @Type, 
	[notification_time] = @ScheduledTime,
	[notification_sent] = @Sent,
	[notification_senttime] = @SentTime,
	[notification_message] = @NotificationMessage,
	[notification_response] = @NotificationResponse
WHERE [notification].[notification_id] = @NotificationId
	AND [notification].[object_active] = 0