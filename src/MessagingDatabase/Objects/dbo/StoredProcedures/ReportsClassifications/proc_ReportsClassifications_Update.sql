CREATE PROCEDURE [dbo].[proc_ReportsClassifications_Update]
	@Id uniqueidentifier,
	@ReportId uniqueidentifier,
	@ClassificationId uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON

	UPDATE ReportsClassifications

	SET 
		ReportId = @ReportId,
		ClassificationId = @ClassificationId

	WHERE Id = @Id
END