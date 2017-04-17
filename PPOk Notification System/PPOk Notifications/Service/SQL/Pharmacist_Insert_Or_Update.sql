IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[pharmacist] 
	WHERE [pharmacist_id] = @PharmacistId 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[pharmacist] ON
INSERT INTO [PPOK].[dbo].[pharmacist]
(
	[pharmacist_id],
	[user_id], 
	[pharmacy_id], 
	[pharmacist_admin], 
	[object_active]
)
VALUES
(
	@PharmacistId,
	@UserId,
	@PharmacyId,
	@IsAdmin,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[pharmacist] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[pharmacist]
SET
	[user_id] = @UserId, 
	[pharmacy_id] = @PharmacyId, 
	[pharmacist_admin] = @IsAdmin
WHERE [pharmacist].[pharmacist_id] = @PharmacistId
END