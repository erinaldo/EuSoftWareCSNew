USE [CSUSA]
GO

/****** Object:  View [dbo].[MCEStock]    Script Date: 05/11/2017 14:41:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER view [dbo].[MCEStock]
as
select a.[ID],[CSNo] [StockCatalogNo],[StockCSNo],[StockSize],[StockUnit],[StockValCode],[StockBatchNo],[StockNote],[StockLibraryID],[SysNote],[StockLocation],[UpdateTime],[Person]
,b.DrugNames  From mcesqlsystem.[dbo].[MCEStock] a
inner join mcesqlsystem.[dbo].[MCEProductsBasicinfo] b on a.[StockCatalogNo] = b.[CatalogNo]



GO


