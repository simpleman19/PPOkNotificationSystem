UPDATE [PPOK].[dbo].[template]
SET
	[object_active] = 0
WHERE [template].[template_id] = @template_id