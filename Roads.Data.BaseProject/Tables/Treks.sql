CREATE TABLE [dbo].[Treks] (
    [Hash]                 NVARCHAR (MAX) NOT NULL,
    [Track]                NVARCHAR (MAX) NOT NULL,
    [TrekId]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [OriginCityNodeId]     INT            NOT NULL,
    [DesinationCityNodeId] INT            NOT NULL,
    [NodesCount]           TINYINT        NOT NULL,
    [TrekDate]             DATETIME       NOT NULL,
    CONSTRAINT [PK_Treks] PRIMARY KEY CLUSTERED ([TrekId] ASC),
    CONSTRAINT [FK_CityNodeTrek] FOREIGN KEY ([OriginCityNodeId]) REFERENCES [dbo].[CityNodes] ([CityNodeId]),
    CONSTRAINT [FK_CityNodeTrek1] FOREIGN KEY ([DesinationCityNodeId]) REFERENCES [dbo].[CityNodes] ([CityNodeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CityNodeTrek]
    ON [dbo].[Treks]([OriginCityNodeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CityNodeTrek1]
    ON [dbo].[Treks]([DesinationCityNodeId] ASC);

