
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/17/2018 12:05:24
-- Generated from EDMX file: C:\Users\artem\source\repos\WebScriptManager\WebScriptManager\Models\ScriptModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ScriptManagerDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AdminScenario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenarioSet] DROP CONSTRAINT [FK_AdminScenario];
GO
IF OBJECT_ID(N'[dbo].[FK_UserScenario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenarioSet] DROP CONSTRAINT [FK_UserScenario];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet] DROP CONSTRAINT [FK_UserGroupUser];
GO
IF OBJECT_ID(N'[dbo].[FK_ScenarioSensorType_Scenario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenarioSensorType] DROP CONSTRAINT [FK_ScenarioSensorType_Scenario];
GO
IF OBJECT_ID(N'[dbo].[FK_ScenarioSensorType_SensorType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenarioSensorType] DROP CONSTRAINT [FK_ScenarioSensorType_SensorType];
GO
IF OBJECT_ID(N'[dbo].[FK_ScenarioSmartThingType_Scenario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenarioSmartThingType] DROP CONSTRAINT [FK_ScenarioSmartThingType_Scenario];
GO
IF OBJECT_ID(N'[dbo].[FK_ScenarioSmartThingType_SmartThingType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenarioSmartThingType] DROP CONSTRAINT [FK_ScenarioSmartThingType_SmartThingType];
GO
IF OBJECT_ID(N'[dbo].[FK_SmartThingTypeSmartThingTypeDictionary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SmartThingTypeDictionarySet] DROP CONSTRAINT [FK_SmartThingTypeSmartThingTypeDictionary];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupUserGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGroupSet] DROP CONSTRAINT [FK_UserGroupUserGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_SensorTypeSensor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SensorSet] DROP CONSTRAINT [FK_SensorTypeSensor];
GO
IF OBJECT_ID(N'[dbo].[FK_SmartThingTypeSmartThing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SmartThingSet] DROP CONSTRAINT [FK_SmartThingTypeSmartThing];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupSmartPlace]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SmartPlaceSet] DROP CONSTRAINT [FK_UserGroupSmartPlace];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupControlBox]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ControlBoxSet] DROP CONSTRAINT [FK_UserGroupControlBox];
GO
IF OBJECT_ID(N'[dbo].[FK_SmartPlaceSensor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SensorSet] DROP CONSTRAINT [FK_SmartPlaceSensor];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlBoxSensor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SensorSet] DROP CONSTRAINT [FK_ControlBoxSensor];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlBoxSmartThing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SmartThingSet] DROP CONSTRAINT [FK_ControlBoxSmartThing];
GO
IF OBJECT_ID(N'[dbo].[FK_SmartPlaceSmartThing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SmartThingSet] DROP CONSTRAINT [FK_SmartPlaceSmartThing];
GO
IF OBJECT_ID(N'[dbo].[FK_ScenarioControlBox_Scenario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenarioControlBox] DROP CONSTRAINT [FK_ScenarioControlBox_Scenario];
GO
IF OBJECT_ID(N'[dbo].[FK_ScenarioControlBox_ControlBox]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ScenarioControlBox] DROP CONSTRAINT [FK_ScenarioControlBox_ControlBox];
GO
IF OBJECT_ID(N'[dbo].[FK_DoubleSensor_inherits_Sensor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SensorSet_DoubleSensor] DROP CONSTRAINT [FK_DoubleSensor_inherits_Sensor];
GO
IF OBJECT_ID(N'[dbo].[FK_BoolSensor_inherits_Sensor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SensorSet_BoolSensor] DROP CONSTRAINT [FK_BoolSensor_inherits_Sensor];
GO
IF OBJECT_ID(N'[dbo].[FK_BoolSmartThing_inherits_SmartThing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SmartThingSet_BoolSmartThing] DROP CONSTRAINT [FK_BoolSmartThing_inherits_SmartThing];
GO
IF OBJECT_ID(N'[dbo].[FK_DoubleSmartThing1_inherits_SmartThing]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SmartThingSet_DoubleSmartThing1] DROP CONSTRAINT [FK_DoubleSmartThing1_inherits_SmartThing];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AdminSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdminSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[ScenarioSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScenarioSet];
GO
IF OBJECT_ID(N'[dbo].[UserGroupSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGroupSet];
GO
IF OBJECT_ID(N'[dbo].[SensorTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SensorTypeSet];
GO
IF OBJECT_ID(N'[dbo].[SmartThingTypeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SmartThingTypeSet];
GO
IF OBJECT_ID(N'[dbo].[SmartThingTypeDictionarySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SmartThingTypeDictionarySet];
GO
IF OBJECT_ID(N'[dbo].[SmartPlaceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SmartPlaceSet];
GO
IF OBJECT_ID(N'[dbo].[SensorSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SensorSet];
GO
IF OBJECT_ID(N'[dbo].[SmartThingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SmartThingSet];
GO
IF OBJECT_ID(N'[dbo].[ControlBoxSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ControlBoxSet];
GO
IF OBJECT_ID(N'[dbo].[LicenceDictionarySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LicenceDictionarySet];
GO
IF OBJECT_ID(N'[dbo].[SensorSet_DoubleSensor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SensorSet_DoubleSensor];
GO
IF OBJECT_ID(N'[dbo].[SensorSet_BoolSensor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SensorSet_BoolSensor];
GO
IF OBJECT_ID(N'[dbo].[SmartThingSet_BoolSmartThing]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SmartThingSet_BoolSmartThing];
GO
IF OBJECT_ID(N'[dbo].[SmartThingSet_DoubleSmartThing1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SmartThingSet_DoubleSmartThing1];
GO
IF OBJECT_ID(N'[dbo].[ScenarioSensorType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScenarioSensorType];
GO
IF OBJECT_ID(N'[dbo].[ScenarioSmartThingType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScenarioSmartThingType];
GO
IF OBJECT_ID(N'[dbo].[ScenarioControlBox]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ScenarioControlBox];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AdminSet'
CREATE TABLE [dbo].[AdminSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [FIO] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [Fingerprint] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [FIO] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Mail] nvarchar(max)  NOT NULL,
    [UserType] int  NOT NULL,
    [UserGroup_Id] bigint  NOT NULL
);
GO

-- Creating table 'ScenarioSet'
CREATE TABLE [dbo].[ScenarioSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ScriptFile] nvarchar(max)  NOT NULL,
    [Access] int  NOT NULL,
    [LastUpdate] datetime  NOT NULL,
    [Admin_Id] bigint  NULL,
    [User_Id] bigint  NULL
);
GO

-- Creating table 'UserGroupSet'
CREATE TABLE [dbo].[UserGroupSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Licence] int  NOT NULL,
    [Parent_Id] bigint  NULL
);
GO

-- Creating table 'SensorTypeSet'
CREATE TABLE [dbo].[SensorTypeSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SmartThingTypeSet'
CREATE TABLE [dbo].[SmartThingTypeSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SmartThingTypeDictionarySet'
CREATE TABLE [dbo].[SmartThingTypeDictionarySet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [LowerBound] float  NOT NULL,
    [UpperBound] float  NOT NULL,
    [SmartThingType_Id] bigint  NOT NULL
);
GO

-- Creating table 'SmartPlaceSet'
CREATE TABLE [dbo].[SmartPlaceSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [UserGroup_Id] bigint  NOT NULL
);
GO

-- Creating table 'SensorSet'
CREATE TABLE [dbo].[SensorSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SensorType_Id] bigint  NOT NULL,
    [SmartPlace_Id] bigint  NOT NULL,
    [ControlBox_Id] bigint  NOT NULL
);
GO

-- Creating table 'SmartThingSet'
CREATE TABLE [dbo].[SmartThingSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [SmartThingType_Id] bigint  NOT NULL,
    [ControlBox_Id] bigint  NOT NULL,
    [SmartPlace_Id] bigint  NOT NULL
);
GO

-- Creating table 'ControlBoxSet'
CREATE TABLE [dbo].[ControlBoxSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [IsStable] bit  NOT NULL,
    [UserGroup_Id] bigint  NOT NULL
);
GO

-- Creating table 'LicenceDictionarySet'
CREATE TABLE [dbo].[LicenceDictionarySet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [LicenceType] int  NOT NULL,
    [UpperBound] float  NOT NULL
);
GO

-- Creating table 'SensorSet_DoubleSensor'
CREATE TABLE [dbo].[SensorSet_DoubleSensor] (
    [Value] float  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'SensorSet_BoolSensor'
CREATE TABLE [dbo].[SensorSet_BoolSensor] (
    [Value] bit  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'SmartThingSet_BoolSmartThing'
CREATE TABLE [dbo].[SmartThingSet_BoolSmartThing] (
    [Switch] int  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'SmartThingSet_DoubleSmartThing1'
CREATE TABLE [dbo].[SmartThingSet_DoubleSmartThing1] (
    [Switch] int  NOT NULL,
    [Value] float  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'ScenarioSensorType'
CREATE TABLE [dbo].[ScenarioSensorType] (
    [Scenaries_Id] bigint  NOT NULL,
    [SensorTypes_Id] bigint  NOT NULL
);
GO

-- Creating table 'ScenarioSmartThingType'
CREATE TABLE [dbo].[ScenarioSmartThingType] (
    [Scenaries_Id] bigint  NOT NULL,
    [SmartThingTypes_Id] bigint  NOT NULL
);
GO

-- Creating table 'ScenarioControlBox'
CREATE TABLE [dbo].[ScenarioControlBox] (
    [Scenaries_Id] bigint  NOT NULL,
    [ControlBoxes_Id] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AdminSet'
ALTER TABLE [dbo].[AdminSet]
ADD CONSTRAINT [PK_AdminSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ScenarioSet'
ALTER TABLE [dbo].[ScenarioSet]
ADD CONSTRAINT [PK_ScenarioSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserGroupSet'
ALTER TABLE [dbo].[UserGroupSet]
ADD CONSTRAINT [PK_UserGroupSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SensorTypeSet'
ALTER TABLE [dbo].[SensorTypeSet]
ADD CONSTRAINT [PK_SensorTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SmartThingTypeSet'
ALTER TABLE [dbo].[SmartThingTypeSet]
ADD CONSTRAINT [PK_SmartThingTypeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SmartThingTypeDictionarySet'
ALTER TABLE [dbo].[SmartThingTypeDictionarySet]
ADD CONSTRAINT [PK_SmartThingTypeDictionarySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SmartPlaceSet'
ALTER TABLE [dbo].[SmartPlaceSet]
ADD CONSTRAINT [PK_SmartPlaceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SensorSet'
ALTER TABLE [dbo].[SensorSet]
ADD CONSTRAINT [PK_SensorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SmartThingSet'
ALTER TABLE [dbo].[SmartThingSet]
ADD CONSTRAINT [PK_SmartThingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ControlBoxSet'
ALTER TABLE [dbo].[ControlBoxSet]
ADD CONSTRAINT [PK_ControlBoxSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LicenceDictionarySet'
ALTER TABLE [dbo].[LicenceDictionarySet]
ADD CONSTRAINT [PK_LicenceDictionarySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SensorSet_DoubleSensor'
ALTER TABLE [dbo].[SensorSet_DoubleSensor]
ADD CONSTRAINT [PK_SensorSet_DoubleSensor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SensorSet_BoolSensor'
ALTER TABLE [dbo].[SensorSet_BoolSensor]
ADD CONSTRAINT [PK_SensorSet_BoolSensor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SmartThingSet_BoolSmartThing'
ALTER TABLE [dbo].[SmartThingSet_BoolSmartThing]
ADD CONSTRAINT [PK_SmartThingSet_BoolSmartThing]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SmartThingSet_DoubleSmartThing1'
ALTER TABLE [dbo].[SmartThingSet_DoubleSmartThing1]
ADD CONSTRAINT [PK_SmartThingSet_DoubleSmartThing1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Scenaries_Id], [SensorTypes_Id] in table 'ScenarioSensorType'
ALTER TABLE [dbo].[ScenarioSensorType]
ADD CONSTRAINT [PK_ScenarioSensorType]
    PRIMARY KEY CLUSTERED ([Scenaries_Id], [SensorTypes_Id] ASC);
GO

-- Creating primary key on [Scenaries_Id], [SmartThingTypes_Id] in table 'ScenarioSmartThingType'
ALTER TABLE [dbo].[ScenarioSmartThingType]
ADD CONSTRAINT [PK_ScenarioSmartThingType]
    PRIMARY KEY CLUSTERED ([Scenaries_Id], [SmartThingTypes_Id] ASC);
GO

-- Creating primary key on [Scenaries_Id], [ControlBoxes_Id] in table 'ScenarioControlBox'
ALTER TABLE [dbo].[ScenarioControlBox]
ADD CONSTRAINT [PK_ScenarioControlBox]
    PRIMARY KEY CLUSTERED ([Scenaries_Id], [ControlBoxes_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Admin_Id] in table 'ScenarioSet'
ALTER TABLE [dbo].[ScenarioSet]
ADD CONSTRAINT [FK_AdminScenario]
    FOREIGN KEY ([Admin_Id])
    REFERENCES [dbo].[AdminSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdminScenario'
CREATE INDEX [IX_FK_AdminScenario]
ON [dbo].[ScenarioSet]
    ([Admin_Id]);
GO

-- Creating foreign key on [User_Id] in table 'ScenarioSet'
ALTER TABLE [dbo].[ScenarioSet]
ADD CONSTRAINT [FK_UserScenario]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserScenario'
CREATE INDEX [IX_FK_UserScenario]
ON [dbo].[ScenarioSet]
    ([User_Id]);
GO

-- Creating foreign key on [UserGroup_Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [FK_UserGroupUser]
    FOREIGN KEY ([UserGroup_Id])
    REFERENCES [dbo].[UserGroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupUser'
CREATE INDEX [IX_FK_UserGroupUser]
ON [dbo].[UserSet]
    ([UserGroup_Id]);
GO

-- Creating foreign key on [Scenaries_Id] in table 'ScenarioSensorType'
ALTER TABLE [dbo].[ScenarioSensorType]
ADD CONSTRAINT [FK_ScenarioSensorType_Scenario]
    FOREIGN KEY ([Scenaries_Id])
    REFERENCES [dbo].[ScenarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SensorTypes_Id] in table 'ScenarioSensorType'
ALTER TABLE [dbo].[ScenarioSensorType]
ADD CONSTRAINT [FK_ScenarioSensorType_SensorType]
    FOREIGN KEY ([SensorTypes_Id])
    REFERENCES [dbo].[SensorTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScenarioSensorType_SensorType'
CREATE INDEX [IX_FK_ScenarioSensorType_SensorType]
ON [dbo].[ScenarioSensorType]
    ([SensorTypes_Id]);
GO

-- Creating foreign key on [Scenaries_Id] in table 'ScenarioSmartThingType'
ALTER TABLE [dbo].[ScenarioSmartThingType]
ADD CONSTRAINT [FK_ScenarioSmartThingType_Scenario]
    FOREIGN KEY ([Scenaries_Id])
    REFERENCES [dbo].[ScenarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SmartThingTypes_Id] in table 'ScenarioSmartThingType'
ALTER TABLE [dbo].[ScenarioSmartThingType]
ADD CONSTRAINT [FK_ScenarioSmartThingType_SmartThingType]
    FOREIGN KEY ([SmartThingTypes_Id])
    REFERENCES [dbo].[SmartThingTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScenarioSmartThingType_SmartThingType'
CREATE INDEX [IX_FK_ScenarioSmartThingType_SmartThingType]
ON [dbo].[ScenarioSmartThingType]
    ([SmartThingTypes_Id]);
GO

-- Creating foreign key on [SmartThingType_Id] in table 'SmartThingTypeDictionarySet'
ALTER TABLE [dbo].[SmartThingTypeDictionarySet]
ADD CONSTRAINT [FK_SmartThingTypeSmartThingTypeDictionary]
    FOREIGN KEY ([SmartThingType_Id])
    REFERENCES [dbo].[SmartThingTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SmartThingTypeSmartThingTypeDictionary'
CREATE INDEX [IX_FK_SmartThingTypeSmartThingTypeDictionary]
ON [dbo].[SmartThingTypeDictionarySet]
    ([SmartThingType_Id]);
GO

-- Creating foreign key on [Parent_Id] in table 'UserGroupSet'
ALTER TABLE [dbo].[UserGroupSet]
ADD CONSTRAINT [FK_UserGroupUserGroup]
    FOREIGN KEY ([Parent_Id])
    REFERENCES [dbo].[UserGroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupUserGroup'
CREATE INDEX [IX_FK_UserGroupUserGroup]
ON [dbo].[UserGroupSet]
    ([Parent_Id]);
GO

-- Creating foreign key on [SensorType_Id] in table 'SensorSet'
ALTER TABLE [dbo].[SensorSet]
ADD CONSTRAINT [FK_SensorTypeSensor]
    FOREIGN KEY ([SensorType_Id])
    REFERENCES [dbo].[SensorTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SensorTypeSensor'
CREATE INDEX [IX_FK_SensorTypeSensor]
ON [dbo].[SensorSet]
    ([SensorType_Id]);
GO

-- Creating foreign key on [SmartThingType_Id] in table 'SmartThingSet'
ALTER TABLE [dbo].[SmartThingSet]
ADD CONSTRAINT [FK_SmartThingTypeSmartThing]
    FOREIGN KEY ([SmartThingType_Id])
    REFERENCES [dbo].[SmartThingTypeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SmartThingTypeSmartThing'
CREATE INDEX [IX_FK_SmartThingTypeSmartThing]
ON [dbo].[SmartThingSet]
    ([SmartThingType_Id]);
GO

-- Creating foreign key on [UserGroup_Id] in table 'SmartPlaceSet'
ALTER TABLE [dbo].[SmartPlaceSet]
ADD CONSTRAINT [FK_UserGroupSmartPlace]
    FOREIGN KEY ([UserGroup_Id])
    REFERENCES [dbo].[UserGroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupSmartPlace'
CREATE INDEX [IX_FK_UserGroupSmartPlace]
ON [dbo].[SmartPlaceSet]
    ([UserGroup_Id]);
GO

-- Creating foreign key on [UserGroup_Id] in table 'ControlBoxSet'
ALTER TABLE [dbo].[ControlBoxSet]
ADD CONSTRAINT [FK_UserGroupControlBox]
    FOREIGN KEY ([UserGroup_Id])
    REFERENCES [dbo].[UserGroupSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupControlBox'
CREATE INDEX [IX_FK_UserGroupControlBox]
ON [dbo].[ControlBoxSet]
    ([UserGroup_Id]);
GO

-- Creating foreign key on [SmartPlace_Id] in table 'SensorSet'
ALTER TABLE [dbo].[SensorSet]
ADD CONSTRAINT [FK_SmartPlaceSensor]
    FOREIGN KEY ([SmartPlace_Id])
    REFERENCES [dbo].[SmartPlaceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SmartPlaceSensor'
CREATE INDEX [IX_FK_SmartPlaceSensor]
ON [dbo].[SensorSet]
    ([SmartPlace_Id]);
GO

-- Creating foreign key on [ControlBox_Id] in table 'SensorSet'
ALTER TABLE [dbo].[SensorSet]
ADD CONSTRAINT [FK_ControlBoxSensor]
    FOREIGN KEY ([ControlBox_Id])
    REFERENCES [dbo].[ControlBoxSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlBoxSensor'
CREATE INDEX [IX_FK_ControlBoxSensor]
ON [dbo].[SensorSet]
    ([ControlBox_Id]);
GO

-- Creating foreign key on [ControlBox_Id] in table 'SmartThingSet'
ALTER TABLE [dbo].[SmartThingSet]
ADD CONSTRAINT [FK_ControlBoxSmartThing]
    FOREIGN KEY ([ControlBox_Id])
    REFERENCES [dbo].[ControlBoxSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlBoxSmartThing'
CREATE INDEX [IX_FK_ControlBoxSmartThing]
ON [dbo].[SmartThingSet]
    ([ControlBox_Id]);
GO

-- Creating foreign key on [SmartPlace_Id] in table 'SmartThingSet'
ALTER TABLE [dbo].[SmartThingSet]
ADD CONSTRAINT [FK_SmartPlaceSmartThing]
    FOREIGN KEY ([SmartPlace_Id])
    REFERENCES [dbo].[SmartPlaceSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SmartPlaceSmartThing'
CREATE INDEX [IX_FK_SmartPlaceSmartThing]
ON [dbo].[SmartThingSet]
    ([SmartPlace_Id]);
GO

-- Creating foreign key on [Scenaries_Id] in table 'ScenarioControlBox'
ALTER TABLE [dbo].[ScenarioControlBox]
ADD CONSTRAINT [FK_ScenarioControlBox_Scenario]
    FOREIGN KEY ([Scenaries_Id])
    REFERENCES [dbo].[ScenarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ControlBoxes_Id] in table 'ScenarioControlBox'
ALTER TABLE [dbo].[ScenarioControlBox]
ADD CONSTRAINT [FK_ScenarioControlBox_ControlBox]
    FOREIGN KEY ([ControlBoxes_Id])
    REFERENCES [dbo].[ControlBoxSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScenarioControlBox_ControlBox'
CREATE INDEX [IX_FK_ScenarioControlBox_ControlBox]
ON [dbo].[ScenarioControlBox]
    ([ControlBoxes_Id]);
GO

-- Creating foreign key on [Id] in table 'SensorSet_DoubleSensor'
ALTER TABLE [dbo].[SensorSet_DoubleSensor]
ADD CONSTRAINT [FK_DoubleSensor_inherits_Sensor]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[SensorSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'SensorSet_BoolSensor'
ALTER TABLE [dbo].[SensorSet_BoolSensor]
ADD CONSTRAINT [FK_BoolSensor_inherits_Sensor]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[SensorSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'SmartThingSet_BoolSmartThing'
ALTER TABLE [dbo].[SmartThingSet_BoolSmartThing]
ADD CONSTRAINT [FK_BoolSmartThing_inherits_SmartThing]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[SmartThingSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'SmartThingSet_DoubleSmartThing1'
ALTER TABLE [dbo].[SmartThingSet_DoubleSmartThing1]
ADD CONSTRAINT [FK_DoubleSmartThing1_inherits_SmartThing]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[SmartThingSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------