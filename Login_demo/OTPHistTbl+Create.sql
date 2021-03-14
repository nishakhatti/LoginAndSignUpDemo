USE [LoginDB]
GO

/****** Object:  Table [dbo].[OTPhistorytbl]    Script Date: 3/15/2021 2:51:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OTPhistorytbl]') AND type in (N'U'))
DROP TABLE [dbo].[OTPhistorytbl]
GO

/****** Object:  Table [dbo].[OTPhistorytbl]    Script Date: 3/15/2021 2:51:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OTPhistorytbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Customer_No] [nvarchar](50) NULL,
	[User_Id] [nvarchar](50) NULL,
	[OTP] [nvarchar](50) NULL,
	[ctime_Stamp] [nvarchar](50) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


