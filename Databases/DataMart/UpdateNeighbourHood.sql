/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [NeighbourHoodId]
      ,[NeighbourHoodName]
      ,[City]
      ,[Region]
      ,[Province]
  FROM [DataMart].[dbo].[neighbourHood]
  --where neighbourHoodID = -1
  order by neighbourHoodID

  SET IDENTITY_INSERT [dbo].[neighbourHood]  ON    

  Insert into [dbo].[neighbourHood]([NeighbourHoodId]
      ,[NeighbourHoodName]
      ,[City]
      ,[Region]
      ,[Province])
  values(-1,'Unknown','Unknown','Unknown','Unknown')

   SET IDENTITY_INSERT [dbo].[neighbourHood]  OFF