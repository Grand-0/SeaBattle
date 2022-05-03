USE SeaBattle

GO

CREATE PROCEDURE InsertNewUser
	@Login NVARCHAR(20),
	@PasswordHash VARBINARY,
	@PasswordSalt UNIQUEIDENTIFIER,
	@Email NVARCHAR(30),
	@Logo NVARCHAR(50)
AS
	INSERT INTO Users([Login], PasswordHash, PasswordSalt, Email, Logo)
	VALUES (@Login, @PasswordHash, @PasswordSalt, @Email, @Logo);

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
	@PasswordHash VARBINARY,
	@PasswordSalt UNIQUEIDENTIFIER
AS
	UPDATE Users
	SET PasswordHash = @PasswordHash,
	PasswordSalt = @PasswordSalt
	WHERE UserID = @ID

GO
