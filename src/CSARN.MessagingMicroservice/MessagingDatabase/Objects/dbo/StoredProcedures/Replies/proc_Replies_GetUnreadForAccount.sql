CREATE PROCEDURE [dbo].[proc_Replies_GetUnreadForAccount]
	@ReportAuthorId uniqueidentifier,
	@PageNum int,
	@PageSize int
AS
BEGIN
	SET NOCOUNT ON
	SELECT 
		Id, 
		Header, 
		Body, 
		AccountId,
		ReportId

	FROM Replies

	WHERE 
		AccountId = @ReportAuthorId 
		AND WasRead = 0

	ORDER BY Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END
