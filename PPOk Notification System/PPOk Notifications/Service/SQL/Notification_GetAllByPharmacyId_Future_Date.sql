﻿SELECT [notification].*
FROM [PPOK].[dbo].[notification], [PPOK].[dbo].[patient]
WHERE [notification].[patient_id] = [patient].[patient_id]
	AND [notification].[notification_time] >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)
	AND [patient].[pharmacy_id] = @pharmacy_id