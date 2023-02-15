USE bikeServiceDB
GO

ALTER TABLE tasks
ALTER COLUMN header NVARCHAR(800)
GO

CREATE INDEX idx_sort
ON tasks (state, initDate, header)
GO