SELECT [refill].*
FROM [PPOK].[dbo].[refill]
WHERE [refill].[refill_id] = @refill_id
	AND [refill].[object_active] = 0