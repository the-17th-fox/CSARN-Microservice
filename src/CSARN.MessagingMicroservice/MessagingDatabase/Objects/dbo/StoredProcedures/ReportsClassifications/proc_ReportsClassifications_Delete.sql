CREATE PROCEDURE [dbo].[proc_ReportsClassifications_Delete]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	DELETE
	FROM ReportsClassifications

	WHERE Id = @Id
END