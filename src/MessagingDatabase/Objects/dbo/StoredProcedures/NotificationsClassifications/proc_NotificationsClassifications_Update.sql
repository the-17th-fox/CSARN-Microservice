CREATE PROCEDURE [dbo].[proc_NotificationsClassifications_Update]
	@Id uniqueidentifier,
	@NotificationId uniqueidentifier,
	@ClassificationId uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON

	UPDATE NotificationsClassifications

	SET 
		NotificationId = @NotificationId,
		ClassificationId = @ClassificationId

	WHERE Id = @Id
END