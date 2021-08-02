USE [DataMart-STG]
GO

/****** Object:  Table [dbo].[stgRealEstateForSale]    Script Date: 2021-07-23 5:50:09 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[stgRealEstateForSale]') AND type in (N'U'))
DROP TABLE [dbo].[stgRealEstateForSale]
GO

/****** Object:  Table [dbo].[stgRealEstateForSale]    Script Date: 2021-07-23 5:50:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[stgRealEstateForSale](
	[Price] [varchar](50) NULL,
	[Address] [varchar](2000) NULL,
	[NeighbourHood] [varchar](500) NULL,
	[Bed] [varchar](50) NULL,
	[Bath] [varchar](50) NULL,
	[Sqft] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[YearsOld] [varchar](100) NULL,
	[ListedBy] [varchar](2000) NULL,
	[Note] [varchar](50) NULL,
	[LoadDate] [date] NULL,
	[Page] [int] NULL,
	[Source] [varchar](50) NULL
) ON [PRIMARY] 
GO


