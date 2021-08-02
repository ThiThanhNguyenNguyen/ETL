USE [DataMart]
GO

/****** Object:  Table [dbo].[RealEstateForSale]    Script Date: 2021-07-27 11:50:09 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rltRealEstateForSale]') AND type in (N'U'))
DROP TABLE [dbo].[rltRealEstateForSale]
GO

/****** Object:  Table [dbo].[rltRealEstateForSale]    Script Date: 2021-07-27 11:50:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[rltRealEstateForSale](
	[RealEstateForSaleKey] INT IDENTITY(1000,1) PRIMARY KEY,
	[RealEstateForSaleId] [varchar](200) NULL,
	[Price] [numeric](18,2) NULL,
	[Address] [varchar](2000) NULL,
	[NeighbourHood] [varchar](500) NULL,
	[Bed] [varchar](50) NULL,
	[Bath] [varchar](50) NULL,
	[Sqft] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[YearsOld] [varchar](100) NULL,
	[ListedBy] [varchar](2000) NULL,
	[Note] [varchar](50) NULL,
	[StartDate] [date] NULL,	
	[EndDate] [date] NULL,
	[Page] [int] NULL,	
	[Source] [varchar](50) NULL
) ON [PRIMARY] 
GO


