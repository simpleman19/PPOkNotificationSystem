INSERT INTO [PPOK].[dbo].[notification]
(
	[patient_id], 
	[notification_type], 
	[notification_time],
	[notification_sent],
	[notification_senttime],
	[notification_message],
	[notification_response], 
	[object_active]
)
VALUES
(
	@PatientId, 
	@Type,
	@ScheduledTime,
	@Sent,
	@SentTime,
	@NotificationMessage,
	@NotificationResponse,
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)