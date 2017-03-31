IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[user] 
	WHERE [user_id] = @UserId 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[user] ON
INSERT INTO [PPOK].[dbo].[user]
(
	[user_id],
	[user_type],
	[user_fname],
	[user_lname],
	[user_email],
	[object_active]
)
VALUES
(
	@UserId,
	@UserType,
	@UserFname,
	@UserLname,
	@UserEmail,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[user] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[user]
SET
	[user_type] = @UserType,
	[user_fname] = @UserFname,
	[user_lname] = @UserLname,
	[user_email] = @UserEmail
WHERE [user].[user_id] = @UserId
END
