CREATE PROCEDURE [dbo].[proc_Reports_Update]
	@Id uniqueidentifier,
	@Header nvarchar(MAX),
	@Body nvarchar(MAX),
	@AccountId uniqueidentifier,
	@StatusId int

AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE Reports
	
	SET 
		Header = @Header,
		Body = @Body,
		AccountId = @AccountId,
		StatusId = @StatusId

	WHERE Id = @Id
END
