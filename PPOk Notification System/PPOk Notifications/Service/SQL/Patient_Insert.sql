﻿INSERT INTO [PPOK].[dbo].[patient]
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
SELECT CAST(SCOPE_IDENTITY() as bigint)