CREATE DATABASE SeaBattle

GO

USE SeaBattle;

GO

CREATE TABLE Users (
	UserID INT IDENTITY(1,1) NOT NULL,
	[Login] NVARCHAR(20) UNIQUE NOT NULL,
	[PasswordHash] NVARCHAR(255) NOT NULL, 
	[PasswordSalt] UNIQUEIDENTIFIER NOT NULL,
	Email NVARCHAR(30) NOT NULL,
	Logo NVARCHAR(50) NULL,
	CONSTRAINT PK_Users_UserID PRIMARY KEY CLUSTERED(UserID),
	CONSTRAINT UQ_Users_Login UNIQUE([Login]),
	CONSTRAINT UQ_Users_Email UNIQUE(Email)
)

GO

CREATE TABLE UserStatistics (
	UserStatisticID INT NOT NULL,
	Battles INT DEFAULT 0,
	WinBattles INT DEFAULT 0,
	WinRate FLOAT DEFAULT 0,
	CONSTRAINT PK_UserStatistics_UserStatisticsID PRIMARY KEY CLUSTERED(UserStatisticID),
	CONSTRAINT FK_UserStatistics_To_Users_UserStatisticsID FOREIGN KEY (UserStatisticID) REFERENCES Users(UserID)
)

GO

CREATE TABLE BattleShipTypes (
	BattleShipTypeID INT IDENTITY(1,1) NOT NULL,
	BattleShipTypeName NVARCHAR(30) NOT NULL,
	BattleShipHealth INT NOT NULL,
	CONSTRAINT PK_BattleShipTypes_BattleShipTypeID PRIMARY KEY CLUSTERED(BattleShipTypeID),
	CONSTRAINT UQ_BattleShipTypes_BattleShipTypeName UNIQUE(BattleShipTypeName)
)

GO

CREATE TABLE Nations (
	NationID INT IDENTITY(1,1) NOT NULL,
	NationName NVARCHAR(30) NOT NULL,
	NationLogo NVARCHAR(40) NOT NULL,
	CONSTRAINT PK_Nations_NationID PRIMARY KEY CLUSTERED(NationID),
	CONSTRAINT UQ_Nations_NationName UNIQUE(NationName)
)

GO

CREATE TABLE BattleShips (
	BattleShipID INT IDENTITY(1,1) NOT NULL,
	ShipName NVARCHAR(30) NOT NULL,
	ShipHealth INT NOT NULL,
	ShipImage NVARCHAR(40) NOT NULL,
	NationID INT NOT NULL,
	BattleShipTypeID INT NOT NULL,
	CONSTRAINT PK_BattleShips_BattleShipID PRIMARY KEY CLUSTERED(BattleShipID),
	CONSTRAINT UQ_BattleShips_ShipName UNIQUE(ShipName),
	CONSTRAINT FK_BattleShips_To_BattleShipTypes_BattleShipTypeID FOREIGN KEY (BattleShipTypeID) REFERENCES BattleShipTypes(BattleShipTypeID),
	CONSTRAINT FK_BattleShips_To_Nations_NationID FOREIGN KEY (NationID) REFERENCES Nations(NationID)
)

GO

