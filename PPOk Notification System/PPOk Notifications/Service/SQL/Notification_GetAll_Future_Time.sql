﻿SELECT [notification].*
FROM [PPOK].[dbo].[notification]
WHERE [notification].[notification_time] >= GETDATE()