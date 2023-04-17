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
	min_warning_sugar int NOT NULL,
	max_warning_sugar int NOT NULL,
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
	carbs int NOT NULL,
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

--User 1 profiles
INSERT INTO profiles (user_id, basal_rate, min_blood_sugar, max_blood_sugar, min_warning_sugar, max_warning_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength) VALUES (1, 0.975, 70, 130, 50, 200, 10, 35, 'Humalog', 100)
INSERT INTO profiles (user_id, basal_rate, min_blood_sugar, max_blood_sugar, min_warning_sugar, max_warning_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength) VALUES (1, 0.8, 70, 130, 50, 200, 15, 35, 'Humalog', 100)

--User 2 profiles
INSERT INTO profiles (user_id, basal_rate, min_blood_sugar, max_blood_sugar, min_warning_sugar, max_warning_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength) VALUES (2, 0.6, 80, 120, 50, 200, 20, 50, 'Humalog', 100)
INSERT INTO profiles (user_id, basal_rate, min_blood_sugar, max_blood_sugar, min_warning_sugar, max_warning_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength) VALUES (2, 0.5, 80, 120, 50, 200, 25, 50, 'Humalog', 100)

--User 1 readings
--Profile 1
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 1, 120, 30, GETDATE());
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 1, 130, 30, DateAdd(DD, -1, GETDATE()));
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 1, 120, 30, DateAdd(DD, -2, GETDATE()));
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 1, 130, 30, DateAdd(DD, -3, GETDATE()));
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 1, 120, 30, DateAdd(DD, -6, GETDATE()));
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 1, 130, 30, DateAdd(DD, -13, GETDATE()));
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 1, 120, 30, DateAdd(DD, -29, GETDATE()));
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 1, 130, 30, DateAdd(DD, -40, GETDATE()));
--Profile 2
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 2, 90, 20, '2023-03-10 11:23:55.444');
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (1, 2, 89, 10, '2023-02-10 11:35:55.444');

--User 2 readings
--Profile 1
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (2, 3, 100, 10, '2023-09-10 11:23:55.444');
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (2, 3, 109, 30, '2023-09-10 11:35:55.444');
--Profile 2
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (2, 4, 98, 20, '2023-03-10 11:23:55.444');
INSERT INTO readings (user_id, profile_id, blood_sugar, carbs, time) VALUES (2, 4, 87, 10, '2023-03-10 11:35:55.444');

--User 1 boluses
--Profile 1
INSERT INTO boluses (reading_id, bolus_amount) VALUES (1, 5);
INSERT INTO boluses (reading_id, bolus_amount) VALUES (2, 3);
--Profile 2
INSERT INTO boluses (reading_id, bolus_amount) VALUES (3, 9);
INSERT INTO boluses (reading_id, bolus_amount) VALUES (4, 7);

--User 2 boluses
--Profile 1
INSERT INTO boluses (reading_id, bolus_amount) VALUES (5, 1);
INSERT INTO boluses (reading_id, bolus_amount) VALUES (6, 2);
--Profile 2
INSERT INTO boluses (reading_id, bolus_amount) VALUES (7, 8);
INSERT INTO boluses (reading_id, bolus_amount) VALUES (8, 4);
GO

Select * from users
SELECT * from profiles
select * from readings WHERE time between DateAdd(DD, -180, GETDATE()) and GETDATE()
select * from boluses

