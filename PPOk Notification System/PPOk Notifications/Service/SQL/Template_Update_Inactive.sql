UPDATE [PPOK].[dbo].[template]
SET
	[pharmacy_id] = @PharmacyId, 
	[template_email] = @TemplateEmail, 
	[template_text] = @TemplateText, 
	[template_phone] = @TemplatePhone
WHERE [template].[template_id] = @TemplateId
	AND [template].[object_active] = 0