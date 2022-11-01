﻿CREATE TABLE [dbo].[Reports]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[Header]       NVARCHAR (MAX)   NOT NULL,
    [Body]         NVARCHAR (MAX)   NOT NULL,
    [AccountId]    UNIQUEIDENTIFIER NOT NULL,
	[ReplyId]   UNIQUEIDENTIFIER NULL,
	[StatusId]	   INT				NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT [FK_Reports_Replies] FOREIGN KEY ([ReplyId]) REFERENCES [Replies]([Id]), 
);