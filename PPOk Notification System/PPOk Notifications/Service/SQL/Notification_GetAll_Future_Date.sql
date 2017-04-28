SELECT [notification].*
FROM [PPOK].[dbo].[notification]
WHERE [notification].[notification_time] >= DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)