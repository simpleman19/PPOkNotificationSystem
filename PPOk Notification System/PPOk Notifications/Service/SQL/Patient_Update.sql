﻿UPDATE [PPOK].[dbo].[patient]
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