CREATE PROCEDURE [dbo].[proc_Classifications_GetAll]
	@PageNum int,
	@PageSize int
AS
BEGIN
	SET NOCOUNT ON
	SELECT 
		Id, 
		Title,
		CreatedAt,
		UpdatedAt
	
	FROM Classifications

	ORDER BY Id
	OFFSET (@PageNum - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
END

