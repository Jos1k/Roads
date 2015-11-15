SET IDENTITY_INSERT [dbo].[Users] ON 
--INSERT INTO [dbo].[Users] ([UserId],[Name],[Password],[UserType]) VALUES(1,'testUser1','5f4dcc3b5aa765d61d8327deb882cf99','testUser');
--INSERT INTO [dbo].[Users] ([UserId],[Name],[Password],[UserType]) VALUES(2,'testUser2','5f4dcc3b5aa765d61d8327deb882cf99','testUser');
--INSERT INTO [dbo].[Users] ([UserId],[Name],[Password],[UserType]) VALUES(3,'testUser3','5f4dcc3b5aa765d61d8327deb882cf99','testUser');


INSERT [dbo].[Users] ([UserId], [Name], [Password], [UserType]) VALUES (1, N'Alex', N'Alex', N'TestUserRole')
INSERT [dbo].[Users] ([UserId], [Name], [Password], [UserType]) VALUES (2, N'Жан-Франко Фердищенко', N'Жан-Франко Фердищенко', N'TestUserRole')
INSERT [dbo].[Users] ([UserId], [Name], [Password], [UserType]) VALUES (3, N'Серж Галперин', N'Серж Галперин', N'TestUserRole')
INSERT [dbo].[Users] ([UserId], [Name], [Password], [UserType]) VALUES (4, N'Драйвер', N'Драйвер', N'TestUserRole')
INSERT [dbo].[Users] ([UserId], [Name], [Password], [UserType]) VALUES (5, N'Saul Goodman', N'Saul Goodman', N'TestUserRole')
INSERT [dbo].[Users] ([UserId], [Name], [Password], [UserType]) VALUES (6, N'Mr. Haizenberg', N'Mr. Haizenberg', N'TestUserRole')
INSERT [dbo].[Users] ([UserId], [Name], [Password], [UserType]) VALUES (7, N'Walter White', N'Walter White', N'TestUserRole')
INSERT [dbo].[Users] ([UserId], [Name], [Password], [UserType]) VALUES (8, N'Linus Torvalds', N'Linus Torvalds', N'TestUserRole')


SET IDENTITY_INSERT [dbo].[Users] OFF