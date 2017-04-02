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
	[preference_contact], 
	[preference_time], 
	[object_active]
)
VALUES
( 
	@PatientId,
	@PharmacyId, 
	@UserId, 
	@DateOfBirth, 
	@ContactMethod, 
	@PreferedContactTime, 
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
	[patient_dob] = @DateOfBirth,  
	[preference_contact] = @ContactMethod, 
	[preference_time] = @PreferedContactTime
WHERE [patient].[patient_id] = @PatientId
END
