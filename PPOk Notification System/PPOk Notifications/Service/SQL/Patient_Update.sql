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