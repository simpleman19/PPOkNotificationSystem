INSERT INTO [PPOK].[dbo].[otp]
( 
	[user_id], 
	[otp_time], 
	[otp_code], 
	[object_active]
)
VALUES
(
	@UserId,
	@Time,
	@Code,
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)