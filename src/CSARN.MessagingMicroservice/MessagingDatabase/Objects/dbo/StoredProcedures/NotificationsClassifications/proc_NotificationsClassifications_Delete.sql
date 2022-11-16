CREATE PROCEDURE [dbo].[proc_NotificationsClassifications_Delete]
	@NotificationId uniqueidentifier,
	@ClassificationId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	DELETE
	FROM NotificationsClassifications

	WHERE NotificationId = @NotificationId 
		AND ClassificationId = @ClassificationId
END