SELECT [notification].*
FROM [PPOK].[dbo].[notification]
WHERE [notification].[notification_time] >= DATEADD(day, DATEDIFF(day, 0, @BeginDate), 0)
	AND [notification].[notification_time] < DATEADD(day, DATEDIFF(day, 0, @EndDate), 1)