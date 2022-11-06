CREATE PROCEDURE [dbo].[proc_Classifications_Delete]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	DELETE
	FROM Classifications

	WHERE Id = @Id
END
