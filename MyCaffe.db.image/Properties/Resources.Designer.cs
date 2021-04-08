﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCaffe.db.image.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MyCaffe.db.image.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USE [master]
        ///
        ///CREATE DATABASE [%DBNAME%] ON  PRIMARY 
        ///( NAME = N&apos;%DBNAME%&apos;, FILENAME = N&apos;%PATH%\%DBNAME%.mdf&apos; , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
        /// LOG ON 
        ///( NAME = N&apos;%DBNAME%_log&apos;, FILENAME = N&apos;%PATH%\%DBNAME%_log.ldf&apos; , MAXSIZE = 2048GB , FILEGROWTH = 10%)
        ///
        ///ALTER DATABASE [%DBNAME%] SET COMPATIBILITY_LEVEL = 100
        ///
        ///ALTER DATABASE [%DBNAME%] SET ANSI_NULL_DEFAULT OFF 
        ///ALTER DATABASE [%DBNAME%] SET ANSI_NULLS OFF 
        ///ALTER DATABASE [%DBNAME%] SET ANSI_PADDING OFF 
        ///ALTER DATABASE [%DBNAME%] SE [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateDatabase {
            get {
                return ResourceManager.GetString("CreateDatabase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[DatasetCreators](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Name] [varchar](256) NULL,
        ///	[Path] [varchar](1024) NULL,
        /// CONSTRAINT [PK_DatasetCreators] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY].
        /// </summary>
        internal static string CreateDatasetCreatorsTable {
            get {
                return ResourceManager.GetString("CreateDatasetCreatorsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[DatasetGroups](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Name] [varchar](128) NULL,
        ///	[DatasetCreatorID] [int] NULL,
        ///	[OwnerID] [nvarchar](60) NULL,
        /// CONSTRAINT [PK_DatasetGroups] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY].
        /// </summary>
        internal static string CreateDatasetGroupsTable {
            get {
                return ResourceManager.GetString("CreateDatasetGroupsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[DatasetParameters](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Name] [nvarchar](256) NULL,
        ///	[Value] [nvarchar](1024) NULL,
        ///	[DatasetID] [int] NULL,
        /// CONSTRAINT [PK_DatasetParameters] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY].
        /// </summary>
        internal static string CreateDatasetParametersTable {
            get {
                return ResourceManager.GetString("CreateDatasetParametersTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[Datasets](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Name] [nvarchar](512) NULL,
        ///	[TrainingSourceID] [int] NULL,
        ///	[TestingSourceID] [int] NULL,
        ///	[DatasetGroupID] [int] NULL,
        ///	[DatasetCreatorID] [int] NULL,
        ///	[ModelGroupID] [int] NULL,
        ///	[ImageHeight] [int] NULL,
        ///	[ImageWidth] [int] NULL,
        ///	[ImageChannels] [int] NULL,
        ///	[ImageEncoded] [bit] NULL,
        ///	[TrainingTotal] [int] NULL,
        ///	[TestingTotal] [int] NULL,
        ///	[TestingPercent] [numeric](12, 5) NULL,
        ///	[Relabeled] [bit] NULL,
        ///	[OwnerID] [n [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateDatasetsTable {
            get {
                return ResourceManager.GetString("CreateDatasetsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[LabelBoosts](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[ProjectID] [int] NULL,
        ///	[ActiveLabel] [int] NULL,
        ///	[Boost] [numeric](12, 5) NULL,
        ///	[SourceID] [int] NULL,
        /// CONSTRAINT [PK_LabelBoosts] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY].
        /// </summary>
        internal static string CreateLabelBoostsTable {
            get {
                return ResourceManager.GetString("CreateLabelBoostsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[Labels](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Label] [int] NULL,
        ///	[Name] [nvarchar](128) NULL,
        ///	[SourceID] [int] NULL,
        ///	[ImageCount] [int] NULL,
        ///	[ActiveLabel] [int] NULL,
        /// CONSTRAINT [PK_Labels] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY].
        /// </summary>
        internal static string CreateLabelsTable {
            get {
                return ResourceManager.GetString("CreateLabelsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[ModelGroups](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Name] [varchar](512) NULL,
        ///	[OwnerID] [nvarchar](60) NULL,
        /// CONSTRAINT [PK_ModelGroups] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY].
        /// </summary>
        internal static string CreateModelGroupsTable {
            get {
                return ResourceManager.GetString("CreateModelGroupsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[RawImageGroups](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Idx] [int] NULL,
        ///	[StartDate] [smalldatetime] NULL,
        ///	[EndDate] [smalldatetime] NULL,
        ///	[Property1] [numeric](12, 5) NULL,
        ///	[Property2] [numeric](12, 5) NULL,
        ///	[Property3] [numeric](12, 5) NULL,
        ///	[Property4] [numeric](12, 5) NULL,
        ///	[Property5] [numeric](12, 5) NULL,
        ///	[Rating] [numeric](12, 5) NULL,
        ///	[RawData] [image] NULL,
        /// CONSTRAINT [PK_LevelDBGroups] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTI [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateRawImageGroupsTable {
            get {
                return ResourceManager.GetString("CreateRawImageGroupsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[RawImageMeans](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Height] [int] NULL,
        ///	[Width] [int] NULL,
        ///	[Channels] [int] NULL,
        ///	[Data] [image] NULL,
        ///	[Encoded] [bit] NULL,
        ///	[SourceID] [int] NULL,
        /// CONSTRAINT [PK_ImageMeans] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY].
        /// </summary>
        internal static string CreateRawImageMeansTable {
            get {
                return ResourceManager.GetString("CreateRawImageMeansTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[RawImageParameters](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Name] [nvarchar](256) NULL,
        ///	[Value] [image] NULL,
        ///	[SourceID] [int] NULL,
        ///	[RawImageID] [int] NULL,
        ///	[TextValue] [nvarchar](1024) NULL,
        ///	[NumericValue] [numeric](12, 5) NULL,
        ///	[NumericValue2] [real] NULL,
        /// CONSTRAINT [PK_RawImageParameters] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ///) ON [PRI [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateRawImageParametersTable {
            get {
                return ResourceManager.GetString("CreateRawImageParametersTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[RawImageResults](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Idx] [int] NULL,
        ///	[Label] [int] NULL,
        ///	[ResultCount] [int] NULL,
        ///	[Results] [image] NULL,
        ///	[SourceID] [int] NULL,
        ///	[TimeStamp] [datetime] NULL,
        /// CONSTRAINT [PK_RawImageResults] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY].
        /// </summary>
        internal static string CreateRawImageResultsTable {
            get {
                return ResourceManager.GetString("CreateRawImageResultsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[RawImages](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Height] [int] NULL,
        ///	[Width] [int] NULL,
        ///	[Channels] [int] NULL,
        ///	[Data] [image] NULL,
        ///	[DebugData] [image] NULL,
        ///	[TimeStamp] [datetime] NULL,
        ///	[Encoded] [bit] NULL,
        ///	[SourceID] [int] NULL,
        ///	[Idx] [int] NULL,
        ///	[GroupID] [int] NULL,
        ///	[OriginalBoost] [smallint] NULL,
        ///	[ActiveBoost] [smallint] NULL,
        ///	[AutoLabel] [bit] NULL,
        ///	[VirtualID] [int] NULL,
        ///	[RawData] [image] NULL,
        ///	[DataCriteria] [image] NULL,
        ///	[OriginalLabel] [in [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateRawImagesTable {
            get {
                return ResourceManager.GetString("CreateRawImagesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[SourceParameters](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Name] [nvarchar](256) NULL,
        ///	[Value] [nvarchar](1024) NULL,
        ///	[SourceID] [int] NULL,
        /// CONSTRAINT [PK_SourceParameters] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY].
        /// </summary>
        internal static string CreateSourceParametersTable {
            get {
                return ResourceManager.GetString("CreateSourceParametersTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[Sources](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[Name] [nvarchar](512) NULL,
        ///	[ImageHeight] [int] NULL,
        ///	[ImageWidth] [int] NULL,
        ///	[ImageChannels] [int] NULL,
        ///	[ImageEncoded] [bit] NULL,
        ///	[ImageCount] [int] NULL,
        ///	[OwnerID] [nvarchar](60) NULL,
        ///	[SaveImagesToFile] [bit] NULL,
        ///	[CopyOfSourceID] [int] NULL,
        /// CONSTRAINT [PK_Sources] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateSourcesTable {
            get {
                return ResourceManager.GetString("CreateSourcesTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF (EXISTS (SELECT name 
        ///FROM master.dbo.sysdatabases 
        ///WHERE (&apos;[&apos; + name + &apos;]&apos; = &apos;%DBNAME%&apos; 
        ///OR name = &apos;%DBNAME%&apos;)))
        ///SELECT(1)
        ///ELSE
        ///SELECT(0).
        /// </summary>
        internal static string QueryDatabaseExists {
            get {
                return ResourceManager.GetString("QueryDatabaseExists", resourceCulture);
            }
        }
    }
}
