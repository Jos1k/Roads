
SET IDENTITY_INSERT [dbo].[FeedbackItems] ON 
INSERT [dbo].[FeedbackItems] ([FeedbackItemId], [FeedbackModelId], [NameTranslationKey], [DescriptionTranslationKey], [IsNumeric], [Mandatory], [SortNumber]) VALUES (1, 2, 'FB_FiveStarsControl_Title_RoadQuality', 'FB_FiveStarsControl_Description_RoadQuality',1,1,1)
INSERT [dbo].[FeedbackItems] ([FeedbackItemId], [FeedbackModelId], [NameTranslationKey], [DescriptionTranslationKey], [IsNumeric], [Mandatory], [SortNumber]) VALUES (2, 2, 'FB_FiveStarsControl_Title_OverallImpression', 'FB_FiveStarsControl_Description_OverallImpression',1,1,2)
INSERT [dbo].[FeedbackItems] ([FeedbackItemId], [FeedbackModelId], [NameTranslationKey], [DescriptionTranslationKey], [IsNumeric], [Mandatory], [SortNumber]) VALUES (3, 1, 'FB_TextAreaControl_Title_OverallComment', 'FB_TextAreaControl_Description_OverallComment',0,1,3)
SET IDENTITY_INSERT [dbo].[FeedbackItems] OFF
