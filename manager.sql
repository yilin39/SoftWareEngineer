USE [manager]
GO
/****** Object:  Table [dbo].[user]    Script Date: 05/14/2020 13:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[uid] [nchar](30) NOT NULL,
	[upwd] [nchar](30) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[salary]    Script Date: 05/14/2020 13:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[salary](
	[id] [nchar](10) NULL,
	[allsalary] [nchar](10) NULL,
	[monsalary] [nchar](10) NULL,
	[Insurance] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[manager]    Script Date: 05/14/2020 13:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[manager](
	[id] [nchar](10) NOT NULL,
	[name] [nchar](20) NOT NULL,
	[native] [nchar](30) NOT NULL,
	[adress] [nchar](30) NOT NULL,
	[phone] [nchar](30) NOT NULL,
	[age] [nchar](30) NOT NULL,
	[salary] [nchar](30) NOT NULL,
	[sex] [nchar](30) NOT NULL,
	[date] [nchar](30) NOT NULL,
	[education] [nchar](30) NOT NULL,
	[allsalary] [nchar](30) NOT NULL,
	[monday] [nchar](30) NOT NULL,
	[insurance] [nchar](30) NOT NULL,
	[monsalary] [nchar](30) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 05/14/2020 13:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[aid] [nchar](30) NOT NULL,
	[apwd] [nchar](30) NOT NULL
) ON [PRIMARY]
GO
