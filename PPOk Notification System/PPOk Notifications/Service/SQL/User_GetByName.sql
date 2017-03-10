SELECT [user].*
FROM [PPOK].[dbo].[user]
WHERE [user].[user_fname] = @user_fname
	AND [user].[user_lname] = @user_lname