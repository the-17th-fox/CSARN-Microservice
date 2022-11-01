CREATE PROCEDURE [dbo].[proc_Notifications_Update]
	@Id uniqueidentifier,
	@Organization nvarchar(MAX),
	@Header nvarchar(MAX),
	@Body nvarchar(MAX),
	@AccountId uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE Notifications
	
	SET 
		Organization = @Organization,
		Header = @Header,
		Body = @Body,
		AccountId = @AccountId

	WHERE Id = @Id
	
	--SELECT Id, Organization, Header, Body, AccountId
	--FROM Notifications
	--WHERE Id = @Id
END