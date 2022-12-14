CREATE TABLE [dbo].[Notifications] (
    [Id]           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Organization] NVARCHAR (MAX)   NOT NULL,
    [Header]       NVARCHAR (MAX)   NOT NULL,
    [Body]         NVARCHAR (MAX)   NOT NULL,
    [AccountId]    UNIQUEIDENTIFIER NOT NULL,
    [TargetAccountId] UNIQUEIDENTIFIER NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(), 
);