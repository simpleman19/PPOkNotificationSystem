SELECT [refill].*
FROM [PPOK].[dbo].[refill]
WHERE [refill].[prescription_id] = @prescription_id
	AND [refill].[object_active] = 0