CREATE PROCEDURE [dbo].[proc_Reports_GetAllByAccount]
	@AccountId uniqueidentifier,
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
	
	FROM Reports AS Reports

	LEFT JOIN ReportsClassifications AS RepClass
	ON Reports.Id = RepClass.ReportId

	LEFT JOIN Replies 
	ON Reports.Id = Replies.ReportId

	WHERE Reports.AccountId = @AccountId

	ORDER BY Reports.Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END