/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [NeighbourHoodId]
      ,[NeighbourHoodName]
      ,[City]
      ,[Region]
      ,[Province]
  FROM [DataMart].[dbo].[neighbourHood]

SELECT * FROM [dbo].[factRealEstateForSale]

ALTER TABLE [dbo].[factRealEstateForSale]
ADD NeighbourHoodId int;

UPDATE s
SET s.NeighbourHoodId = t.neighbourHoodId
FROM [DataMart].[dbo].[factRealEstateForSale] s
JOIN 
(SELECT a.RealEstateForSaleKey, a.NeighbourHood,  b.neighbourHoodName, ISNULL(b.neighbourHoodId,-1) neighbourHoodId
FROM [DataMart].[dbo].[rltRealEstateForSale] a
LEFT JOIN [DataMart].[dbo].[neighbourHood] b
ON a.NeighbourHood = b.neighbourHoodName
--WHERE a.NeighbourHood <> '' and b.neighbourHoodID is NULL
) t
ON s.RealEstateForSaleKey = t.RealEstateForSaleKey

select count(*) from 
[dbo].[factRealEstateForSale]
--UPDATE a
--SET NeighbourHoodId = c.neighbourHoodId
SELECT a.RealEstateForSaleKey,a.[NeighbourHoodId], b.NeighbourHood, c.[NeighbourHoodId]
FROM [dbo].[factRealEstateForSale] a
INNER JOIN [dbo].[rltRealEstateForSale] b ON a.RealEstateForSaleKey = b.RealEstateForSaleKey
LEFT JOIN [dbo].[neighbourHood] c ON b.NeighbourHood = c.neighbourHoodName
Where ISNULL(a.[NeighbourHoodId],0) <> c.neighbourHoodID