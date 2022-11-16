CREATE PROCEDURE [dbo].[proc_Classifications_Update]
	@Id uniqueidentifier,
	@Title nvarchar(MAX)

AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE Classifications
	
	SET 
		Title = @Title,
		UpdatedAt = GETDATE()

	WHERE Id = @Id
END
