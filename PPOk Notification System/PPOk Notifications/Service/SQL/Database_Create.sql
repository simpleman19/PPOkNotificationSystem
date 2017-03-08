CREATE DATABASE [PPOK]
GO

CREATE TABLE [PPOK].[dbo].[user] 
( 
	[user_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[user_type] char, 
	[user_fname] varchar(64) NOT NULL, 
	[user_lname] varchar(64) NOT NULL, 
	[user_email] varchar(255),
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[login]
( 
	[login_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[user_id] bigint FOREIGN KEY REFERENCES [user]([user_id]) NOT NULL, 
	[login_hash] varchar(255) NOT NULL, 
	[login_salt] varchar(32) NOT NULL, 
	[login_token] varchar(255) NOT NULL, 
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[pharmacy] 
( 
	[pharmacy_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[pharmacy_name] varchar(128) NOT NULL, 
	[pharmacy_phone] varchar(10) NOT NULL, 
	[pharmacy_address] varchar(255) NOT NULL, 
	[template_refill] bigint NOT NULL, 
	[template_ready] bigint NOT NULL, 
	[template_recall] bigint NOT NULL, 
	[template_birthday] bigint NOT NULL, 
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[pharmacist] 
( 
	[pharmacist_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[user_id] bigint FOREIGN KEY REFERENCES [user]([user_id]) NOT NULL, 
	[pharmacy_id] bigint FOREIGN KEY REFERENCES [pharmacy]([pharmacy_id]) NOT NULL, 
	[pharmacist_admin] bit NOT NULL, 
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[patient] 
( 
	[patient_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[pharmacy_id] bigint FOREIGN KEY REFERENCES [pharmacy]([pharmacy_id]) NOT NULL, 
	[user_id] bigint FOREIGN KEY REFERENCES [user]([user_id]) NOT NULL, 
	[patient_dob] date NOT NULL, 
	[patient_phone] varchar(10) NOT NULL, 
	[preference_phone] int NOT NULL, 
	[preference_text] int NOT NULL, 
	[preference_email] int NOT NULL, 
	[preference_time] datetime NOT NULL, 
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[prescription] 
( 
	[prescription_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[patient_id] bigint FOREIGN KEY REFERENCES [patient]([patient_id]) NOT NULL, 
	[prescription_name] varchar(128) NOT NULL, 
	[prescription_datefilled] date NOT NULL, 
	[prescription_supply] int NOT NULL,
	[prescription_refills] int NOT NULL, 
	[prescription_upc] varchar(32) NOT NULL, 
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[refill] 
( 
	[refill_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[prescription_id] bigint FOREIGN KEY REFERENCES [prescription]([prescription_id]) NOT NULL, 
	[refill_date] datetime,
	[refill_filled] bit NOT NULL, 
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[notification] 
( 
	[notification_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[patient_id] bigint FOREIGN KEY REFERENCES [patient]([patient_id]) NOT NULL, 
	[notification_type] int NOT NULL, 
	[notification_time] datetime NOT NULL,
	[notification_response] varchar(255), 
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[template] 
( 
	[template_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[pharmacy_id] bigint FOREIGN KEY REFERENCES [pharmacy]([pharmacy_id]) NOT NULL, 
	[template_email] varchar(255) NOT NULL, 
	[template_text] varchar(255) NOT NULL, 
	[template_phone] varchar(255) NOT NULL, 
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[otp] 
( 
	[otp_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[user_id] bigint FOREIGN KEY REFERENCES [user]([user_id]) NOT NULL, 
	[otp_time] datetime NOT NULL, 
	[otp_code] varchar(64) NOT NULL, 
	[object_active] bit NOT NULL
)
GO

CREATE TABLE [PPOK].[dbo].[emailotp] 
( 
	[emailotp_id] bigint PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	[notification_id] bigint FOREIGN KEY REFERENCES [notification]([notification_id]) NOT NULL, 
	[emailotp_time] datetime NOT NULL, 
	[emailotp_code] varchar(64) NOT NULL, 
	[object_active] bit NOT NULL
)
GO