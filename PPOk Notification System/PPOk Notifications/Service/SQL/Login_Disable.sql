UPDATE [PPOK].[dbo].[login]
SET
	[object_active] = 0
WHERE [login].[login_id] = @login_id