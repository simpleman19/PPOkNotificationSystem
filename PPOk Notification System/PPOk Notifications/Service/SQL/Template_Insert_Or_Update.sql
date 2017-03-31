IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[template] 
	WHERE [template_id] = @TemplateId 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[template] ON
INSERT INTO [PPOK].[dbo].[template]
(
	[template_id],
	[pharmacy_id], 
	[template_email], 
	[template_text], 
	[template_phone], 
	[object_active]
)
VALUES
(
	@TemplateId,
	@PharmacyId, 
	@TemplateEmail, 
	@TemplateText, 
	@TemplatePhone,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[template] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[template]
SET
	[pharmacy_id] = @PharmacyId, 
	[template_email] = @TemplateEmail, 
	[template_text] = @TemplateText, 
	[template_phone] = @TemplatePhone
WHERE [template].[template_id] = @TemplateId
END