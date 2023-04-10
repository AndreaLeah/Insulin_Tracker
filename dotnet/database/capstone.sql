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
	CONSTRAINT FK_user_id FOREIGN KEY (user_id) REFERENCES users(user_id)
)

-- Populate default data for testing: user and admin with password of 'password'
-- These values should not be kept when going to Production
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT INTO profiles (user_id, basal_rate, min_blood_sugar, max_blood_sugar, carb_ratio, correction_ratio, insulin_type, insulin_strength) VALUES (1, 0.975, 70, 130, 10, 35, 'Humalog', 100)
GO

Select * from users
SELECT * from profiles
