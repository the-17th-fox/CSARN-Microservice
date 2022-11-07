CREATE PROCEDURE [dbo].[proc_Reports_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		Reports.Id, 
		Reports.Header, 
		Reports.Body, 
		Reports.AccountId,
		Reports.StatusId,
		Replies.Id AS ReplyId,
		RepClass.ClassificationId
	
	FROM Reports

	LEFT JOIN ReportsClassifications AS RepClass
	ON RepClass.ReportId = Reports.Id

	LEFT JOIN Replies
	ON Replies.ReportId = Reports.Id

	WHERE Reports.Id = @Id
END

