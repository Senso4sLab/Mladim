BEGIN TRANSACTION;
GO

ALTER TABLE [Organizations] ADD [Attributes_AllowShareData] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240528182903_allowShareDataProperty', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Member] ADD [EmailSent] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240622172102_email_property', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [Mladim1ka] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240824042510_1kaPropertyAdded', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240824042556_1kapropetryAdded', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'Attributes_Description');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Projects] ALTER COLUMN [Attributes_Description] nvarchar(max) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Organizations]') AND [c].[name] = N'Attributes_Description');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Organizations] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Organizations] ALTER COLUMN [Attributes_Description] nvarchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Activities]') AND [c].[name] = N'Attributes_Description');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Activities] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Activities] ALTER COLUMN [Attributes_Description] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241017112748_nullDescription', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [ResetPasswordUtc] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241118203014_addReserPasswordUtc', N'7.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Activities] DROP CONSTRAINT [FK_Activities_Questionnairies_SurveyQuestionnairyId];
GO

DROP TABLE [SurveyQuestionSurveyQuestionnairy];
GO

DROP TABLE [Questionnairies];
GO

DROP INDEX [IX_Activities_SurveyQuestionnairyId] ON [Activities];
GO

DELETE FROM [Questions]
WHERE [Id] = 1;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 2;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 3;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 4;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 5;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 6;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 7;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 8;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 9;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 10;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 11;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 12;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 13;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 14;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 15;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 16;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 17;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 18;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 19;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 20;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 21;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 22;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 23;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 24;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 25;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 26;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 27;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 28;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 29;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 30;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 31;
SELECT @@ROWCOUNT;

GO

DELETE FROM [Questions]
WHERE [Id] = 32;
SELECT @@ROWCOUNT;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Questions]') AND [c].[name] = N'Discriminator');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Questions] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Questions] DROP COLUMN [Discriminator];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Questions]') AND [c].[name] = N'Texts');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Questions] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Questions] DROP COLUMN [Texts];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Activities]') AND [c].[name] = N'SurveyQuestionnairyId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Activities] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Activities] DROP COLUMN [SurveyQuestionnairyId];
GO

EXEC sp_rename N'[Questions].[UniqueQuestionId]', N'TargetGroup', N'COLUMN';
GO

ALTER TABLE [Questions] ADD [Text_Female] nvarchar(max) NULL;
GO

ALTER TABLE [Questions] ADD [Text_Male] nvarchar(max) NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AnonymousSurveyResponse]') AND [c].[name] = N'AnonymousParticipant_Id');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [AnonymousSurveyResponse] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [AnonymousSurveyResponse] ALTER COLUMN [AnonymousParticipant_Id] int NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AnonymousSurveyResponse]') AND [c].[name] = N'AnonymousParticipant_Gender');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [AnonymousSurveyResponse] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [AnonymousSurveyResponse] ALTER COLUMN [AnonymousParticipant_Gender] int NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AnonymousSurveyResponse]') AND [c].[name] = N'AnonymousParticipant_AgeGroup');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [AnonymousSurveyResponse] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [AnonymousSurveyResponse] ALTER COLUMN [AnonymousParticipant_AgeGroup] int NULL;
GO

ALTER TABLE [AnonymousSurveyResponse] ADD [AnonymousYouthWorker_AgeGroup] int NULL;
GO

ALTER TABLE [AnonymousSurveyResponse] ADD [AnonymousYouthWorker_Gender] int NULL;
GO

ALTER TABLE [AnonymousSurveyResponse] ADD [AnonymousYouthWorker_Role] int NULL;
GO

ALTER TABLE [AnonymousSurveyResponse] ADD [AnonymousYouthWorker_YearsOfExperience] int NULL;
GO

ALTER TABLE [Activities] ADD [Attributes_ActivityTargetGroup] int NOT NULL DEFAULT 1;
GO

CREATE TABLE [SurveyQuestionSubQuestions] (
    [Id] int NOT NULL IDENTITY,
    [Text_Female] nvarchar(max) NOT NULL,
    [Text_Male] nvarchar(max) NOT NULL,
    [SurveyQuestionId] int NOT NULL,
    CONSTRAINT [PK_SurveyQuestionSubQuestions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SurveyQuestionSubQuestions_Questions_SurveyQuestionId] FOREIGN KEY ([SurveyQuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SurveyQuestionSubQuestions_SurveyQuestionId] ON [SurveyQuestionSubQuestions] ([SurveyQuestionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260912034033_YouthWorkerSurvery', N'7.0.9');
GO

COMMIT;
GO

