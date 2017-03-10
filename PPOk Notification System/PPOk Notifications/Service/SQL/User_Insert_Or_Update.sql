IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[user] 
	WHERE [user_id] = @user_id 
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
	@user_id,
	@user_type,
	@user_fname,
	@user_lname,
	@user_email,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[user] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[user]
SET
	[user_type] = @user_type,
	[user_fname] = @user_fname,
	[user_lname] = @user_lname,
	[user_email] = @user_email,
	[object_active] = 1
WHERE [user].[user_id] = @user_id
END