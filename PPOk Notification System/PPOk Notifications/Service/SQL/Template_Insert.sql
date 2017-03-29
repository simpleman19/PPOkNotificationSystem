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
	@pharmacy_id, 
	@template_email, 
	@template_text, 
	@template_phone,
	1
)