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
	@IsAdmin,
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)