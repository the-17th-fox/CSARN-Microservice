CREATE PROCEDURE [dbo].[proc_Classifications_GetAllForReports]
	@ClassificationId uniqueidentifier,
	@PageNum int,
	@PageSize int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		C.Id, 
		C.Title,
		RC.ReportId,
		C.CreatedAt,
		C.UpdatedAt

	FROM Classifications AS C
	
	LEFT JOIN ReportsClassifications AS RC
	ON C.Id = RC.ClassificationId
	WHERE RC.ReportId = @ClassificationId

	ORDER BY C.Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END
