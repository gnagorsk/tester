
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/14/2013 02:18:37
-- Generated from EDMX file: C:\Users\jajcer\Desktop\repo\tester\etap1\Tester\Questionnaire\Questions.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_QuestionCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionSet] DROP CONSTRAINT [FK_QuestionCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionAnswer_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionAnswer] DROP CONSTRAINT [FK_QuestionAnswer_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionAnswer_Answer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionAnswer] DROP CONSTRAINT [FK_QuestionAnswer_Answer];
GO
IF OBJECT_ID(N'[dbo].[FK_CorrectQuestionAnswer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionSet] DROP CONSTRAINT [FK_CorrectQuestionAnswer];
GO
IF OBJECT_ID(N'[dbo].[FK_Subcategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategorySet] DROP CONSTRAINT [FK_Subcategory];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[QuestionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionSet];
GO
IF OBJECT_ID(N'[dbo].[CategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategorySet];
GO
IF OBJECT_ID(N'[dbo].[AnswerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnswerSet];
GO
IF OBJECT_ID(N'[dbo].[QuestionAnswer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionAnswer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'QuestionSet'
CREATE TABLE [dbo].[QuestionSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [text] nvarchar(max)  NOT NULL,
    [Category_id] int  NOT NULL,
    [CorrectAnswer_id] int  NOT NULL
);
GO

-- Creating table 'CategorySet'
CREATE TABLE [dbo].[CategorySet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [text] nvarchar(max)  NOT NULL,
    [Category_id] int  NULL
);
GO

-- Creating table 'AnswerSet'
CREATE TABLE [dbo].[AnswerSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [text] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'QuestionAnswer'
CREATE TABLE [dbo].[QuestionAnswer] (
    [QuestionAnswer_Answer_id] int  NOT NULL,
    [Answers_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'QuestionSet'
ALTER TABLE [dbo].[QuestionSet]
ADD CONSTRAINT [PK_QuestionSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [PK_CategorySet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'AnswerSet'
ALTER TABLE [dbo].[AnswerSet]
ADD CONSTRAINT [PK_AnswerSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [QuestionAnswer_Answer_id], [Answers_id] in table 'QuestionAnswer'
ALTER TABLE [dbo].[QuestionAnswer]
ADD CONSTRAINT [PK_QuestionAnswer]
    PRIMARY KEY NONCLUSTERED ([QuestionAnswer_Answer_id], [Answers_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Category_id] in table 'QuestionSet'
ALTER TABLE [dbo].[QuestionSet]
ADD CONSTRAINT [FK_QuestionCategory]
    FOREIGN KEY ([Category_id])
    REFERENCES [dbo].[CategorySet]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionCategory'
CREATE INDEX [IX_FK_QuestionCategory]
ON [dbo].[QuestionSet]
    ([Category_id]);
GO

-- Creating foreign key on [QuestionAnswer_Answer_id] in table 'QuestionAnswer'
ALTER TABLE [dbo].[QuestionAnswer]
ADD CONSTRAINT [FK_QuestionAnswer_Question]
    FOREIGN KEY ([QuestionAnswer_Answer_id])
    REFERENCES [dbo].[QuestionSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Answers_id] in table 'QuestionAnswer'
ALTER TABLE [dbo].[QuestionAnswer]
ADD CONSTRAINT [FK_QuestionAnswer_Answer]
    FOREIGN KEY ([Answers_id])
    REFERENCES [dbo].[AnswerSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionAnswer_Answer'
CREATE INDEX [IX_FK_QuestionAnswer_Answer]
ON [dbo].[QuestionAnswer]
    ([Answers_id]);
GO

-- Creating foreign key on [CorrectAnswer_id] in table 'QuestionSet'
ALTER TABLE [dbo].[QuestionSet]
ADD CONSTRAINT [FK_CorrectQuestionAnswer]
    FOREIGN KEY ([CorrectAnswer_id])
    REFERENCES [dbo].[AnswerSet]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CorrectQuestionAnswer'
CREATE INDEX [IX_FK_CorrectQuestionAnswer]
ON [dbo].[QuestionSet]
    ([CorrectAnswer_id]);
GO

-- Creating foreign key on [Category_id] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [FK_Subcategory]
    FOREIGN KEY ([Category_id])
    REFERENCES [dbo].[CategorySet]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Subcategory'
CREATE INDEX [IX_FK_Subcategory]
ON [dbo].[CategorySet]
    ([Category_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------