CREATE PROCEDURE [dbo].[proc_Notifications_Add]
	@Organization nvarchar(MAX),
	@Header nvarchar(MAX),
	@Body nvarchar(MAX),
	@AccountId uniqueidentifier,
	@TargetAccountId uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO Notifications (
			Organization,
			Header,
			Body,
			AccountId,
			TargetAccountId)
		VALUES (
			@Organization,
			@Header,
			@Body,
			@AccountId,
			@TargetAccountId)
END