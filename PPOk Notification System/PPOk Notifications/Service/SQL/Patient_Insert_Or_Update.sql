IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[patient] 
	WHERE [patient_id] = @PatientId 
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
	@PatientId,
	@PharmacyId, 
	@UserId, 
	@PatientDob, 
	@PatientPhone, 
	@PreferencePhone, 
	@PreferenceText, 
	@PreferenceEmail, 
	@PreferenceTime, 
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[patient] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[patient]
SET
	[pharmacy_id] = @PharmacyId, 
	[user_id] = @UserId, 
	[patient_dob] = @PatientDob, 
	[patient_phone] = @PatientPhone, 
	[preference_phone] = @PreferencePhone, 
	[preference_text] = @PreferenceText, 
	[preference_email] = @PreferenceEmail, 
	[preference_time] = @PreferenceTime
WHERE [patient].[patient_id] = @PatientId
END
