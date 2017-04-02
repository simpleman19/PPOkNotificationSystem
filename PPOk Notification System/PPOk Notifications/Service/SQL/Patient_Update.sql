UPDATE [PPOK].[dbo].[patient]
SET
	[pharmacy_id] = @PharmacyId, 
	[user_id] = @UserId, 
	[patient_dob] = @DateOfBirth, 
	[preference_contact] = @ContactMethod, 
	[preference_time] = @PreferedContactTime
WHERE [patient].[patient_id] = @PatientId