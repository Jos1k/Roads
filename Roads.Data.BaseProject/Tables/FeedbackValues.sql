CREATE TABLE [dbo].[FeedbackValues] (
    [FeedbackValueId] INT            IDENTITY (1, 1) NOT NULL,
    [Value]           NVARCHAR (MAX) NOT NULL,
    [FeedbackId]      INT            NOT NULL,
    [FeedbackItemId]  INT            NOT NULL,
    CONSTRAINT [PK_FeedbackValues] PRIMARY KEY CLUSTERED ([FeedbackValueId] ASC),
    CONSTRAINT [FK_FeedbackItemFeedbackValue] FOREIGN KEY ([FeedbackItemId]) REFERENCES [dbo].[FeedbackItems] ([FeedbackItemId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_FeedbackItemFeedbackValue]
    ON [dbo].[FeedbackValues]([FeedbackItemId] ASC);

