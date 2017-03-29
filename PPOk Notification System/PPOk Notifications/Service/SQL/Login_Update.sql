UPDATE [PPOK].[dbo].[login]
SET
	[user_id] = @user_id, 
	[login_hash] = @login_hash, 
	[login_salt] = @login_salt, 
	[login_token] = @login_token
WHERE [login].[login_id] = @login_id