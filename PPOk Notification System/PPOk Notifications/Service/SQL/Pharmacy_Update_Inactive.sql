UPDATE [PPOK].[dbo].[pharmacy]
SET
	[pharmacy_name] = @PharmacyName, 
	[pharmacy_phone] = @PharmacyPhone, 
	[pharmacy_address] = @PharmacyAddress, 
	[template_refill] = @TemplateRefill.TemplateId, 
	[template_ready] = @TemplateReady.TemplateId, 
	[template_recall] = @TemplateRecall.TemplateId, 
	[template_birthday] = @TemplateBirthday.TemplateId
WHERE [pharmacy].[pharmacy_id] = @PharmacyId
	AND [pharmacy].[object_active] = 0