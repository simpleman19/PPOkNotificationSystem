﻿SELECT [template].*
FROM [PPOK].[dbo].[template]
WHERE [template].[pharmacy_id] = @pharmacy_id
	AND [template].[object_active] = 0