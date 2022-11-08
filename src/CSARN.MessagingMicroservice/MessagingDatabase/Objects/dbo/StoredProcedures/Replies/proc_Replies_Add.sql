CREATE PROCEDURE [dbo].[proc_Replies_Add]
	@Header nvarchar(MAX),
	@Body nvarchar(MAX),
	@AuthorId uniqueidentifier,
	@ReportId uniqueidentifier,
	@WasRead bit

AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Replies(
			Header,
			Body,
			AccountId,
			ReportId,
			WasRead)
		VALUES (
			@Header,
			@Body,
			@AuthorId,
			@ReportId,
			@WasRead)
END