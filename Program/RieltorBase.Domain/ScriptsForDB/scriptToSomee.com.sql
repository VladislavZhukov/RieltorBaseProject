USE [VolgaInfoDB]

ALTER DATABASE [VolgaInfoDB] SET COMPATIBILITY_LEVEL = 100

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VolgaInfoDB].[dbo].[sp_fulltext_database] @action = 'enable'
end

ALTER DATABASE [VolgaInfoDB] SET ANSI_NULL_DEFAULT OFF 

ALTER DATABASE [VolgaInfoDB] SET ANSI_NULLS OFF 

ALTER DATABASE [VolgaInfoDB] SET ANSI_PADDING OFF 

ALTER DATABASE [VolgaInfoDB] SET ANSI_WARNINGS OFF 

ALTER DATABASE [VolgaInfoDB] SET ARITHABORT OFF 

ALTER DATABASE [VolgaInfoDB] SET AUTO_CLOSE OFF 

ALTER DATABASE [VolgaInfoDB] SET AUTO_CREATE_STATISTICS ON 

ALTER DATABASE [VolgaInfoDB] SET AUTO_SHRINK OFF 

ALTER DATABASE [VolgaInfoDB] SET AUTO_UPDATE_STATISTICS ON 

ALTER DATABASE [VolgaInfoDB] SET CURSOR_CLOSE_ON_COMMIT OFF 

ALTER DATABASE [VolgaInfoDB] SET CURSOR_DEFAULT  GLOBAL 

ALTER DATABASE [VolgaInfoDB] SET CONCAT_NULL_YIELDS_NULL OFF 

ALTER DATABASE [VolgaInfoDB] SET NUMERIC_ROUNDABORT OFF 

ALTER DATABASE [VolgaInfoDB] SET QUOTED_IDENTIFIER OFF 

ALTER DATABASE [VolgaInfoDB] SET RECURSIVE_TRIGGERS OFF 

ALTER DATABASE [VolgaInfoDB] SET  DISABLE_BROKER 

