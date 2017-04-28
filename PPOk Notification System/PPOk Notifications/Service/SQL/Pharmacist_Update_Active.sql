UPDATE [PPOK].[dbo].[pharmacist]
SET
	[user_id] = @UserId, 
	[pharmacy_id] = @PharmacyId, 
	[pharmacist_admin] = @IsAdmin
WHERE [pharmacist].[pharmacist_id] = @PharmacistId
	AND [pharmacist].[object_active] = 1