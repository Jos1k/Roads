CREATE TABLE [dbo].[FeedbackModels] (
    [FeedbackModelId]   INT            IDENTITY (1, 1) NOT NULL,
    [HtmlCode]          NVARCHAR (MAX) NOT NULL,
    [JavascriptCode]    NVARCHAR (MAX) NOT NULL,
    [FeedbackModelName] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_FeedbackModels] PRIMARY KEY CLUSTERED ([FeedbackModelId] ASC)
);

