CREATE TABLE [dbo].[NotificationsClassifications]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [NotificationId] UNIQUEIDENTIFIER NOT NULL, 
    [ClassificationId] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [PK_NotificationsClassifications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NotificationsClassifications_Notifications] FOREIGN KEY ([NotificationId]) REFERENCES [Notifications]([Id]), 
    CONSTRAINT [FK_NotificationsClassifications_Classifications] FOREIGN KEY ([ClassificationId]) REFERENCES [Classifications]([Id]),
);
