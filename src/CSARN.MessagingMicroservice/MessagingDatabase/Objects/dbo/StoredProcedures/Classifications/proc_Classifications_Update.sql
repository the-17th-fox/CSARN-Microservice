﻿CREATE PROCEDURE [dbo].[proc_Classifications_Update]
	@Id uniqueidentifier,
	@Title nvarchar(MAX)

AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE Classifications
	
	SET Title = @Title

	WHERE Id = @Id
END