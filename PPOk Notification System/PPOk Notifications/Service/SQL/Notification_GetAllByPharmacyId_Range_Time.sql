﻿SELECT [notification].*
FROM [PPOK].[dbo].[notification], [PPOK].[dbo].[patient]
WHERE [notification].[patient_id] = [patient].[patient_id]
	AND [notification].[notification_time] >= @BeginDate
	AND [notification].[notification_time] < DATEADD(DATEDIFF(0, @EndDate), 1)
	AND [patient].[pharmacy_id] = @pharmacy_id