﻿IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[notification] 
	WHERE [notification_id] = @NotificationId 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[notification] ON
INSERT INTO [PPOK].[dbo].[notification]
(
	[notification_id],
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
	@NotificationId,
	@PatientId, 
	@Type,
	@ScheduledTime,
	@Sent,
	@SentTime,
	@NotificationMessage,
	@NotificationResponse,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[notification] OFF
END
ELSE
BEGIN
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
END
