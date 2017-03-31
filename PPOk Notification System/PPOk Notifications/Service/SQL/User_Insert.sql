INSERT INTO [PPOK].[dbo].[user]
(
	[user_type],
	[user_fname],
	[user_lname],
	[user_email],
	[object_active]
)
VALUES
(
	@UserType,
	@UserFname,
	@UserLname,
	@UserEmail,
	1
)