﻿INSERT INTO [PPOK].[dbo].[pharmacy]
(
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
	@PharmacyName,
	@PharmacyPhone,
	@PharmacyAddress,
	@TemplateRefillId,
	@TemplateReadyId,
	@TemplateRecallId,
	@TemplateBirthdayId,
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)