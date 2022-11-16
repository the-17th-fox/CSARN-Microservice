CREATE PROCEDURE [dbo].[proc_Classfications_GetAllForNotification]
	@NotificationId uniqueidentifier,
	@PageNum int,
	@PageSize int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		C.Id, 
		C.Title,
		C.CreatedAt,
		C.UpdatedAt

	FROM Classifications AS C
	
	LEFT JOIN NotificationsClassifications AS NC
	ON C.Id = NC.ClassificationId
	WHERE NC.NotificationId = @NotificationId

	ORDER BY C.Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END