IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[notification] 
	WHERE [notification_id] = @notification_id 
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
	@notification_id,
	@patient_id, 
	@notification_type, 
	@notification_time,
	@notification_response,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[notification] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[notification]
SET
	[patient_id] = @patient_id, 
	[notification_type] = @notification_type, 
	[notification_time] = @notification_time,
	[notification_response] = @notification_response,
	[object_active] = 1
WHERE [notification].[notification_id] = @notification_id
END