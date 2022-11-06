CREATE TABLE [dbo].[ReportsClassifications]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [ReportId] UNIQUEIDENTIFIER NOT NULL, 
    [ClassificationId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_ReportsClassifications] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_ReportsClassifications_Reports] FOREIGN KEY ([ReportId]) REFERENCES [Reports]([Id]),
    CONSTRAINT [FK_ReportsClassifications_Classifications] FOREIGN KEY ([ClassificationId]) REFERENCES [Classifications]([Id]) 
);
