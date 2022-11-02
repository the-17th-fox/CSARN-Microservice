CREATE PROCEDURE [dbo].[proc_Replies_Delete]
	@Id uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON

	DELETE
	FROM Replies

	WHERE 
		Id = @Id
END