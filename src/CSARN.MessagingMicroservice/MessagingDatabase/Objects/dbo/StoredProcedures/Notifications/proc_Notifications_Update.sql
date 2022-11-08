CREATE PROCEDURE [dbo].[proc_Notifications_Update]
	@Id uniqueidentifier,
	@Organization nvarchar(MAX),
	@Header nvarchar(MAX),
	@Body nvarchar(MAX),
	@AccountId uniqueidentifier,
	@TargetAccountId uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE Notifications
	
	SET 
		Organization = @Organization,
		Header = @Header,
		Body = @Body,
		AccountId = @AccountId,
		TargetAccountId = @TargetAccountId

	WHERE Id = @Id
END