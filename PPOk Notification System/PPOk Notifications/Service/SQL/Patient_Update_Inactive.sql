﻿UPDATE [PPOK].[dbo].[patient]
SET
	[person_code] = @PersonCode,
	[pharmacy_id] = @PharmacyId, 
	[user_id] = @UserId, 
	[patient_dob] = @DateOfBirth, 
	[preference_contact] = @ContactMethod, 
	[preference_time] = @PreferedContactTime
WHERE [patient].[patient_id] = @PatientId
	AND [patient].[object_active] = 0
