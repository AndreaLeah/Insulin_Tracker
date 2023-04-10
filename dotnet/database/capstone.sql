USE master
GO

-- Drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

-- Create Tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE profiles (
	profile_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	basal_rate decimal(4,3) NOT NULL,
	min_blood_sugar int NOT NULL,
	max_blood_sugar int NOT NULL,
	carb_ratio int NOT NULL,
	correction_ratio int NOT NULL,
	insulin_type nvarchar(100),
	insulin_strength int

	CONSTRAINT PK_profile PRIMARY KEY (profile_id)
	CONSTRAINT FK_profiles_user_id FOREIGN KEY (user_id) REFERENCES users(user_id)
)

CREATE TABLE readings (
	reading_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	profile_id int NOT NULL,
	blood_sugar int NOT NULL,
	time datetime NOT NULL

	CONSTRAINT PK_reading PRIMARY KEY (reading_id),
	CONSTRAINT FK_readings_user_id FOREIGN KEY (user_id) REFERENCES users(user_id),
	CONSTRAINT FK_readings_profile_id FOREIGN KEY (profile_id) REFERENCES profiles(profile_id)
)

CREATE TABLE boluses (
	bolus_id int IDENTITY(1,1) NOT NULL,
	reading_id int NOT NULL,
	bolus_amount decimal NOT NULL

	CONSTRAINT PK_bolus PRIMARY KEY (bolus_id),
	CONSTRAINT FK_boluses_reading_id FOREIGN KEY (reading_id) REFERENCES readings(reading_id),
)

-- Populate default data for testing: user and admin with password of 'password'
-- These values should not be kept when going to Production
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT INTO profiles (user_id, basal_rate, min_blood_sugar, max_blood_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength) VALUES (1, 0.975, 70, 130, 10, 35, 'Humalog', 100)
INSERT INTO profiles (user_id, basal_rate, min_blood_sugar, max_blood_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength) VALUES (1, 0.975, 70, 130, 10, 35, 'Humalog', 100)
INSERT INTO readings (user_id, profile_id, blood_sugar, time) VALUES (1, 1, 120, '2023-04-10 11:23:55.444');
INSERT INTO boluses (reading_id, bolus_amount) VALUES (1, 5);

GO

Select * from users
SELECT * from profiles
select * from readings
select * from boluses

select bolus_id, u.user_id, bolus_amount, r.reading_id, p.profile_id, blood_sugar, time, basal_rate, min_blood_sugar, max_blood_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength
from boluses b
INNER JOIN readings r ON r.reading_id = b.reading_id
INNER JOIN profiles p ON p.profile_id = r.profile_id
INNER JOIN users u ON u.user_id = p.user_id
WHERE
b.bolus_id = 1

SELECT b.bolus_id, r.reading_id, bolus_amount
FROM users u
INNER JOIN readings r ON r.user_id = u.user_id
INNER JOIN boluses b ON b.reading_id = r.reading_id
WHERE r.reading_id = 1