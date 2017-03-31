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
