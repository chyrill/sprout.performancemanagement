USE [master]
GO
/****** Object:  Database [sprout_performance]    Script Date: 19/10/2017 2:49:09 PM ******/
CREATE DATABASE [sprout_performance]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sprout_performance', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\sprout_performance.mdf' , SIZE = 2760704KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'sprout_performance_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\sprout_performance_log.ldf' , SIZE = 860160KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [sprout_performance] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sprout_performance].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [sprout_performance] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [sprout_performance] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [sprout_performance] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [sprout_performance] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [sprout_performance] SET ARITHABORT OFF 
GO
ALTER DATABASE [sprout_performance] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [sprout_performance] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [sprout_performance] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [sprout_performance] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [sprout_performance] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [sprout_performance] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [sprout_performance] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [sprout_performance] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [sprout_performance] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [sprout_performance] SET  ENABLE_BROKER 
GO
ALTER DATABASE [sprout_performance] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [sprout_performance] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [sprout_performance] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [sprout_performance] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [sprout_performance] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [sprout_performance] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [sprout_performance] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [sprout_performance] SET RECOVERY FULL 
GO
ALTER DATABASE [sprout_performance] SET  MULTI_USER 
GO
ALTER DATABASE [sprout_performance] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [sprout_performance] SET DB_CHAINING OFF 
GO
ALTER DATABASE [sprout_performance] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [sprout_performance] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [sprout_performance] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'sprout_performance', N'ON'
GO
ALTER DATABASE [sprout_performance] SET QUERY_STORE = OFF
GO
USE [sprout_performance]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [sprout_performance]
GO
/****** Object:  User [IIS APPPOOL\.NET v4.5]    Script Date: 19/10/2017 2:49:09 PM ******/
CREATE USER [IIS APPPOOL\.NET v4.5] FOR LOGIN [IIS APPPOOL\.NET v4.5] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\.NET v4.5]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\.NET v4.5]
GO
/****** Object:  Table [dbo].[AnswerItemData]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerItemData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeReviewId] [bigint] NOT NULL,
	[EmployeeScore] [decimal](10, 2) NULL,
	[EmployeeRemark] [varchar](max) NULL,
	[SupervisorScore] [decimal](10, 2) NULL,
	[SupervisorRemark] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeReviewData]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeReviewData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ReviewTemplateId] [bigint] NOT NULL,
	[Name] [varchar](120) NOT NULL,
	[Description] [varchar](250) NULL,
	[EmployeeId] [bigint] NOT NULL,
	[SupervisorId] [bigint] NOT NULL,
	[EmployeeAverageScore] [decimal](10, 2) NULL,
	[SupervisorAverageScore] [decimal](10, 2) NULL,
	[Rating] [varchar](25) NULL,
	[ReviewDate] [datetime] NOT NULL,
	[Status] [varchar](60) NOT NULL,
	[DateCreated] [datetime] NULL,
	[CreatedBy] [varchar](120) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RatingData]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RatingData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ReviewTemplateId] [bigint] NOT NULL,
	[RangeFrom] [decimal](10, 2) NOT NULL,
	[RangeTo] [decimal](10, 2) NOT NULL,
	[Description] [varchar](25) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReviewTemplateData]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewTemplateData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](120) NOT NULL,
	[Description] [varchar](250) NULL,
	[PointsPerItem] [int] NOT NULL,
	[Questions] [varchar](max) NOT NULL,
	[CreatedBy] [varchar](120) NULL,
	[DateCreated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfoData]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfoData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](60) NOT NULL,
	[LastName] [varchar](60) NOT NULL,
	[MiddleName] [varchar](60) NULL,
	[Email] [varchar](120) NOT NULL,
	[PhoneNumber] [varchar](20) NULL,
	[DateCreated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLoginData]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserInfoId] [bigint] NOT NULL,
	[Username] [varchar](120) NOT NULL,
	[Password] [varchar](120) NOT NULL,
	[AuthCode] [varchar](250) NULL,
	[IsAdmin] [bit] NULL,
	[ExpirationDate] [datetime] NULL,
	[Attempt] [int] NULL,
	[LastSuccessfulLogin] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserLoginData] ADD  DEFAULT ((1)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[UserLoginData] ADD  DEFAULT ((0)) FOR [Attempt]
GO
/****** Object:  StoredProcedure [dbo].[sp_AnswerItemData_add]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AnswerItemData_add]
	@EmployeeReviewId bigint,
	@EmployeeScore decimal(10,2),
	@EmployeeRemark varchar(max),
	@SupervisorScore decimal(10,2),
	@SupervisorRemark varchar(max)

	as
		
		insert into dbo.AnswerItemData (EmployeeReviewId,EmployeeScore,EmployeeRemark,SupervisorScore,SupervisorRemark) values (@EmployeeReviewId,@EmployeeScore,@EmployeeRemark,@SupervisorScore,@SupervisorRemark)
GO
/****** Object:  StoredProcedure [dbo].[sp_AnswerItemData_update]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AnswerItemData_update]
	@Id bigint,
	@EmployeeReviewId bigint,
	@EmployeeScore decimal(10,2),
	@EmployeeRemark varchar(max),
	@SupervisorScore decimal(10,2),
	@SupervisorRemark varchar(max)

	as
		
		update dbo.AnswerItemData set

		EmployeeReviewId=@EmployeeReviewId,
		EmployeeScore=@EmployeeScore,
		EmployeeRemark=@EmployeeRemark,
		SupervisorScore=@SupervisorScore,
		SupervisorRemark=@SupervisorRemark

		where Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[sp_EmployeeReviewData_add]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EmployeeReviewData_add]
	@ReviewTemplateId bigint,
	@Name varchar(120),
	@Description varchar(250),
	@EmployeeId bigint,
	@SupervisorId bigint,
	@EmployeeAverageScore decimal(10,2),
	@SupervisorAverageScore decimal(10,2),
	@Rating varchar(25),
	@ReviewDate datetime,
	@Status varchar(60),
	@DateCreated datetime,
	@CreatedBy varchar(120)

	as 

	insert into dbo.EmployeeReviewData (ReviewTemplateId,Name,Description,EmployeeId,SupervisorId,EmployeeAverageScore,SupervisorAverageScore,Rating,ReviewDate,Status,DateCreated,CreatedBy) values (@ReviewTemplateId,@Name,@Description,@EmployeeId,@SupervisorId,@EmployeeAverageScore,@SupervisorAverageScore,@Rating,@ReviewDate,@Status,@DateCreated,@CreatedBy)
GO
/****** Object:  StoredProcedure [dbo].[sp_EmployeeReviewData_update]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EmployeeReviewData_update]
	@Id bigint,
	@ReviewTemplateId bigint,
	@Name varchar(120),
	@Description varchar(250),
	@EmployeeId bigint,
	@SupervisorId bigint,
	@EmployeeAverageScore decimal(10,2),
	@SupervisorAverageScore decimal(10,2),
	@Rating varchar(25),
	@ReviewDate datetime,
	@Status varchar(60),
	@DateCreated datetime,
	@CreatedBy varchar(120)

	as 

	update dbo.EmployeeReviewData set
	
	ReviewTemplateId=@ReviewTemplateId,
	Name = @Name,
	Description= @Description,
	EmployeeId=@EmployeeId,
	SupervisorId=@SupervisorId,
	EmployeeAverageScore=@EmployeeAverageScore,
	SupervisorAverageScore=@SupervisorAverageScore,
	Rating=@Rating,
	ReviewDate=@ReviewDate,
	Status=@Status,
	DateCreated=@DateCreated,
	CreatedBy=@CreatedBy

	where Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[sp_RatingData_add]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RatingData_add]
	@ReviewTemplateId bigint,
	@RangeFrom decimal(10,2),
	@RangeTo decimal(10,2),
	@Description varchar(25)

	as

	insert into dbo.RatingData (ReviewTemplateId,RangeFrom,RangeTo,Description) values (@ReviewTemplateId,@RangeFrom,@RangeTo,@Description)
GO
/****** Object:  StoredProcedure [dbo].[sp_RatingData_update]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RatingData_update]
@Id bigint,
	@ReviewTemplateId bigint,
	@RangeFrom decimal(10,2),
	@RangeTo decimal(10,2),
	@Description varchar(25)

	as

	update dbo.RatingData set

	ReviewTemplateId = @ReviewTemplateId,
	RangeFrom=@RangeFrom,
	RangeTo=@RangeTo,
	Description=@Description

	where Id =@Id
GO
/****** Object:  StoredProcedure [dbo].[sp_ReviewTemplateData_add]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ReviewTemplateData_add]
	@Name varchar(120),
	@Description varchar(250),
	@PointsPerItem int,
	@Questions varchar(max),
	@CreatedBy varchar(120),
	@DateCreated datetime

	as 

	insert into dbo.ReviewTemplateData ( Name,Description,PointsPerItem,Questions,CreatedBy,DateCreated) values (@Name,@Description,@PointsPerItem,@Questions,@CreatedBy,@DateCreated)
GO
/****** Object:  StoredProcedure [dbo].[sp_ReviewTemplateData_update]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ReviewTemplateData_update]
@Id bigint,
	@Name varchar(120),
	@Description varchar(250),
	@PointsPerItem int,
	@Questions varchar(max),
	@CreatedBy varchar(120),
	@DateCreated datetime

	as 

	update dbo.ReviewTemplateData set
	Name=@Name,
	Description=@Description,
	PointsPerItem=@PointsPerItem,
	Questions=@Questions,
	CreatedBy=@CreatedBy,
	DateCreated=@DateCreated

	where Id =@Id
GO
/****** Object:  StoredProcedure [dbo].[sp_UserInfoData_add]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UserInfoData_add]
	@FirstName varchar(60),
	@LastName varchar(60),
	@MiddleName varchar(60),
	@Email varchar(60),
	@PhoneNumber varchar(20),
	@DateCreated datetime

	as 
		insert into dbo.UserInfoData (FirstName,LastName,MiddleName, Email, PhoneNumber, DateCreated) values (@FirstName,@LastName,@MiddleName,@Email,@PhoneNumber,@DateCreated)
GO
/****** Object:  StoredProcedure [dbo].[sp_UserInfoData_update]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UserInfoData_update]
	@Id bigint,
	@FirstName varchar(60),
	@LastName varchar(60),
	@MiddleName varchar(60),
	@Email varchar(60),
	@PhoneNumber varchar(20),
	@DateCreated datetime

	as 
		update dbo.UserInfoData set

		FirstName = @FirstName,
		LastName =@LastName,
		MiddleName = @MiddleName,
		Email =@Email,
		PhoneNumber =@PhoneNumber,
		DateCreated = @DateCreated

		where Id = Id
GO
/****** Object:  StoredProcedure [dbo].[sp_UserLoginData_add]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UserLoginData_add]
	@UserInfoId bigint,
	@Username varchar(120),
	@Password varchar(120),
	@AuthCode varchar(250),
	@IsAdmin bit,
	@ExpirationDate datetime,
	@Attempt int,
	@LastSuccessfulLogin datetime

	as
		insert into dbo.UserLoginData (UserInfoId, Username, Password, AuthCode,IsAdmin, ExpirationDate,Attempt,LastSuccessfulLogin) values (@UserInfoId,@Username,@Password,@AuthCode,@IsAdmin,@ExpirationDate,@Attempt,@LastSuccessfulLogin)
GO
/****** Object:  StoredProcedure [dbo].[sp_UserLoginData_update]    Script Date: 19/10/2017 2:49:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UserLoginData_update]
	@Id bigint,
	@UserInfoId bigint,
	@Username varchar(120),
	@Password varchar(120),
	@AuthCode varchar(250),
	@IsAdmin bit,
	@ExpirationDate datetime,
	@Attempt int,
	@LastSuccessfulLogin datetime

	as
		update dbo.UserLoginData set
		UserInfoId = @UserInfoId,
		Username = @Username,
		Password=@Password,
		AuthCode = @AuthCode,
		IsAdmin = @IsAdmin,
		ExpirationDate =@ExpirationDate,
		Attempt=@Attempt,
		LastSuccessfulLogin=@LastSuccessfulLogin

		where Id = Id
GO
USE [master]
GO
ALTER DATABASE [sprout_performance] SET  READ_WRITE 
GO
