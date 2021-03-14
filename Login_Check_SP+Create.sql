USE [LoginDB]
GO

/****** Object:  Table [dbo].[Login_Check_SP]    Script Date: 3/15/2021 2:47:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Login_Check_SP]') AND type in (N'U'))
DROP TABLE [dbo].[Login_Check_SP]
GO

/****** Object:  Table [dbo].[Login_Check_SP]    Script Date: 3/15/2021 2:47:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Login_Check_SP](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nchar](20) NULL,
	[Password] [nchar](20) NULL,
	[MobileNo] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


