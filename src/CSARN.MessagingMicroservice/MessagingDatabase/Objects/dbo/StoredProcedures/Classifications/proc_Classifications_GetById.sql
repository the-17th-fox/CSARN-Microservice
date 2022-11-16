CREATE PROCEDURE [dbo].[proc_Classifications_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		Id, 
		Title, 
		CreatedAt,
		UpdatedAt

	FROM Classifications

	WHERE @Id = Id
END

