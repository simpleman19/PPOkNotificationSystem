﻿INSERT INTO [PPOK].[dbo].[patient]
(
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