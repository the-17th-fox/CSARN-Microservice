CREATE PROCEDURE [dbo].[proc_ReportsClassifications_Create]
	@ReportId uniqueidentifier,
	@ClassificationId uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO ReportsClassifications (
		ReportId,
		ClassificationId)
	
	VALUES (
		@ReportId,
		@ClassificationId)
END