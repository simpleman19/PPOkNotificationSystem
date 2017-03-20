INSERT INTO [PPOK].[dbo].[login]
(
	[user_id], 
	[login_hash], 
	[login_salt], 
	[login_token], 
	[object_active]
)
VALUES
(
	@user_id,
	@login_hash,
	@login_salt,
	@login_token,
	1
)