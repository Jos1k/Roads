/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								D:\Projects\Roads\GIT\Roads.Data.BaseProject\ServerPublish_Roads.DataBase.Project.publish.xml
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF (N'$(IsLoadInitialData)'= N'true')
BEGIN
 :r ..\InitialData\InsertLanguages.sql
 :r ..\InitialData\InsertRegions.sql
 :r ..\InitialData\InsertCity.sql
 :r ..\InitialData\InsertMapObjectTranslations.sql
 :r ..\InitialData\InsertDynamicContents.sql
 :r ..\InitialData\InsertSettings.sql
 :r ..\InitialData\InsertStaticContents.sql
 :r ..\InitialData\InsertFeedbackModels.sql
 :r ..\InitialData\InsertFeedbackItems.sql
END

IF (N'$(IsLoadTestData)'= N'true')
BEGIN
 :r ..\TestData\InsertTreks.sql
 :r ..\TestData\InsertRouteNodes.sql
 :r ..\TestData\InserUsers.sql
 :r ..\TestData\InsertFeedbacks.sql
 :r ..\TestData\InsertFeedbackValues.sql
END