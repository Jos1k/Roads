CREATE TABLE [dbo].[MapObjectTranslations] (
    [MapObjectId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Value]       NVARCHAR (MAX) NOT NULL,
    [LanguageId]  INT            NOT NULL,
    [LanguageKey] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_MapObjectTranslations] PRIMARY KEY CLUSTERED ([MapObjectId] ASC),
    CONSTRAINT [FK_LanguageMapObjectTranslation] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([LanguageId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_LanguageMapObjectTranslation]
    ON [dbo].[MapObjectTranslations]([LanguageId] ASC);

