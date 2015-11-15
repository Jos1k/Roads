CREATE TABLE [dbo].[Feedbacks] (
    [FeedbackId]  INT      IDENTITY (1, 1) NOT NULL,
    [SubmitTime]  DATETIME NOT NULL,
    [RouteNodeId] INT      NOT NULL,
    [UserId]      INT      NOT NULL,
    CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED ([FeedbackId] ASC),
    CONSTRAINT [FK_RouteNodeFeedback] FOREIGN KEY ([RouteNodeId]) REFERENCES [dbo].[RouteNodes] ([RouteNodeId]),
    CONSTRAINT [FK_UserFeedback] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_RouteNodeFeedback]
    ON [dbo].[Feedbacks]([RouteNodeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_UserFeedback]
    ON [dbo].[Feedbacks]([UserId] ASC);

