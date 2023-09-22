CREATE PROCEDURE ConfirmSignIn
    @username VARCHAR(20),
    @password varchar(20)
AS
BEGIN

SET NOCOUNT ON

IF EXISTS(SELECT * FROM [user] WHERE username = @username AND password = @password)
    SELECT 'true' AS UserExists
ELSE
    SELECT 'false' AS UserExists

END