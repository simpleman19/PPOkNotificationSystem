UPDATE [PPOK].[dbo].[user]
SET
	[user_type] = @UserType,
	[user_fname] = @UserFname,
	[user_lname] = @UserLname,
	[user_email] = @UserEmail
WHERE [user].[user_id] = @UserId
	AND [user].[object_active] = 0