IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[template] 
	WHERE [template_id] = @template_id 
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
	@template_id,
	@pharmacy_id, 
	@template_email, 
	@template_text, 
	@template_phone,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[template] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[template]
SET
	[pharmacy_id] = @pharmacy_id, 
	[template_email] = @template_email, 
	[template_text] = @template_text, 
	[template_phone] = @template_phone
WHERE [template].[template_id] = @template_id
END