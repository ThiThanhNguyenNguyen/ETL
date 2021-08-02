USE [DataMart]
GO

/****** Object:  Table [dbo].[factRealEstateForSale]    Script Date: 2021-08-02 10:58:51 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[factRealEstateForSale]') AND type in (N'U'))
DROP TABLE [dbo].[factRealEstateForSale]
GO

/****** Object:  Table [dbo].[factRealEstateForSale]    Script Date: 2021-08-02 10:58:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[factRealEstateForSale](
	[DateKey] [int] NOT NULL,
	[RealEstateForSaleKey] [int] NOT NULL,
	[RealEstateForSaleId] [varchar](200) NOT NULL,
	[Price] [numeric](18, 2) NULL,
	CONSTRAINT PK_FactRealEstate PRIMARY KEY (DateKey, RealEstateForSaleKey),
	FOREIGN KEY (RealEstateForSaleKey) REFERENCES rltRealEstateForSale(RealEstateForSaleKey)
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_FactRealEstate   
    ON [dbo].[factRealEstateForSale] (DateKey, RealEstateForSaleId);   
GO  


