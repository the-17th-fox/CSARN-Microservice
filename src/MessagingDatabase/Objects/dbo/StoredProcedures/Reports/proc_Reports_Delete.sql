CREATE PROCEDURE [dbo].[proc_Reports_Delete]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	DELETE
	FROM Reports

	WHERE Id = @Id
END
