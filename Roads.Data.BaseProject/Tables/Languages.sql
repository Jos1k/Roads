CREATE TABLE [dbo].[Languages] (
    [LanguageId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX) NOT NULL,
    [IsDefault]  BIT            NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([LanguageId] ASC)
);

