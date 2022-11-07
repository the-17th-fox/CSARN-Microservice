CREATE PROCEDURE [dbo].[proc_Replies_GetAll]
	@PageNum int,
	@PageSize int
AS
BEGIN
	SET NOCOUNT ON
	SELECT 
		R.Id, 
		R.Header, 
		R.Body, 
		R.AccountId, 
		R.WasRead,
		R.ReportId
	
	FROM Replies AS R

	ORDER BY R.Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END