CREATE TABLE [dbo].[DynamicTranslations] (
    [DynamicObjectId]  INT            IDENTITY (1, 1) NOT NULL,
    [Value]            NVARCHAR (MAX) NOT NULL,
    [DescriptionValue] NVARCHAR (MAX) NULL,
    [LanguageId]       INT            NOT NULL,
    [DynamicKey]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_DynamicTranslations] PRIMARY KEY CLUSTERED ([DynamicObjectId] ASC),
    CONSTRAINT [FK_LanguageDynamicTranslations] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([LanguageId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_LanguageDynamicTranslations]
    ON [dbo].[DynamicTranslations]([LanguageId] ASC);

