UPDATE [PPOK].[dbo].[login]
SET
	[user_id] = @UserId, 
	[login_hash] = @LoginHash, 
	[login_salt] = @LoginSalt, 
	[login_token] = @LoginToken
WHERE [login].[login_id] = @LoginId
	AND [login].[object_active] = 1