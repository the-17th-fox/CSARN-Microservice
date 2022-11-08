CREATE PROCEDURE [dbo].[proc_Replies_GetForReport]
	@ReportId uniqueidentifier,
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
		WasRead,
		ReportId
	
	FROM Replies

	WHERE ReportId = @ReportId

	ORDER BY Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END