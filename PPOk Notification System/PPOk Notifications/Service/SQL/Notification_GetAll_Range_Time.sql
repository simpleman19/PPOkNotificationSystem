SELECT [notification].*
FROM [PPOK].[dbo].[notification]
WHERE [notification].[notification_time] >= @BeginDate
	AND [notification].[notification_time] < DATEADD(DATEDIFF(0, @EndDate), 1)