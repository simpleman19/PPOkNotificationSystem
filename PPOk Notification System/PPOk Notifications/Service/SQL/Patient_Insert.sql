INSERT INTO [PPOK].[dbo].[patient]
(
	[pharmacy_id], 
	[user_id], 
	[patient_dob], 
	[preference_contact], 
	[preference_time], 
	[object_active]
)
VALUES
( 
	@PharmacyId, 
	@UserId, 
	@DateOfBirth, 
	@ContactMethod, 
	@PreferedContactTime, 
	1
)
