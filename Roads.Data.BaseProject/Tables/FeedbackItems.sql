CREATE TABLE [dbo].[FeedbackItems] (
    [FeedbackItemId]            INT            IDENTITY (1, 1) NOT NULL,
    [Mandatory]                 BIT            NOT NULL,
    [SortNumber]                INT            NOT NULL,
    [IsNumeric]                 BIT            NOT NULL,
    [FeedbackModelId]           INT            NOT NULL,
    [NameTranslationKey]        NVARCHAR (MAX) NOT NULL,
    [DescriptionTranslationKey] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_FeedbackItems] PRIMARY KEY CLUSTERED ([FeedbackItemId] ASC),
    CONSTRAINT [FK_FeedbackModelFeedbackItem] FOREIGN KEY ([FeedbackModelId]) REFERENCES [dbo].[FeedbackModels] ([FeedbackModelId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_FeedbackModelFeedbackItem]
    ON [dbo].[FeedbackItems]([FeedbackModelId] ASC);

