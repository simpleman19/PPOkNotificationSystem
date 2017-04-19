IF NOT EXISTS 
( 
	SELECT 1 
	FROM [PPOK].[dbo].[prescription] 
	WHERE [prescription_id] = @PrescriptionId 
)
BEGIN
SET IDENTITY_INSERT [PPOK].[dbo].[prescription] ON
INSERT INTO [PPOK].[dbo].[prescription]
(
	[prescription_id],
	[patient_id], 
	[prescription_name], 
	[prescription_datefilled], 
	[prescription_supply],
	[prescription_refills], 
	[prescription_upc],
	[object_active]
)
VALUES
(
	@PrescriptionId,
	@PatientId,
	@PrescriptionName,
	@PrescriptionDateFilled,
	@PrescriptionDaysSupply,
	@PrescriptionRefills,
	@PrescriptionUpc,
	1
)
SET IDENTITY_INSERT [PPOK].[dbo].[prescription] OFF
END
ELSE
BEGIN
UPDATE [PPOK].[dbo].[prescription]
SET
	[patient_id] = @PatientId, 
	[prescription_name] = @PrescriptionName, 
	[prescription_datefilled] = @PrescriptionDateFilled,
	[prescription_supply] = @PrescriptionDaysSupply,
	[prescription_refills] = @PrescriptionRefills, 
	[prescription_upc] = @PrescriptionUpc
WHERE [prescription].[prescription_id] = @PrescriptionId
END