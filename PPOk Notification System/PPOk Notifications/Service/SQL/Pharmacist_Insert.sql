INSERT INTO [PPOK].[dbo].[pharmacist]
(
	[user_id], 
	[pharmacy_id], 
	[pharmacist_admin], 
	[object_active]
)
VALUES
(
	@UserId,
	@PharmacyId,
	@PharmacistAdmin,
	1
)