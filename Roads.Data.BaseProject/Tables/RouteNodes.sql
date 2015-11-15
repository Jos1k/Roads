CREATE TABLE [dbo].[RouteNodes] (
    [RouteNodeId]           INT IDENTITY (1, 1) NOT NULL,
    [OriginCityNodeId]      INT NOT NULL,
    [DestinationCityNodeId] INT NOT NULL,
    CONSTRAINT [PK_RouteNodes] PRIMARY KEY CLUSTERED ([RouteNodeId] ASC),
    CONSTRAINT [FK_CityNodeRouteNodeDestination] FOREIGN KEY ([DestinationCityNodeId]) REFERENCES [dbo].[CityNodes] ([CityNodeId]),
    CONSTRAINT [FK_CityNodeRouteNodeOrigin] FOREIGN KEY ([OriginCityNodeId]) REFERENCES [dbo].[CityNodes] ([CityNodeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CityNodeRouteNodeOrigin]
    ON [dbo].[RouteNodes]([OriginCityNodeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_CityNodeRouteNodeDestination]
    ON [dbo].[RouteNodes]([DestinationCityNodeId] ASC);

