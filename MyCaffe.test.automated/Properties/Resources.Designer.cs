﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyCaffe.test.automated.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MyCaffe.test.automated.Properties.Resources", typeof(Resources).Assembly);
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
        ///CREATE DATABASE [%DBNAME%] ON  PRIMARY 
        ///( NAME = N&apos;%DBNAME%&apos;, FILENAME = N&apos;%PATH%\%DBFNAME%.mdf&apos; , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
        /// LOG ON 
        ///( NAME = N&apos;%DBNAME%_log&apos;, FILENAME = N&apos;%PATH%\%DBFNAME%_log.ldf&apos; , SIZE = 4096KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
        ///ALTER DATABASE [%DBNAME%] SET COMPATIBILITY_LEVEL = 100
        ///IF (1 = FULLTEXTSERVICEPROPERTY(&apos;IsFullTextInstalled&apos;))
        ///begin
        ///EXEC [%DBNAME%].[dbo].[sp_fulltext_database] @action = &apos;enable&apos;
        ///end
        ///ALTER DATABASE [ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateDatabase {
            get {
                return ResourceManager.GetString("CreateDatabase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[Sessions](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[TimeStamp] [datetime] NULL,
        ///	[Session] [varchar](256) NULL,
        ///	[TotalTestsRun] [int] NULL,
        ///	[TotalTestFailures] [int] NULL,
        ///	[TestFailureRate] [numeric](12, 5) NULL,
        ///	[TotalTestTiming] [numeric](12, 5) NULL,
        ///	[Path] [varchar](1024) NULL,
        /// CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMAR [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateSessionsTable {
            get {
                return ResourceManager.GetString("CreateSessionsTable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE [dbo].[Tests](
        ///	[ID] [int] IDENTITY(1,1) NOT NULL,
        ///	[TestGroup] [varchar](128) NULL,
        ///	[TestMethod] [varchar](128) NULL,
        ///	[Success] [bit] NULL,
        ///	[ErrorString] [varchar](1024) NULL,
        ///	[ErrorLocation] [varchar](1024) NULL,
        ///	[TestTiming] [numeric](12, 5) NULL,
        ///	[SessionID] [int] NULL,
        /// CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
        ///(
        ///	[ID] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRI [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string CreateTestsTable {
            get {
                return ResourceManager.GetString("CreateTestsTable", resourceCulture);
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
