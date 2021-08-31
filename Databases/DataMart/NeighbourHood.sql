USE [DataMart]
GO

/****** Object:  Table [dbo].[neighbourHood]    Script Date: 2021-08-02 10:58:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[neighbourHood]') AND type in (N'U'))
DROP TABLE [dbo].[neighbourHood]
GO

/****** Object:  Table [dbo].[neighbourHood]    Script Date: 2021-08-02 10:58:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[neighbourHood](
	[NeighbourHoodId] INT IDENTITY(1,1) PRIMARY KEY,
	[NeighbourHoodName] [varchar](200) NOT NULL,
	[City] [varchar](200) NOT NULL,
	[Region] [varchar](200) NOT NULL,
	[Province] [varchar](200) NOT NULL
) ON [PRIMARY]
GO
   