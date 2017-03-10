UPDATE [PPOK].[dbo].[user]
SET
	[user_type] = @user_type,
	[user_fname] = @user_fname,
	[user_lname] = @user_lname,
	[user_email] = @user_email
WHERE [user].[user_id] = @user_id