IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[login] 
	WHERE [login_id] = @login_id 
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
	@login_id,
	@user_id,
	@login_hash,
	@login_salt,
	@login_token,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[login] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[login]
SET
	[user_id] = @user_id, 
	[login_hash] = @login_hash, 
	[login_salt] = @login_salt, 
	[login_token] = @login_token
WHERE [login].[login_id] = @login_id
END