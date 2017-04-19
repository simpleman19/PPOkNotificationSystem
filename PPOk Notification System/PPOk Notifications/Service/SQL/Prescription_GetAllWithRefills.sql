SELECT r.[refill_id], r.[refill_date], r.[refill_filled], pers.*
FROM [PPOK].[dbo].[refill] r
INNER JOIN [PPOK].[dbo].[prescription] pers ON r.[prescription_id] = pers.[prescription_id]
INNER JOIN [PPOK].[dbo].[patient] pat ON pat.[patient_id] = pers.[patient_id]
INNER JOIN [PPOK].[dbo].[pharmacy] pharm ON pat.[pharmacy_id] = pharm.[pharmacy_id]
WHERE r.[object_active] = 1 AND pharm.[pharmacy_id] = @pharmacy_id