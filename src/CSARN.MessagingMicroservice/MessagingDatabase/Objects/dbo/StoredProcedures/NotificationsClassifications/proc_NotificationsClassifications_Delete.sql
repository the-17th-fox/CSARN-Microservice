CREATE PROCEDURE [dbo].[proc_NotificationsClassifications_Delete]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	DELETE
	FROM NotificationsClassifications

	WHERE Id = @Id
END