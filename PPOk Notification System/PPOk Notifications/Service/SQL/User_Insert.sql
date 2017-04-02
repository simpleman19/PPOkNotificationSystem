INSERT INTO [PPOK].[dbo].[user]
(
	[user_type],
	[user_fname],
	[user_lname],
	[user_email],
	[user_phone],
	[object_active]
)
VALUES
(
	@Type,
	@FirstName,
	@LastName,
	@Email,
	@Phone,
	1
)