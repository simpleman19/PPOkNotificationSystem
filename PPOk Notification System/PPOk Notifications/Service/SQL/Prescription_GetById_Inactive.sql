SELECT [prescription].*
FROM [PPOK].[dbo].[prescription]
WHERE [prescription].[prescription_id] = @prescription_id
	AND [prescription].[object_active] = 0