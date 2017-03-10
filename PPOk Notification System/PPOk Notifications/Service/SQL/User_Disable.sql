UPDATE [PPOK].[dbo].[user]
SET
	[object_active] = 0
WHERE [user].[user_id] = @user_id