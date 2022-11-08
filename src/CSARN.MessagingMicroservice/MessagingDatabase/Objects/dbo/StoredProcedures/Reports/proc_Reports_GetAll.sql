CREATE PROCEDURE [dbo].[proc_Reports_GetAll]
	@PageNum int,
	@PageSize int
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
	ON Reports.Id = RepClass.ReportId

	LEFT JOIN Replies
	ON Reports.Id = Replies.ReportId

	ORDER BY Reports.Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END