IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[login] 
	WHERE [login_id] = @LoginId 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[login] ON
INSERT INTO [PPOK].[dbo].[login]
(
	[login_id], 
	[user_id], 
	[login_hash], 
	[login_salt], 
	[login_token], 
	[object_active]
)
VALUES
(
	@LoginId,
	@UserId,
	@LoginHash,
	@LoginSalt,
	@LoginToken,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[login] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[login]
SET
	[user_id] = @UserId, 
	[login_hash] = @LoginHash, 
	[login_salt] = @LoginSalt, 
	[login_token] = @LoginToken
WHERE [login].[login_id] = @LoginId
END