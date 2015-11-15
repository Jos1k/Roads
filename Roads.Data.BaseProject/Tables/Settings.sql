CREATE TABLE [dbo].[Settings] (
    [SettingId]    INT            IDENTITY (1, 1) NOT NULL,
    [SettingName]  NVARCHAR (MAX) NOT NULL,
    [SettingValue] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED ([SettingId] ASC)
);

