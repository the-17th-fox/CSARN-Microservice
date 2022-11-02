CREATE PROCEDURE [dbo].[proc_Replies_Update]
	@Id uniqueidentifier,
	@Header nvarchar(MAX),
	@Body nvarchar(MAX),
	@AccountId uniqueidentifier,
	@WasRead bit

AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE Replies
	
	SET 
		Header = @Header,
		Body = @Body,
		AccountId = @AccountId,
		WasRead = @WasRead

	WHERE Id = @Id
END