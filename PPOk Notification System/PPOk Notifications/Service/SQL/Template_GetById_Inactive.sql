SELECT [template].*
FROM [PPOK].[dbo].[template]
WHERE [template].[template_id] = @template_id
	AND [template].[object_active] = 0