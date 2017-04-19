UPDATE [PPOK].[dbo].[user]
SET
	[object_active] = 1
WHERE [user].[user_id] = @UserId