UPDATE [PPOK].[dbo].[emailotp]
SET
	[object_active] = 1
WHERE [emailotp].[emailotp_id] = @emailotp_id