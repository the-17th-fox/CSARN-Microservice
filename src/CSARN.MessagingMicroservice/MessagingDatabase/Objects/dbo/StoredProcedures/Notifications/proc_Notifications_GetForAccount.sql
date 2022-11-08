CREATE PROCEDURE [dbo].[proc_Notifications_GetForAccount]
	@TargetAccountId uniqueidentifier,
	@PageNum int,
	@PageSize int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		N.Id, 
		N.Organization, 
		N.Header, N.Body, 
		N.AccountId,
		N.TargetAccountId,
		NC.ClassificationId
	
	FROM Notifications AS N

	LEFT JOIN NotificationsClassifications AS NC
	ON N.Id = NC.NotificationId

	WHERE 
		N.TargetAccountId IS NOT NULL
		AND N.TargetAccountId = @TargetAccountId

	ORDER BY N.Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END
