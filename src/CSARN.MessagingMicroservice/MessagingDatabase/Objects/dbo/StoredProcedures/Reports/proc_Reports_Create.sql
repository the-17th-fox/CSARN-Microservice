CREATE PROCEDURE [dbo].[proc_Reports_Create]
	@Header nvarchar(MAX),
	@Body nvarchar(MAX),
	@AccountId uniqueidentifier,
	@StatusId int

AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO Reports(
			Header,
			Body,
			AccountId,
			StatusId)

		VALUES (
			@Header,
			@Body,
			@AccountId,
			@StatusId)
END
