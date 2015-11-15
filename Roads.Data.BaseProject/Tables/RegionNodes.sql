CREATE TABLE [dbo].[RegionNodes] (
    [RegionNodeId] INT            IDENTITY (1, 1) NOT NULL,
    [LanguageKey]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_RegionNodes] PRIMARY KEY CLUSTERED ([RegionNodeId] ASC)
);

