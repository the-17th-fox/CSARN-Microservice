CREATE PROCEDURE [dbo].[proc_Classification_Add]
	@Title nvarchar(MAX)

AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Classifications (Title)
	VALUES (@Title)
END
