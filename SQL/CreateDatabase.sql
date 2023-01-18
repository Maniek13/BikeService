CREATE DATABASE bikeServiceDB
GO

USE bikeServiceDB
GO

CREATE TABLE app(
	appID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	name nvarchar(max) NOT NULL,
	appKey nvarchar(10) NOT NULL
)

CREATE TABLE users(
	userID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	login nvarchar(max) NOT NULL,
	password nvarchar(max) NOT NULL,
	appID int NOT NULL
)

CREATE TABLE tasks(
	taskID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	appID int NOT NULL,
	header nvarchar(max) NOT NULL,
	description nvarchar(max) NOT NULL,
	state int  DEFAULT 0,
	taskIDKey nvarchar(MAX) 
)

GO

CREATE TRIGGER add_task
ON tasks
FOR INSERT
AS
	DECLARE @IsActualeSet NVARCHAR(MAX) =  ISNULL(0, (SELECT taskIDKey FROM inserted))
	
	IF @IsActualeSet = 0
	BEGIN
		DECLARE @taskID int = (SELECT taskID FROM inserted)
		DECLARE @appID int = (SELECT appID FROM inserted)
		DECLARE @appKey NVARCHAR(MAX) = (SELECT appKey FROM app WHERE appID = @appID)
		DECLARE @nr int = (SELECT ABS(CHECKSUM(NEWID()) % (1000000 - 1 + 1)) + 1)
	
		UPDATE tasks
		SET taskIDKey = CAST(@taskID as nvarchar) + @appKey + CAST(@nr as nvarchar)
		WHERE taskID = @taskID
	END
