CREATE DATABASE bikeServiceDB
GO

USE bikeServiceDB
GO

CREATE TABLE app(
	appID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	name NVARCHAR(max) NOT NULL,
	appKey NVARCHAR(10) NOT NULL
)

CREATE TABLE users(
	userID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	login NVARCHAR(max) NOT NULL,
	password NVARCHAR(max) NOT NULL,
	appID INT NOT NULL
)

CREATE TABLE tasks(
	taskID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	appID INT NOT NULL,
	header NVARCHAR(max) NOT NULL,
	description NVARCHAR(max) NOT NULL,
	state INT  DEFAULT 0,
	taskIDKey NVARCHAR(MAX),
	initDate DATETIME
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
		SET taskIDKey = CAST(@taskID as nvarchar) + @appKey + CAST(@nr as nvarchar), initDate = GETDATE()
		WHERE taskID = @taskID
	END
