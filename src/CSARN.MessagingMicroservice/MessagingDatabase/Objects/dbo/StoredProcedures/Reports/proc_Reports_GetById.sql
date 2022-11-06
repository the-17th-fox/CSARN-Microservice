CREATE PROCEDURE [dbo].[proc_Reports_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		RP.Id, 
		RP.Header, 
		RP.Body, 
		RP.AccountId, 
		RP.ReplyId,
		RP.StatusId,
		RPC.ClassificationId
	
	FROM Reports AS RP

	LEFT JOIN ReportsClassifications AS RPC
	ON RP.Id = RPC.ReportId

	WHERE @Id = RP.Id
END

