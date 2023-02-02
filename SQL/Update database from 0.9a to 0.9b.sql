USE bikeServiceDB
GO



ALTER TABLE tasks
ADD	initDate DATETIME;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[add_task]
ON [dbo].[tasks]
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
