INSERT INTO [PPOK].[dbo].[notification]
(
	[patient_id], 
	[notification_type], 
	[notification_time],
	[notification_response],
	[object_active]
)
VALUES
( 
	@patient_id, 
	@notification_type, 
	@notification_time,
	@notification_response,
	1
)