ALTER DATABASE [VolgaInfoDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 

ALTER DATABASE [VolgaInfoDB] SET DATE_CORRELATION_OPTIMIZATION OFF 

ALTER DATABASE [VolgaInfoDB] SET TRUSTWORTHY OFF 

ALTER DATABASE [VolgaInfoDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 

ALTER DATABASE [VolgaInfoDB] SET PARAMETERIZATION SIMPLE 

ALTER DATABASE [VolgaInfoDB] SET READ_COMMITTED_SNAPSHOT OFF 

ALTER DATABASE [VolgaInfoDB] SET HONOR_BROKER_PRIORITY OFF 

ALTER DATABASE [VolgaInfoDB] SET RECOVERY FULL 

ALTER DATABASE [VolgaInfoDB] SET  MULTI_USER 

ALTER DATABASE [VolgaInfoDB] SET PAGE_VERIFY CHECKSUM  

ALTER DATABASE [VolgaInfoDB] SET DB_CHAINING OFF 

EXEC sys.sp_db_vardecimal_storage_format N'VolgaInfoDB', N'ON'

USE [VolgaInfoDB]

/****** Object:  Table [dbo].[Action]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Action](
	[Id_action] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED 
(
	[Id_action] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Agent]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Agent](
	[Id_agent] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Addres] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Id_firm] [int] NOT NULL,
	[IsFirmAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED 
(
	[Id_agent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Changelog]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Changelog](
	[ChangeLogId] [int] NOT NULL,
	[ChangeDate] [date] NOT NULL,
	[Description] [text] NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[AgentWhoChanges] [xml] NOT NULL,
	[RealtyObjectInfo] [xml] NOT NULL,
 CONSTRAINT [PK_Changelog] PRIMARY KEY CLUSTERED 
(
	[ChangeLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[ChangelogAgent]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[ChangelogAgent](
	[ChangelogAgentId] [int] IDENTITY(1,1) NOT NULL,
	[ChangeDate] [date] NOT NULL,
	[Description] [text] NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[AgentWhoChanges] [xml] NOT NULL,
	[AgentInfo] [xml] NOT NULL,
 CONSTRAINT [PK_AgentChangelog] PRIMARY KEY CLUSTERED 
(
	[ChangelogAgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[Firm]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Firm](
	[FirmId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Firm] PRIMARY KEY CLUSTERED 
(
	[FirmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[Photo]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Photo](
	[PhotoId] [int] IDENTITY(1,1) NOT NULL,
	[RealtyObjectId] [int] NOT NULL,
	[FirmId] [int] NOT NULL,
	[RelativeSource] [nvarchar](200) NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[PhotoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[PropertyType]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[PropertyType](
	[PropertyTypeId] [int] IDENTITY(1,1) NOT NULL,
	[PropertyName] [nvarchar](50) NOT NULL,
	[PropertyValueType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PropertiesApartment] PRIMARY KEY CLUSTERED 
(
	[PropertyTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[PropertyValue]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[PropertyValue](
	[RealtyObjectId] [int] NOT NULL,
	[PropertyTypeId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_PropertyValue] PRIMARY KEY CLUSTERED 
(
	[RealtyObjectId] ASC,
	[PropertyTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[RealtyObject]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[RealtyObject](
	[RealtyObjectId] [int] IDENTITY(1,1) NOT NULL,
	[RealtyObjectTypeId] [int] NOT NULL,
	[AgentId] [int] NULL,
 CONSTRAINT [PK_Apartament] PRIMARY KEY CLUSTERED 
(
	[RealtyObjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[RealtyObjectType]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[RealtyObjectType](
	[RealtyObjectTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TypeApartament] PRIMARY KEY CLUSTERED 
(
	[RealtyObjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [dbo].[RealtyObjectType_PropertyType]    Script Date: 21.03.2016 19:39:13 ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[RealtyObjectType_PropertyType](
	[RealtyObjectTypeId] [int] NOT NULL,
	[PropertyTypeId] [int] NOT NULL,
 CONSTRAINT [PK_RealtyObjectType_PropertyType] PRIMARY KEY CLUSTERED 
(
	[RealtyObjectTypeId] ASC,
	[PropertyTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[Agent]  WITH CHECK ADD  CONSTRAINT [FK_Agent_Firm] FOREIGN KEY([Id_firm])
REFERENCES [dbo].[Firm] ([FirmId])
ON UPDATE CASCADE
ON DELETE CASCADE

ALTER TABLE [dbo].[Agent] CHECK CONSTRAINT [FK_Agent_Firm]

ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Apartament] FOREIGN KEY([RealtyObjectId])
REFERENCES [dbo].[RealtyObject] ([RealtyObjectId])

ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Apartament]

ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_PhotoApartment_Firm] FOREIGN KEY([FirmId])
REFERENCES [dbo].[Firm] ([FirmId])

ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_PhotoApartment_Firm]

ALTER TABLE [dbo].[PropertyValue]  WITH CHECK ADD  CONSTRAINT [FK_Apartament_PropertiesApartment_Apartament1] FOREIGN KEY([RealtyObjectId])
REFERENCES [dbo].[RealtyObject] ([RealtyObjectId])
ON UPDATE CASCADE
ON DELETE CASCADE

ALTER TABLE [dbo].[PropertyValue] CHECK CONSTRAINT [FK_Apartament_PropertiesApartment_Apartament1]

ALTER TABLE [dbo].[PropertyValue]  WITH CHECK ADD  CONSTRAINT [FK_Apartament_PropertiesApartment_PropertiesApartment1] FOREIGN KEY([PropertyTypeId])
REFERENCES [dbo].[PropertyType] ([PropertyTypeId])
ON UPDATE CASCADE
ON DELETE CASCADE

ALTER TABLE [dbo].[PropertyValue] CHECK CONSTRAINT [FK_Apartament_PropertiesApartment_PropertiesApartment1]

ALTER TABLE [dbo].[RealtyObject]  WITH CHECK ADD  CONSTRAINT [FK_Apartament_Agent] FOREIGN KEY([AgentId])
REFERENCES [dbo].[Agent] ([Id_agent])
ON UPDATE CASCADE
ON DELETE CASCADE

ALTER TABLE [dbo].[RealtyObject] CHECK CONSTRAINT [FK_Apartament_Agent]

ALTER TABLE [dbo].[RealtyObject]  WITH CHECK ADD  CONSTRAINT [FK_Apartament_TypeApartament] FOREIGN KEY([RealtyObjectTypeId])
REFERENCES [dbo].[RealtyObjectType] ([RealtyObjectTypeId])

ALTER TABLE [dbo].[RealtyObject] CHECK CONSTRAINT [FK_Apartament_TypeApartament]

ALTER TABLE [dbo].[RealtyObjectType_PropertyType]  WITH CHECK ADD  CONSTRAINT [FK_TypeApartament_PropertiesApartment_PropertiesApartment1] FOREIGN KEY([PropertyTypeId])
REFERENCES [dbo].[PropertyType] ([PropertyTypeId])
ON UPDATE CASCADE
ON DELETE CASCADE

ALTER TABLE [dbo].[RealtyObjectType_PropertyType] CHECK CONSTRAINT [FK_TypeApartament_PropertiesApartment_PropertiesApartment1]

ALTER TABLE [dbo].[RealtyObjectType_PropertyType]  WITH CHECK ADD  CONSTRAINT [FK_TypeApartament_PropertiesApartment_TypeApartament1] FOREIGN KEY([RealtyObjectTypeId])
REFERENCES [dbo].[RealtyObjectType] ([RealtyObjectTypeId])

ALTER TABLE [dbo].[RealtyObjectType_PropertyType] CHECK CONSTRAINT [FK_TypeApartament_PropertiesApartment_TypeApartament1]

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Додумать' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action', @level2type=N'COLUMN',@level2name=N'Id_action'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Может ли юзер добовлять и удалять агентов и редактировать все квартиры фирмы и инфу о фирме' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agent', @level2type=N'COLUMN',@level2name=N'IsFirmAdmin'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Информация об агенте который изменял данные квартиры' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Changelog', @level2type=N'COLUMN',@level2name=N'AgentWhoChanges'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Информация об агенте который изменял данные агента' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangelogAgent', @level2type=N'COLUMN',@level2name=N'AgentWhoChanges'

USE [master]

ALTER DATABASE [VolgaInfoDB] SET  READ_WRITE 

