CREATE PROCEDURE [dbo].[proc_NotificationsClassifications_GetAll]
	@PageNum int,
	@PageSize int
AS
BEGIN
	SET NOCOUNT ON
	SELECT Id, NotificationId, ClassificationId
	
	FROM NotificationsClassifications

	ORDER BY Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END


