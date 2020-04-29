USE [manager]
GO
/****** Object:  Table [dbo].[user]    Script Date: 04/29/2020 08:52:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[uid] [nchar](10) NULL,
	[upwd] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[manager]    Script Date: 04/29/2020 08:52:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[manager](
	[id] [nchar](10) NULL,
	[name] [nchar](10) NULL,
	[native] [nchar](10) NULL,
	[adress] [nchar](10) NULL,
	[phone] [nchar](10) NULL,
	[age] [nchar](10) NULL,
	[salary] [nchar](10) NULL,
	[sex] [nchar](10) NULL,
	[date] [nchar](10) NULL,
	[education] [nchar](10) NULL,
	[allsalary] [nchar](10) NULL,
	[monday] [nchar](10) NULL,
	[insurance] [nchar](10) NULL,
	[monsalary] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 04/29/2020 08:52:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[aid] [nchar](10) NULL,
	[apwd] [nchar](10) NULL
) ON [PRIMARY]
GO
