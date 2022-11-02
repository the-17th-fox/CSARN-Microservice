CREATE PROCEDURE [dbo].[proc_Replies_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	SELECT Id, Header, AccountId, ReportId, WasRead
	FROM Replies

	WHERE @Id = Id
END