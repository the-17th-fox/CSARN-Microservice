CREATE PROCEDURE [dbo].[proc_Reports_GetAll]
	@PageNum int,
	@PageSize int
AS
BEGIN
	SET NOCOUNT ON
	SELECT 
		RP.Id, 
		RP.Header, 
		RP.Body, 
		RP.AccountId, 
		RP.ReplyId,
		RP.StatusId--,
		--RPC.ClassificationId
	
	FROM Reports AS RP

	--LEFT JOIN ReportsClassifications AS RPC
	--ON RP.Id = RPC.ReportId

	--ORDER BY RP.Id
	--OFFSET (@PageNum - 1) * @PageSize ROWS
	--FETCH NEXT @PageSize ROWS ONLY
END

