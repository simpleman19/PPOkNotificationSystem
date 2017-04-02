INSERT INTO [PPOK].[dbo].[template]
(
	[pharmacy_id], 
	[template_email], 
	[template_text], 
	[template_phone], 
	[object_active]
)
VALUES
(
	@PharmacyId, 
	@TemplateEmail, 
	@TemplateText, 
	@TemplatePhone,
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)