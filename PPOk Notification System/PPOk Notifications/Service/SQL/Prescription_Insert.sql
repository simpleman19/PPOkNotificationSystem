﻿INSERT INTO [PPOK].[dbo].[prescription]
(
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
	@PatientId,
	@PrescriptionName,
	@PrescriptionDateFilled,
	@PrescriptionDaysSupply,
	@PrescriptionRefills,
	@PrescriptionUpc,
	1
)
SELECT CAST(SCOPE_IDENTITY() as bigint)