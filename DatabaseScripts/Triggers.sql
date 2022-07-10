USE SeaBattle

GO

CREATE TRIGGER Users_INSERT_Full
ON Users
AFTER INSERT
AS
INSERT INTO UserStatistics (UserStatisticID)
SELECT UserID FROM inserted;