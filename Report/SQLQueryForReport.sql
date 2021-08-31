DECLARE @month AS VARCHAR(2)
SET @month = '07'
PRINT @month
DECLARE @year AS VARCHAR(4)
SET @year = '2021'
PRINT @year

SELECT  
	a.[DateKey],
	b.[NeighbourHood],
	FORMAT(ROUND(AVG(a.[Price]),2),'###############.##') AS AverageListingPrice
FROM [DataMart].[dbo].[factRealEstateForSale]  a with(nolock)
INNER JOIN [DataMart].[dbo].[rltRealEstateForSale] b with(nolock)
ON a.[RealEstateForSaleKey] = b.[RealEstateForSaleKey] 
WHERE
	1=1
	AND a.[PRICE] IS NOT NULL 
	AND b.[NeighbourHood] <> '' 
	AND SUBSTRING(CAST(DateKey AS VARCHAR(100)),5,2) = '08'
	AND SUBSTRING(CAST(DateKey AS VARCHAR(100)),1,4) = '2021'
GROUP BY a.[DateKey], b.[NeighbourHood]	
ORDER BY a.[DateKey], b.[NeighbourHood]


SELECT  
	[DateKey],
	SUBSTRING(CAST([DateKey] AS VARCHAR(100)),7,2) AS SingleDay,
	FORMAT(ROUND(AVG([Price]),2),'###############.##') AS AvgListingPrice
FROM [DataMart].[dbo].[factRealEstateForSale]  with(nolock)
WHERE
	1=1
	AND [PRICE] IS NOT NULL 
	AND SUBSTRING(CAST([DateKey] AS VARCHAR(100)),5,2) = '08'
	AND SUBSTRING(CAST([DateKey] AS VARCHAR(100)),1,4) = '2021'
GROUP BY [DateKey], SUBSTRING(CAST([DateKey] AS VARCHAR(100)),5,2)
ORDER BY [DateKey]


SELECT  
	[DateKey],
	FORMAT(ROUND(AVG([Price]),2),'###############.##')
FROM [DataMart].[dbo].[factRealEstateForSale]  with(nolock)
WHERE
	1=1
	AND [PRICE] IS NOT NULL 
	AND SUBSTRING(CAST([DateKey] AS VARCHAR(100)),5,2) = '08'
	AND SUBSTRING(CAST([DateKey] AS VARCHAR(100)),1,4) = '2021'
GROUP BY [DateKey]
ORDER BY [DateKey]

SELECT  [DateKey], count(DateKey) AS TotalItem
FROM [DataMart].[dbo].[factRealEstateForSale] with(nolock)
GROUP BY [DateKey]
--WHERE DateKey = '20210723'

SELECT distinct SUBSTRING(CAST(DateKey AS VARCHAR(100)),5,2) as Months
FROM [DataMart].[dbo].[factRealEstateForSale] with(nolock)
ORDER BY months

SELECT distinct SUBSTRING(CAST(DateKey AS VARCHAR(100)),1,4) as Years
FROM [DataMart].[dbo].[factRealEstateForSale] with(nolock)
ORDER BY years

SELECT CAST(YEAR(GETDATE()) AS VARCHAR(10)) AS CurrentYear

SELECT RIGHT('00' + CAST(MONTH(GETDATE()) AS VARCHAR(10)),2) AS CurrentMonth


---------------------

SELECT SUBSTRING(CAST(DateKey AS VARCHAR(100)),5,2) AS 'Month'
FROM [DataMart].[dbo].[factRealEstateForSale]


------------------------------------

SELECT  TOP 10
	b.[NeighbourHood],
	ROUND(AVG(a.[Price]),2) AS AverageListingPrice
FROM [DataMart].[dbo].[factRealEstateForSale]  a with(nolock)
INNER JOIN [DataMart].[dbo].[rltRealEstateForSale] b with(nolock)
ON a.[RealEstateForSaleKey] = b.[RealEstateForSaleKey] 
WHERE
	1=1
	AND a.[PRICE] IS NOT NULL 
	AND b.[NeighbourHood] <> ''
	AND SUBSTRING(CAST(DateKey AS VARCHAR(100)),7,2) = '20'
	AND SUBSTRING(CAST(DateKey AS VARCHAR(100)),5,2) = '08'
	AND SUBSTRING(CAST(DateKey AS VARCHAR(100)),1,4) = '2021'
GROUP BY b.[NeighbourHood]	
ORDER BY AverageListingPrice DESC

SELECT RIGHT('00' + CAST(DAY(GETDATE()) AS VARCHAR(10)),2) AS CurrentDay