﻿UPDATE [PPOK].[dbo].[pharmacy]
SET
	[pharmacy_name] = @pharmacy_name, 
	[pharmacy_phone] = @pharmacy_phone, 
	[pharmacy_address] = @pharmacy_address, 
	[template_refill] = @template_refill.TemplateId, 
	[template_ready] = @template_ready.TemplateId, 
	[template_recall] = @template_recall.TemplateId, 
	[template_birthday] = @template_birthday.TemplateId, 
	[object_active] = 1
WHERE [pharmacy].[pharmacy_id] = @pharmacy_id
	AND [pharmacy].[object_active] = 1