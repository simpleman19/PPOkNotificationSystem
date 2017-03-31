IF NOT EXISTS 
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
	[notification_response],
	[object_active]
)
VALUES
( 
	@NotificationId,
	@PatientId, 
	@NotificationType, 
	@NotificationTime,
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
	[notification_type] = @NotificationType, 
	[notification_time] = @NotificationTime,
	[notification_response] = @NotificationResponse
WHERE [notification].[notification_id] = @NotificationId
END