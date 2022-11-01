CREATE PROCEDURE [dbo].[proc_NotificationsClassifications_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	SELECT Id, NotificationId, ClassificationId
	FROM NotificationsClassifications

	WHERE @Id = Id
END


