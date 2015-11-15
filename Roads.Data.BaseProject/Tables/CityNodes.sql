CREATE TABLE [dbo].[CityNodes] (
    [CityNodeId]   INT            IDENTITY (1, 1) NOT NULL,
    [RegionNodeId] INT            NOT NULL,
    [LanguageKey]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_CityNodes] PRIMARY KEY CLUSTERED ([CityNodeId] ASC),
    CONSTRAINT [FK_RegionNodeCityNode] FOREIGN KEY ([RegionNodeId]) REFERENCES [dbo].[RegionNodes] ([RegionNodeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_RegionNodeCityNode]
    ON [dbo].[CityNodes]([RegionNodeId] ASC);

