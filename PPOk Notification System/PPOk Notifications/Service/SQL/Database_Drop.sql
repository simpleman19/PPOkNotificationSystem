﻿IF DB_ID('PPOK') IS NOT NULL 
BEGIN
	ALTER DATABASE [PPOK] SET single_user with rollback immediate 
	DROP DATABASE [PPOK]
END