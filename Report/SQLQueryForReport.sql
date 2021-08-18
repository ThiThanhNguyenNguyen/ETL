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
	AND SUBSTRING(CAST(DateKey AS VARCHAR(100)),5,2) = @month
	AND SUBSTRING(CAST(DateKey AS VARCHAR(100)),1,4) = @year
GROUP BY a.[DateKey], b.[NeighbourHood]	
ORDER BY a.[DateKey], b.[NeighbourHood]


---------------------

SELECT SUBSTRING(CAST(DateKey AS VARCHAR(100)),5,2) AS 'Month'
FROM [DataMart].[dbo].[factRealEstateForSale]