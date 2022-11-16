CREATE PROCEDURE [dbo].[proc_NotificationsClassifications_Create]
	@NotificationId uniqueidentifier,
	@ClassificationId uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO NotificationsClassifications (
		NotificationId,
		ClassificationId)
	
	VALUES (
		@NotificationId,
		@ClassificationId)
END