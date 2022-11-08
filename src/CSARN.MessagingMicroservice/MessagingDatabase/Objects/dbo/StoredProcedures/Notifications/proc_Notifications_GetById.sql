CREATE PROCEDURE [dbo].[proc_Notifications_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	SELECT 
		N.Id, 
		N.Organization, 
		N.Header, 
		N.Body, 
		N.AccountId, 
		N.TargetAccountId,
		NC.ClassificationId

	FROM Notifications AS N

	LEFT JOIN NotificationsClassifications AS NC
	ON N.Id = NC.NotificationId

	WHERE @Id = N.Id
END
