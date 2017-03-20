UPDATE [PPOK].[dbo].[login]
SET
	[object_active] = 1
WHERE [login].[login_id] = @login_id