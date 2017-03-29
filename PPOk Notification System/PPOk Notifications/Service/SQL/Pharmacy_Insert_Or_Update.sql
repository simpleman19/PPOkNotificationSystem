IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[pharmacy] 
	WHERE [pharmacy_id] = @pharmacy_id 
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
	@pharmacy_id,
	@pharmacy_name,
	@pharmacy_phone,
	@pharmacy_address,
	@template_refill.TemplateId,
	@template_ready.TemplateId,
	@template_recall.TemplateId,
	@template_birthday.TemplateId,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[pharmacy] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[pharmacy]
SET
	[pharmacy_name] = @pharmacy_name, 
	[pharmacy_phone] = @pharmacy_phone, 
	[pharmacy_address] = @pharmacy_address, 
	[template_refill] = @template_refill.TemplateId, 
	[template_ready] = @template_ready.TemplateId, 
	[template_recall] = @template_recall.TemplateId, 
	[template_birthday] = @template_birthday.TemplateId
WHERE [pharmacy].[pharmacy_id] = @pharmacy_id
END