CREATE PROCEDURE [dbo].[proc_Classifications_GetAllForReport]
	@ReportId uniqueidentifier,
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
	
	LEFT JOIN ReportsClassifications AS RC
	ON C.Id = RC.ClassificationId
	WHERE RC.ReportId = @ReportId

	ORDER BY C.Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END
