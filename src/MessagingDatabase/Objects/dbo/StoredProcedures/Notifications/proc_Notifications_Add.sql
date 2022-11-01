CREATE PROCEDURE [dbo].[proc_Notifications_Add]
	@Organization nvarchar(MAX),
	@Header nvarchar(MAX),
	@Body nvarchar(MAX),
	@AccountId uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Notifications (
			Organization,
			Header,
			Body,
			AccountId)
		VALUES (
			@Organization,
			@Header,
			@Body,
			@AccountId)
	
	--SET @Id = IDENT_CURRENT()

	--SELECT Id, Organization, Header, Body, AccountId
	--FROM Notifications
	--WHERE Id = @Id
END