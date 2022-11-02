CREATE PROCEDURE [dbo].[proc_Notifications_GetAll]
	@PageNum int,
	@PageSize int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		N.Id, 
		N.Organization, 
		N.Header, N.Body, 
		N.AccountId--, 
		--NC.ClassificationId
	
	FROM Notifications AS N

	--LEFT JOIN NotificationsClassifications AS NC
	--ON N.Id = NC.NotificationId

	--ORDER BY N.Id
	--OFFSET (@PageNum - 1) * @PageSize ROWS
	--FETCH NEXT @PageSize ROWS ONLY
END