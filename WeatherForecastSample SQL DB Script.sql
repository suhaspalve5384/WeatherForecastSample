USE [master]
GO
/****** Object:  Database [WeatherForecastSample]    Script Date: 19-04-2023 01:48:44 AM ******/
CREATE DATABASE [WeatherForecastSample]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WeatherForecastSample', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\WeatherForecastSample.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WeatherForecastSample_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\WeatherForecastSample_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [WeatherForecastSample] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WeatherForecastSample].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WeatherForecastSample] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET ARITHABORT OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WeatherForecastSample] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WeatherForecastSample] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WeatherForecastSample] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WeatherForecastSample] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET RECOVERY FULL 
GO
ALTER DATABASE [WeatherForecastSample] SET  MULTI_USER 
GO
ALTER DATABASE [WeatherForecastSample] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WeatherForecastSample] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WeatherForecastSample] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WeatherForecastSample] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WeatherForecastSample] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'WeatherForecastSample', N'ON'
GO
ALTER DATABASE [WeatherForecastSample] SET QUERY_STORE = OFF
GO
USE [WeatherForecastSample]
GO
/****** Object:  Table [dbo].[CurrentWeather]    Script Date: 19-04-2023 01:48:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrentWeather](
	[CurrentWeatherId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[temperature] [numeric](5, 2) NULL,
	[windspeed] [numeric](5, 2) NULL,
	[winddirection] [numeric](5, 2) NULL,
	[weathercode] [int] NULL,
	[is_day] [bit] NULL,
	[time] [datetime] NULL,
 CONSTRAINT [PK_CurrentWeather] PRIMARY KEY CLUSTERED 
(
	[CurrentWeatherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DailyWeather]    Script Date: 19-04-2023 01:48:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyWeather](
	[DailyWeatherId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[time] [datetime] NULL,
	[wheathertype] [nvarchar](100) NULL,
	[wheatherTypeValue] [numeric](7, 2) NULL,
 CONSTRAINT [PK_DailyWeather] PRIMARY KEY CLUSTERED 
(
	[DailyWeatherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HourlyWeather]    Script Date: 19-04-2023 01:48:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HourlyWeather](
	[HourlyWeatherId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[time] [datetime] NULL,
	[wheathertype] [nvarchar](100) NULL,
	[wheatherTypeValue] [numeric](7, 2) NULL,
 CONSTRAINT [PK_HourlyWeather] PRIMARY KEY CLUSTERED 
(
	[HourlyWeatherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 19-04-2023 01:48:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[latitude] [numeric](7, 3) NOT NULL,
	[longitude] [numeric](7, 3) NOT NULL,
	[timezone] [nvarchar](200) NULL,
	[timezone_abbreviation] [nvarchar](50) NULL,
	[elevation] [numeric](7, 3) NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CurrentWeather]  WITH CHECK ADD  CONSTRAINT [FK_CurrentWeather_Locations] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([LocationId])
GO
ALTER TABLE [dbo].[CurrentWeather] CHECK CONSTRAINT [FK_CurrentWeather_Locations]
GO
ALTER TABLE [dbo].[DailyWeather]  WITH CHECK ADD  CONSTRAINT [FK_DailyWeather_Locations] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([LocationId])
GO
ALTER TABLE [dbo].[DailyWeather] CHECK CONSTRAINT [FK_DailyWeather_Locations]
GO
ALTER TABLE [dbo].[HourlyWeather]  WITH CHECK ADD  CONSTRAINT [FK_HourlyWeather_Locations] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Locations] ([LocationId])
GO
ALTER TABLE [dbo].[HourlyWeather] CHECK CONSTRAINT [FK_HourlyWeather_Locations]
GO
USE [master]
GO
ALTER DATABASE [WeatherForecastSample] SET  READ_WRITE 
GO
