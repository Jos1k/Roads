CREATE TABLE [dbo].[StaticTranslations] (
    [EnumKey]             NVARCHAR (MAX) NOT NULL,
    [Value]               NVARCHAR (MAX) NOT NULL,
    [LanguageId]          INT            NOT NULL,
    [StaticTranslationId] BIGINT         IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_StaticTranslations] PRIMARY KEY CLUSTERED ([StaticTranslationId] ASC),
    CONSTRAINT [FK_LanguageStaticTranslation] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([LanguageId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_LanguageStaticTranslation]
    ON [dbo].[StaticTranslations]([LanguageId] ASC);

