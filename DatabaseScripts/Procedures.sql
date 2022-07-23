USE SeaBattle

GO

CREATE PROCEDURE InsertNewUser
	@Login NVARCHAR(20),
	@PasswordHash NVARCHAR(255),
	@PasswordSalt UNIQUEIDENTIFIER,
	@Email NVARCHAR(30),
	@Logo NVARCHAR(50) = NULL
AS
	INSERT INTO Users([Login], PasswordHash, PasswordSalt, Email, Logo)
	VALUES (@Login, @PasswordHash, @PasswordSalt, @Email, @Logo)

GO

CREATE PROCEDURE CheckLogin
	@Login NVARCHAR(20),
	@ID INT OUTPUT
AS
	SELECT @ID = UserID FROM Users
	WHERE [Login] = @Login

GO

CREATE PROCEDURE CheckEmail
	@Email NVARCHAR(30),
	@ID INT OUTPUT
AS
	SELECT @ID = UserID FROM Users
	WHERE Email = @Email

GO

CREATE PROCEDURE GetUserByID 
	@ID INT
AS
	SELECT * FROM Users
	WHERE UserID = @ID

GO

CREATE PROCEDURE GetFullUserByID
	@ID INT
AS
	SELECT 
	U.UserID, 
	U.Login, 
	U.PasswordHash, 
	U.PasswordSalt, 
	U.Email, 
	U.Logo, 
	US.Battles, 
	US.WinBattles, 
	US.WinRate 
	FROM Users AS U
	JOIN UserStatistics AS US
	ON US.UserStatisticID = U.UserID
	WHERE U.UserID = @ID

GO

CREATE PROCEDURE ChangePassword
	@ID INT,
	@PasswordHash NVARCHAR(255),
	@PasswordSalt UNIQUEIDENTIFIER
AS
	UPDATE Users
	SET PasswordHash = @PasswordHash,
	PasswordSalt = @PasswordSalt
	WHERE UserID = @ID

GO

CREATE PROCEDURE GetUserByLogin
	@Login NVARCHAR(20)
AS
	SELECT * FROM Users
	WHERE Login = @Login

GO

CREATE PROCEDURE GetUserIdByLogin
	@Login Nvarchar(20)
AS
	SELECT UserID FROM Users
	WHERE Login = @Login

GO

CREATE PROCEDURE GetAllNations
AS
	SELECT * FROM Nations

GO

CREATE PROCEDURE GetNationById
	@ID INT
AS
	SELECT * FROM Nations
	WHERE NationID = @ID

GO

CREATE PROCEDURE GetBattleShipTypes
AS
	SELECT * FROM BattleShipTypes

GO

CREATE PROCEDURE GetBattleShipsByNation
	@NationID INT
AS
	SELECT * FROM BattleShips
	WHERE NationID = @NationID

GO

CREATE PROCEDURE UpdateUserEmail
	@ID INT,
	@Email NVARCHAR(30)
AS
	UPDATE Users
	SET [Email] = @Email
	WHERE UserID = @ID

GO

CREATE PROCEDURE UpdateUserLogin
	@ID INT,
	@Login NVARCHAR(20)
AS
	UPDATE Users
	SET [Login] = @Login
	WHERE UserID = @ID

GO

CREATE PROCEDURE UpdateUserLogo
	@ID INT,
	@PathToLogo NVARCHAR(50)
AS
	UPDATE Users
	SET [Logo] = @PathToLogo
	WHERE UserID = @ID

GO

CREATE PROCEDURE UpdateUserLoginAndEmail
	@ID INT,
	@Login NVARCHAR(20),
	@Email NVARCHAR(30)
AS
	UPDATE Users
	SET [Login] = @Login,
	[Email] = @Email
	WHERE UserID = @ID

GO