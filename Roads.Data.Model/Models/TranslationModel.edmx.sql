
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/06/2015 12:58:31
-- Generated from EDMX file: D:\Projects\Roads\GIT\Roads.Data.Model\Models\TranslationModel.edmx
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

IF OBJECT_ID(N'[dbo].[FK_LanguageStaticTranslation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StaticTranslations] DROP CONSTRAINT [FK_LanguageStaticTranslation];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageMapObjectTranslation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MapObjectTranslations] DROP CONSTRAINT [FK_LanguageMapObjectTranslation];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageDynamicTranslations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DynamicTranslations] DROP CONSTRAINT [FK_LanguageDynamicTranslations];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Languages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Languages];
GO
IF OBJECT_ID(N'[dbo].[DynamicTranslations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DynamicTranslations];
GO
IF OBJECT_ID(N'[dbo].[StaticTranslations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaticTranslations];
GO
IF OBJECT_ID(N'[dbo].[MapObjectTranslations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MapObjectTranslations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [LanguageId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'DynamicTranslations'
CREATE TABLE [dbo].[DynamicTranslations] (
    [DynamicObjectId] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [DescriptionValue] nvarchar(max)  NULL,
    [LanguageId] int  NOT NULL,
    [DynamicKey] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'StaticTranslations'
CREATE TABLE [dbo].[StaticTranslations] (
    [EnumKey] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [LanguageId] int  NOT NULL,
    [StaticTranslationId] bigint IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'MapObjectTranslations'
CREATE TABLE [dbo].[MapObjectTranslations] (
    [MapObjectId] bigint IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [LanguageId] int  NOT NULL,
    [LanguageKey] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [LanguageId] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([LanguageId] ASC);
GO

-- Creating primary key on [DynamicObjectId] in table 'DynamicTranslations'
ALTER TABLE [dbo].[DynamicTranslations]
ADD CONSTRAINT [PK_DynamicTranslations]
    PRIMARY KEY CLUSTERED ([DynamicObjectId] ASC);
GO

-- Creating primary key on [StaticTranslationId] in table 'StaticTranslations'
ALTER TABLE [dbo].[StaticTranslations]
ADD CONSTRAINT [PK_StaticTranslations]
    PRIMARY KEY CLUSTERED ([StaticTranslationId] ASC);
GO

-- Creating primary key on [MapObjectId] in table 'MapObjectTranslations'
ALTER TABLE [dbo].[MapObjectTranslations]
ADD CONSTRAINT [PK_MapObjectTranslations]
    PRIMARY KEY CLUSTERED ([MapObjectId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LanguageId] in table 'StaticTranslations'
ALTER TABLE [dbo].[StaticTranslations]
ADD CONSTRAINT [FK_LanguageStaticTranslation]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Languages]
        ([LanguageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageStaticTranslation'
CREATE INDEX [IX_FK_LanguageStaticTranslation]
ON [dbo].[StaticTranslations]
    ([LanguageId]);
GO

-- Creating foreign key on [LanguageId] in table 'MapObjectTranslations'
ALTER TABLE [dbo].[MapObjectTranslations]
ADD CONSTRAINT [FK_LanguageMapObjectTranslation]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Languages]
        ([LanguageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageMapObjectTranslation'
CREATE INDEX [IX_FK_LanguageMapObjectTranslation]
ON [dbo].[MapObjectTranslations]
    ([LanguageId]);
GO

-- Creating foreign key on [LanguageId] in table 'DynamicTranslations'
ALTER TABLE [dbo].[DynamicTranslations]
ADD CONSTRAINT [FK_LanguageDynamicTranslations]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Languages]
        ([LanguageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageDynamicTranslations'
CREATE INDEX [IX_FK_LanguageDynamicTranslations]
ON [dbo].[DynamicTranslations]
    ([LanguageId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------