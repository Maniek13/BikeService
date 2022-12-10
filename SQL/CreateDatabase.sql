CREATE DATABASE bikeServiceDB
GO

USE bikeServiceDB
GO

CREATE TABLE app(
	appID int NOT NULL PRIMARY KEY,
	name nvarchar(max) NOT NULL,
	appKey nvarchar(max) NOT NULL
)

CREATE TABLE users(
	userID int NOT NULL PRIMARY KEY,
	name nvarchar(max) NOT NULL,
	password nvarchar(max) NOT NULL,
	appID int NOT NULL
)

CREATE TABLE tasks(
	taskID int NOT NULL PRIMARY KEY,
	appID int NOT NULL,
	header nvarchar(max) NOT NULL,
	description nvarchar(max) NOT NULL,
	state int NOT NULL
)

GO
