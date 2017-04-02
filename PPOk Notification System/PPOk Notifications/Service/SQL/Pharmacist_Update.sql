UPDATE [PPOK].[dbo].[pharmacist]
SET
	[user_id] = @UserId, 
	[pharmacy_id] = @PharmacyId, 
	[pharmacist_admin] = @PharmacistAdmin
WHERE [pharmacist].[pharmacist_id] = @PharmacistId