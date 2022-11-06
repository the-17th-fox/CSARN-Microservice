CREATE PROCEDURE [dbo].[proc_Notifications_Delete]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	DELETE
	FROM Notifications

	WHERE Id = @Id
END