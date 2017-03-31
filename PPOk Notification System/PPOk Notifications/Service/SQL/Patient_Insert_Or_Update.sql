IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[patient] 
	WHERE [patient_id] = @patient_id 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[patient] ON
INSERT INTO [PPOK].[dbo].[patient]
(
	[patient_id],
	[pharmacy_id], 
	[user_id], 
	[patient_dob], 
	[patient_phone], 
	[preference_phone], 
	[preference_text], 
	[preference_email], 
	[preference_time], 
	[object_active]
)
VALUES
( 
	@patient_id,
	@pharmacy_id, 
	@user_id, 
	@patient_dob, 
	@patient_phone, 
	@preference_phone, 
	@preference_text, 
	@preference_email, 
	@preference_time, 
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[patient] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[patient]
SET
	[pharmacy_id] = @pharmacy_id, 
	[user_id] = @user_id, 
	[patient_dob] = @patient_dob, 
	[patient_phone] = @patient_phone, 
	[preference_phone] = @preference_phone, 
	[preference_text] = @preference_text, 
	[preference_email] = @preference_email, 
	[preference_time] = @preference_time
WHERE [patient].[patient_id] = @patient_id
END