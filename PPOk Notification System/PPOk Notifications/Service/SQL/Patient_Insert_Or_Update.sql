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
	[person_code],
	[pharmacy_id], 
	[user_id], 
	[patient_dob], 
	[preference_contact], 
	[preference_time],
	[send_refill_message],
	[send_birthday_message],
	[object_active]
)
VALUES
( 
	@PersonCode,
	@PharmacyId, 
	@UserId, 
	@DateOfBirth, 
	@ContactMethod, 
	@PreferedContactTime,
	@SendRefillMessage,
	@SendBirthdayMessage,
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
	[preference_time] = @PreferedContactTime,
	[send_refill_message] = @SendRefillMessage,
	[send_birthday_message] = @SendBirthdayMessage
WHERE [patient].[patient_id] = @PatientId
END
