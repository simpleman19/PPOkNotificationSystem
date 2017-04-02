UPDATE [PPOK].[dbo].[user]
SET
	[user_type] = @Type,
	[user_fname] = @Firstname,
	[user_lname] = @Lastname,
	[user_email] = @Email,
	[user_phone] = @Phone
WHERE [user].[user_id] = @UserId
