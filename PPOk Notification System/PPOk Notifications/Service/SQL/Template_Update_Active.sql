UPDATE [PPOK].[dbo].[template]
SET
	[pharmacy_id] = @pharmacy_id, 
	[template_email] = @template_email, 
	[template_text] = @template_text, 
	[template_phone] = @template_phone
WHERE [template].[template_id] = @template_id
	AND [template].[object_active] = 1