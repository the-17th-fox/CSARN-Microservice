CREATE PROCEDURE [dbo].[proc_Classifications_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON
	SELECT Id, Title
	FROM Classifications

	WHERE @Id = Id
END

