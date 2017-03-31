IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[pharmacy] 
	WHERE [pharmacy_id] = @PharmacyId 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[pharmacy] ON
INSERT INTO [PPOK].[dbo].[pharmacy]
(
	[pharmacy_id],
	[pharmacy_name], 
	[pharmacy_phone], 
	[pharmacy_address], 
	[template_refill], 
	[template_ready], 
	[template_recall], 
	[template_birthday], 
	[object_active]
)
VALUES
(
	@PharmacyId,
	@PharmacyName,
	@PharmacyPhone,
	@PharmacyAddress,
	@TemplateRefillId,
	@TemplateReadyId,
	@TemplateRecallId,
	@TemplateBirthdayId,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[pharmacy] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[pharmacy]
SET
	[pharmacy_name] = @PharmacyName, 
	[pharmacy_phone] = @PharmacyPhone, 
	[pharmacy_address] = @PharmacyAddress, 
	[template_refill] = @TemplateRefillId, 
	[template_ready] = @TemplateReadyId, 
	[template_recall] = @TemplateRecallId, 
	[template_birthday] = @TemplateBirthdayId
WHERE [pharmacy].[pharmacy_id] = @PharmacyId
END
