/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [NeighbourHoodId]
      ,[NeighbourHoodName]
      ,[City]
      ,[Region]
      ,[Province]
  FROM [DataMart].[dbo].[neighbourHood]
  --where neighbourHoodID = -1
  order by neighbourHoodID

  Insert into [dbo].[neighbourHood]
  values(-1,'Unknown','Unknown','Unknown','Unknown')