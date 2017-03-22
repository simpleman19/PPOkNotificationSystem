UPDATE [PPOK].[dbo].[emailotp]
SET
	[object_active] = 0
WHERE [emailotp].[emailotp_id] = @emailotp_id