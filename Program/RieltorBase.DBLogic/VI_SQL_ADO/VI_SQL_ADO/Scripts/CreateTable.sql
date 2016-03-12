USE [VolgInfoDB]
GO
/****** Object:  ForeignKey [FK_TypeApartament_PropertiesApartment_PropertiesApartment1]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[TypeApartment_PropertiesApartment] DROP CONSTRAINT [FK_TypeApartament_PropertiesApartment_PropertiesApartment1]
GO
/****** Object:  ForeignKey [FK_TypeApartament_PropertiesApartment_TypeApartament1]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[TypeApartment_PropertiesApartment] DROP CONSTRAINT [FK_TypeApartament_PropertiesApartment_TypeApartament1]
GO
/****** Object:  ForeignKey [FK_Agent_Firm]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Agent] DROP CONSTRAINT [FK_Agent_Firm]
GO
/****** Object:  ForeignKey [FK_Apartament_Agent]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment] DROP CONSTRAINT [FK_Apartament_Agent]
GO
/****** Object:  ForeignKey [FK_Apartament_TypeApartament]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment] DROP CONSTRAINT [FK_Apartament_TypeApartament]
GO
/****** Object:  ForeignKey [FK_Photo_Apartament]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[PhotoApartment] DROP CONSTRAINT [FK_Photo_Apartament]
GO
/****** Object:  ForeignKey [FK_PhotoApartment_Firm]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[PhotoApartment] DROP CONSTRAINT [FK_PhotoApartment_Firm]
GO
/****** Object:  ForeignKey [FK_Apartament_PropertiesApartment_Apartament1]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment_PropertiesApartment] DROP CONSTRAINT [FK_Apartament_PropertiesApartment_Apartament1]
GO
/****** Object:  ForeignKey [FK_Apartament_PropertiesApartment_PropertiesApartment1]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment_PropertiesApartment] DROP CONSTRAINT [FK_Apartament_PropertiesApartment_PropertiesApartment1]
GO
/****** Object:  Table [dbo].[Apartment_PropertiesApartment]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment_PropertiesApartment] DROP CONSTRAINT [FK_Apartament_PropertiesApartment_Apartament1]
GO
ALTER TABLE [dbo].[Apartment_PropertiesApartment] DROP CONSTRAINT [FK_Apartament_PropertiesApartment_PropertiesApartment1]
GO
DROP TABLE [dbo].[Apartment_PropertiesApartment]
GO
/****** Object:  Table [dbo].[PhotoApartment]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[PhotoApartment] DROP CONSTRAINT [FK_Photo_Apartament]
GO
ALTER TABLE [dbo].[PhotoApartment] DROP CONSTRAINT [FK_PhotoApartment_Firm]
GO
DROP TABLE [dbo].[PhotoApartment]
GO
/****** Object:  Table [dbo].[Apartment]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment] DROP CONSTRAINT [FK_Apartament_Agent]
GO
ALTER TABLE [dbo].[Apartment] DROP CONSTRAINT [FK_Apartament_TypeApartament]
GO
DROP TABLE [dbo].[Apartment]
GO
/****** Object:  Table [dbo].[Agent]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Agent] DROP CONSTRAINT [FK_Agent_Firm]
GO
DROP TABLE [dbo].[Agent]
GO
/****** Object:  Table [dbo].[TypeApartment_PropertiesApartment]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[TypeApartment_PropertiesApartment] DROP CONSTRAINT [FK_TypeApartament_PropertiesApartment_PropertiesApartment1]
GO
ALTER TABLE [dbo].[TypeApartment_PropertiesApartment] DROP CONSTRAINT [FK_TypeApartament_PropertiesApartment_TypeApartament1]
GO
DROP TABLE [dbo].[TypeApartment_PropertiesApartment]
GO
/****** Object:  Table [dbo].[Action]    Script Date: 02/17/2016 18:10:38 ******/
DROP TABLE [dbo].[Action]
GO
/****** Object:  Table [dbo].[PropertiesApartment]    Script Date: 02/17/2016 18:10:38 ******/
DROP TABLE [dbo].[PropertiesApartment]
GO
/****** Object:  Table [dbo].[TypeApartment]    Script Date: 02/17/2016 18:10:38 ******/
DROP TABLE [dbo].[TypeApartment]
GO
/****** Object:  Table [dbo].[Changelog]    Script Date: 02/17/2016 18:10:38 ******/
DROP TABLE [dbo].[Changelog]
GO
/****** Object:  Table [dbo].[ChangelogAgent]    Script Date: 02/17/2016 18:10:38 ******/
DROP TABLE [dbo].[ChangelogAgent]
GO
/****** Object:  Table [dbo].[Firm]    Script Date: 02/17/2016 18:10:37 ******/
DROP TABLE [dbo].[Firm]
GO
/****** Object:  Table [dbo].[Firm]    Script Date: 02/17/2016 18:10:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Firm](
	[Id_firm] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Firm] PRIMARY KEY CLUSTERED 
(
	[Id_firm] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangelogAgent]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangelogAgent](
	[Id_agentCangelog] [int] IDENTITY(1,1) NOT NULL,
	[DateChange] [date] NOT NULL,
	[Description] [text] NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[AgentWhoChanges] [xml] NOT NULL,
	[AgentInfo] [xml] NOT NULL,
 CONSTRAINT [PK_AgentChangelog] PRIMARY KEY CLUSTERED 
(
	[Id_agentCangelog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Информация об агенте который изменял данные агента' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ChangelogAgent', @level2type=N'COLUMN',@level2name=N'AgentWhoChanges'
GO
/****** Object:  Table [dbo].[Changelog]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Changelog](
	[Id_changelog] [int] NOT NULL,
	[DateChange] [date] NOT NULL,
	[Description] [text] NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[AgentWhoChanges] [xml] NOT NULL,
	[ApartmentInfo] [xml] NOT NULL,
 CONSTRAINT [PK_Changelog] PRIMARY KEY CLUSTERED 
(
	[Id_changelog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Информация об агенте который изменял данные квартиры' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Changelog', @level2type=N'COLUMN',@level2name=N'AgentWhoChanges'
GO
/****** Object:  Table [dbo].[TypeApartment]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeApartment](
	[id_typeApartment] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TypeApartament] PRIMARY KEY CLUSTERED 
(
	[id_typeApartment] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropertiesApartment]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertiesApartment](
	[Id_propertiesApartment] [int] IDENTITY(1,1) NOT NULL,
	[NameProperties] [nvarchar](50) NOT NULL,
	[TypeProperties] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PropertiesApartment] PRIMARY KEY CLUSTERED 
(
	[Id_propertiesApartment] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Action]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Action](
	[Id_action] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Додумать' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action', @level2type=N'COLUMN',@level2name=N'Id_action'
GO
/****** Object:  Table [dbo].[TypeApartment_PropertiesApartment]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeApartment_PropertiesApartment](
	[id_typeApartment] [int] NOT NULL,
	[Id_propertiesApartment] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Agent]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agent](
	[Id_agent] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Addres] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Id_firm] [int] NOT NULL,
	[FirmAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED 
(
	[Id_agent] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Может ли юзер добовлять и удалять агентов и редактировать все квартиры фирмы и инфу о фирме' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agent', @level2type=N'COLUMN',@level2name=N'FirmAdmin'
GO
/****** Object:  Table [dbo].[Apartment]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apartment](
	[Id_apartment] [int] IDENTITY(1,1) NOT NULL,
	[Id_typeApartment] [int] NOT NULL,
	[Id_agent] [int] NULL,
 CONSTRAINT [PK_Apartament] PRIMARY KEY CLUSTERED 
(
	[Id_apartment] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhotoApartment]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoApartment](
	[Id_photo] [int] IDENTITY(1,1) NOT NULL,
	[Name] [image] NOT NULL,
	[Id_apartment] [int] NOT NULL,
	[Id_firm] [int] NOT NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[Id_photo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Apartment_PropertiesApartment]    Script Date: 02/17/2016 18:10:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apartment_PropertiesApartment](
	[Id_apartment] [int] NOT NULL,
	[Id_propertiesApartment] [int] NOT NULL,
	[ValueProperties] [nvarchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_TypeApartament_PropertiesApartment_PropertiesApartment1]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[TypeApartment_PropertiesApartment]  WITH CHECK ADD  CONSTRAINT [FK_TypeApartament_PropertiesApartment_PropertiesApartment1] FOREIGN KEY([Id_propertiesApartment])
REFERENCES [dbo].[PropertiesApartment] ([Id_propertiesApartment])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TypeApartment_PropertiesApartment] CHECK CONSTRAINT [FK_TypeApartament_PropertiesApartment_PropertiesApartment1]
GO
/****** Object:  ForeignKey [FK_TypeApartament_PropertiesApartment_TypeApartament1]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[TypeApartment_PropertiesApartment]  WITH CHECK ADD  CONSTRAINT [FK_TypeApartament_PropertiesApartment_TypeApartament1] FOREIGN KEY([id_typeApartment])
REFERENCES [dbo].[TypeApartment] ([id_typeApartment])
GO
ALTER TABLE [dbo].[TypeApartment_PropertiesApartment] CHECK CONSTRAINT [FK_TypeApartament_PropertiesApartment_TypeApartament1]
GO
/****** Object:  ForeignKey [FK_Agent_Firm]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Agent]  WITH CHECK ADD  CONSTRAINT [FK_Agent_Firm] FOREIGN KEY([Id_firm])
REFERENCES [dbo].[Firm] ([Id_firm])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Agent] CHECK CONSTRAINT [FK_Agent_Firm]
GO
/****** Object:  ForeignKey [FK_Apartament_Agent]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartament_Agent] FOREIGN KEY([Id_agent])
REFERENCES [dbo].[Agent] ([Id_agent])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Apartment] CHECK CONSTRAINT [FK_Apartament_Agent]
GO
/****** Object:  ForeignKey [FK_Apartament_TypeApartament]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartament_TypeApartament] FOREIGN KEY([Id_typeApartment])
REFERENCES [dbo].[TypeApartment] ([id_typeApartment])
GO
ALTER TABLE [dbo].[Apartment] CHECK CONSTRAINT [FK_Apartament_TypeApartament]
GO
/****** Object:  ForeignKey [FK_Photo_Apartament]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[PhotoApartment]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Apartament] FOREIGN KEY([Id_apartment])
REFERENCES [dbo].[Apartment] ([Id_apartment])
GO
ALTER TABLE [dbo].[PhotoApartment] CHECK CONSTRAINT [FK_Photo_Apartament]
GO
/****** Object:  ForeignKey [FK_PhotoApartment_Firm]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[PhotoApartment]  WITH CHECK ADD  CONSTRAINT [FK_PhotoApartment_Firm] FOREIGN KEY([Id_firm])
REFERENCES [dbo].[Firm] ([Id_firm])
GO
ALTER TABLE [dbo].[PhotoApartment] CHECK CONSTRAINT [FK_PhotoApartment_Firm]
GO
/****** Object:  ForeignKey [FK_Apartament_PropertiesApartment_Apartament1]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment_PropertiesApartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartament_PropertiesApartment_Apartament1] FOREIGN KEY([Id_apartment])
REFERENCES [dbo].[Apartment] ([Id_apartment])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Apartment_PropertiesApartment] CHECK CONSTRAINT [FK_Apartament_PropertiesApartment_Apartament1]
GO
/****** Object:  ForeignKey [FK_Apartament_PropertiesApartment_PropertiesApartment1]    Script Date: 02/17/2016 18:10:38 ******/
ALTER TABLE [dbo].[Apartment_PropertiesApartment]  WITH CHECK ADD  CONSTRAINT [FK_Apartament_PropertiesApartment_PropertiesApartment1] FOREIGN KEY([Id_propertiesApartment])
REFERENCES [dbo].[PropertiesApartment] ([Id_propertiesApartment])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Apartment_PropertiesApartment] CHECK CONSTRAINT [FK_Apartament_PropertiesApartment_PropertiesApartment1]
GO
