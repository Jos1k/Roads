
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/03/2015 17:14:50
-- Generated from EDMX file: D:\IBanch\Repo\Roads.Data.Model\Models\DatabaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RoadsDataBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RouteNodeFeedback]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedbacks] DROP CONSTRAINT [FK_RouteNodeFeedback];
GO
IF OBJECT_ID(N'[dbo].[FK_CityNodeRouteNodeOrigin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RouteNodes] DROP CONSTRAINT [FK_CityNodeRouteNodeOrigin];
GO
IF OBJECT_ID(N'[dbo].[FK_CityNodeRouteNodeDestination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RouteNodes] DROP CONSTRAINT [FK_CityNodeRouteNodeDestination];
GO
IF OBJECT_ID(N'[dbo].[FK_RegionNodeCityNode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CityNodes] DROP CONSTRAINT [FK_RegionNodeCityNode];
GO
IF OBJECT_ID(N'[dbo].[FK_UserFeedback]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedbacks] DROP CONSTRAINT [FK_UserFeedback];
GO
IF OBJECT_ID(N'[dbo].[FK_FeedbackModelFeedbackItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FeedbackItems] DROP CONSTRAINT [FK_FeedbackModelFeedbackItem];
GO
IF OBJECT_ID(N'[dbo].[FK_FeedbackItemFeedbackValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FeedbackValues] DROP CONSTRAINT [FK_FeedbackItemFeedbackValue];
GO
IF OBJECT_ID(N'[dbo].[FK_CityNodeTrek]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Treks] DROP CONSTRAINT [FK_CityNodeTrek];
GO
IF OBJECT_ID(N'[dbo].[FK_CityNodeTrek1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Treks] DROP CONSTRAINT [FK_CityNodeTrek1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[RouteNodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RouteNodes];
GO
IF OBJECT_ID(N'[dbo].[CityNodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CityNodes];
GO
IF OBJECT_ID(N'[dbo].[RegionNodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RegionNodes];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Feedbacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedbacks];
GO
IF OBJECT_ID(N'[dbo].[FeedbackItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FeedbackItems];
GO
IF OBJECT_ID(N'[dbo].[FeedbackModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FeedbackModels];
GO
IF OBJECT_ID(N'[dbo].[FeedbackValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FeedbackValues];
GO
IF OBJECT_ID(N'[dbo].[Treks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Treks];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'RouteNodes'
CREATE TABLE [dbo].[RouteNodes] (
    [RouteNodeId] int IDENTITY(1,1) NOT NULL,
    [OriginCityNodeId] int  NOT NULL,
    [DestinationCityNodeId] int  NOT NULL
);
GO

-- Creating table 'CityNodes'
CREATE TABLE [dbo].[CityNodes] (
    [CityNodeId] int IDENTITY(1,1) NOT NULL,
    [RegionNodeId] int  NOT NULL,
    [LanguageKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RegionNodes'
CREATE TABLE [dbo].[RegionNodes] (
    [RegionNodeId] int IDENTITY(1,1) NOT NULL,
    [LanguageKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [UserType] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Feedbacks'
CREATE TABLE [dbo].[Feedbacks] (
    [FeedbackId] int IDENTITY(1,1) NOT NULL,
    [SubmitTime] datetime  NOT NULL,
    [RouteNodeId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'FeedbackItems'
CREATE TABLE [dbo].[FeedbackItems] (
    [FeedbackItemId] int IDENTITY(1,1) NOT NULL,
    [Mandatory] bit  NOT NULL,
    [SortNumber] int  NOT NULL,
    [IsNumeric] bit  NOT NULL,
    [FeedbackModelId] int  NOT NULL,
    [NameTranslationKey] nvarchar(max)  NOT NULL,
    [DescriptionTranslationKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FeedbackModels'
CREATE TABLE [dbo].[FeedbackModels] (
    [FeedbackModelId] int IDENTITY(1,1) NOT NULL,
    [HtmlCode] nvarchar(max)  NOT NULL,
    [JavascriptCode] nvarchar(max)  NOT NULL,
    [FeedbackModelName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FeedbackValues'
CREATE TABLE [dbo].[FeedbackValues] (
    [FeedbackValueId] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [FeedbackId] int  NOT NULL,
    [FeedbackItemId] int  NOT NULL
);
GO

-- Creating table 'Treks'
CREATE TABLE [dbo].[Treks] (
    [Hash] nvarchar(max)  NOT NULL,
    [Track] nvarchar(max)  NOT NULL,
    [TrekId] bigint IDENTITY(1,1) NOT NULL,
    [OriginCityNodeId] int  NOT NULL,
    [DesinationCityNodeId] int  NOT NULL,
    [NodesCount] tinyint  NOT NULL,
    [TrekDate] datetime  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [SettingId] int IDENTITY(1,1) NOT NULL,
    [SettingName] nvarchar(max)  NOT NULL,
    [SettingValue] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [RouteNodeId] in table 'RouteNodes'
ALTER TABLE [dbo].[RouteNodes]
ADD CONSTRAINT [PK_RouteNodes]
    PRIMARY KEY CLUSTERED ([RouteNodeId] ASC);
GO

-- Creating primary key on [CityNodeId] in table 'CityNodes'
ALTER TABLE [dbo].[CityNodes]
ADD CONSTRAINT [PK_CityNodes]
    PRIMARY KEY CLUSTERED ([CityNodeId] ASC);
GO

-- Creating primary key on [RegionNodeId] in table 'RegionNodes'
ALTER TABLE [dbo].[RegionNodes]
ADD CONSTRAINT [PK_RegionNodes]
    PRIMARY KEY CLUSTERED ([RegionNodeId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [FeedbackId] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [PK_Feedbacks]
    PRIMARY KEY CLUSTERED ([FeedbackId] ASC);
GO

-- Creating primary key on [FeedbackItemId] in table 'FeedbackItems'
ALTER TABLE [dbo].[FeedbackItems]
ADD CONSTRAINT [PK_FeedbackItems]
    PRIMARY KEY CLUSTERED ([FeedbackItemId] ASC);
GO

-- Creating primary key on [FeedbackModelId] in table 'FeedbackModels'
ALTER TABLE [dbo].[FeedbackModels]
ADD CONSTRAINT [PK_FeedbackModels]
    PRIMARY KEY CLUSTERED ([FeedbackModelId] ASC);
GO

-- Creating primary key on [FeedbackValueId] in table 'FeedbackValues'
ALTER TABLE [dbo].[FeedbackValues]
ADD CONSTRAINT [PK_FeedbackValues]
    PRIMARY KEY CLUSTERED ([FeedbackValueId] ASC);
GO

-- Creating primary key on [TrekId] in table 'Treks'
ALTER TABLE [dbo].[Treks]
ADD CONSTRAINT [PK_Treks]
    PRIMARY KEY CLUSTERED ([TrekId] ASC);
GO

-- Creating primary key on [SettingId] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([SettingId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RouteNodeId] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK_RouteNodeFeedback]
    FOREIGN KEY ([RouteNodeId])
    REFERENCES [dbo].[RouteNodes]
        ([RouteNodeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RouteNodeFeedback'
CREATE INDEX [IX_FK_RouteNodeFeedback]
ON [dbo].[Feedbacks]
    ([RouteNodeId]);
GO

-- Creating foreign key on [OriginCityNodeId] in table 'RouteNodes'
ALTER TABLE [dbo].[RouteNodes]
ADD CONSTRAINT [FK_CityNodeRouteNodeOrigin]
    FOREIGN KEY ([OriginCityNodeId])
    REFERENCES [dbo].[CityNodes]
        ([CityNodeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityNodeRouteNodeOrigin'
CREATE INDEX [IX_FK_CityNodeRouteNodeOrigin]
ON [dbo].[RouteNodes]
    ([OriginCityNodeId]);
GO

-- Creating foreign key on [DestinationCityNodeId] in table 'RouteNodes'
ALTER TABLE [dbo].[RouteNodes]
ADD CONSTRAINT [FK_CityNodeRouteNodeDestination]
    FOREIGN KEY ([DestinationCityNodeId])
    REFERENCES [dbo].[CityNodes]
        ([CityNodeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityNodeRouteNodeDestination'
CREATE INDEX [IX_FK_CityNodeRouteNodeDestination]
ON [dbo].[RouteNodes]
    ([DestinationCityNodeId]);
GO

-- Creating foreign key on [RegionNodeId] in table 'CityNodes'
ALTER TABLE [dbo].[CityNodes]
ADD CONSTRAINT [FK_RegionNodeCityNode]
    FOREIGN KEY ([RegionNodeId])
    REFERENCES [dbo].[RegionNodes]
        ([RegionNodeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RegionNodeCityNode'
CREATE INDEX [IX_FK_RegionNodeCityNode]
ON [dbo].[CityNodes]
    ([RegionNodeId]);
GO

-- Creating foreign key on [UserId] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK_UserFeedback]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserFeedback'
CREATE INDEX [IX_FK_UserFeedback]
ON [dbo].[Feedbacks]
    ([UserId]);
GO

-- Creating foreign key on [FeedbackModelId] in table 'FeedbackItems'
ALTER TABLE [dbo].[FeedbackItems]
ADD CONSTRAINT [FK_FeedbackModelFeedbackItem]
    FOREIGN KEY ([FeedbackModelId])
    REFERENCES [dbo].[FeedbackModels]
        ([FeedbackModelId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FeedbackModelFeedbackItem'
CREATE INDEX [IX_FK_FeedbackModelFeedbackItem]
ON [dbo].[FeedbackItems]
    ([FeedbackModelId]);
GO

-- Creating foreign key on [FeedbackItemId] in table 'FeedbackValues'
ALTER TABLE [dbo].[FeedbackValues]
ADD CONSTRAINT [FK_FeedbackItemFeedbackValue]
    FOREIGN KEY ([FeedbackItemId])
    REFERENCES [dbo].[FeedbackItems]
        ([FeedbackItemId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FeedbackItemFeedbackValue'
CREATE INDEX [IX_FK_FeedbackItemFeedbackValue]
ON [dbo].[FeedbackValues]
    ([FeedbackItemId]);
GO

-- Creating foreign key on [OriginCityNodeId] in table 'Treks'
ALTER TABLE [dbo].[Treks]
ADD CONSTRAINT [FK_CityNodeTrek]
    FOREIGN KEY ([OriginCityNodeId])
    REFERENCES [dbo].[CityNodes]
        ([CityNodeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityNodeTrek'
CREATE INDEX [IX_FK_CityNodeTrek]
ON [dbo].[Treks]
    ([OriginCityNodeId]);
GO

-- Creating foreign key on [DesinationCityNodeId] in table 'Treks'
ALTER TABLE [dbo].[Treks]
ADD CONSTRAINT [FK_CityNodeTrek1]
    FOREIGN KEY ([DesinationCityNodeId])
    REFERENCES [dbo].[CityNodes]
        ([CityNodeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityNodeTrek1'
CREATE INDEX [IX_FK_CityNodeTrek1]
ON [dbo].[Treks]
    ([DesinationCityNodeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------