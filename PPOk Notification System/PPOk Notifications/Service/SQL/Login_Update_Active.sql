UPDATE [PPOK].[dbo].[login]
SET
	[user_id] = @user_id, 
	[login_hash] = @login_hash, 
	[login_salt] = @login_salt, 
	[login_token] = @login_token, 
	[object_active] = 1
WHERE [login].[login_id] = @login_id
	AND [login].[object_active] = 1