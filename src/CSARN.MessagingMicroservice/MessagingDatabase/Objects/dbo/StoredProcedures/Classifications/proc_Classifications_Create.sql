CREATE PROCEDURE [dbo].[proc_Classifications_Create]
	@Title nvarchar(MAX)

AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Classifications (Title)
	VALUES (@Title)
END
