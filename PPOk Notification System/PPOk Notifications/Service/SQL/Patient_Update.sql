UPDATE [PPOK].[dbo].[patient]
SET
	[person_code] = @PersonCode,
	[pharmacy_id] = @PharmacyId, 
	[user_id] = @UserId, 
	[patient_dob] = @DateOfBirth, 
	[preference_contact] = @ContactMethod, 
	[preference_time] = @PreferedContactTime,
	[send_refill_message] = @SendRefillMessage,
	[send_birthday_message] = @SendBirthdayMessage
WHERE [patient].[patient_id] = @PatientId