INSERT INTO [Finance].[dbo].[HD_ITEMMASTER]
           ([ITEM_CODE]
           ,[DESCRIPTION]
           ,[USER]
           ,[ITM_CAT_CODE]
           ,[UNIT]
           ,[FRACTION]
           ,[FLAG]
           ,[PART_NO]
           ,[BRAND]
           ,[ALIAS_NAME]
           ,[BRN_CODE]
           ,[SUB_CAT_CODE]
           ,[AR_DESC]
           ,[UPD_FLAG]
           ,[BARCODE])
    SELECT     upc_code,  prd_desc,'Admin',dep_id,unit_m_name ,1,'A',prd_id,'','',1,0,prd_desc,'N',upc_code
FROM   [targetpos].[dbo].[prod] where prd_id <> ''
go
INSERT INTO [Finance].[dbo].[BARCODE]
           ([BARCODE]
           ,[UNIT]
           ,[FRACTION]
           ,[SALE_PRICE]
           ,[RETAIL_PRICE]
           ,[ITEM_CODE]
           ,[BRN_CODE]
           ,[ITEM_ID]
           ,[MAIN_ID]
           ,[DESCRIPTION]
           ,[DESCRIPTION_AR]
           ,[ITM_CAT_CODE])
    SELECT     upc_code,unit_m_name ,1,[sale_p],[sale_p],upc_code,1,[prd_id] ,1, prd_desc,prd_desc,dep_id
FROM   [targetpos].[dbo].[prod] where prd_id <> ''

GO
INSERT INTO [Finance].[dbo].[STOCK_MASTER]
           ([ITEM_CODE]
           ,[STOCK]
           ,[LAST_PUR_PRICE]
           ,[SALE_PRICE]
           ,[USER1]
           ,[LOCATION]
           ,[RE_ORDER]
           ,[PROFIT]
           
           ,[RETAIL_PRICE]
           ,[BRN_CODE]
           ,[SALE_PRICE_EX]
           ,[OP_STOCK]
           ,[W_MIN_PC]
           ,[UPD_FLAG]
           ,[AVG_PUR_PRICE])
    SELECT p.upc_code,s.Stock,p.[cost_p],p.[sale_p],'aDMIN',nULL,10,20,p.[sale_p],1,p.[sale_p],0,0,'U',p.[cost_p]
     
  FROM [targetpos].[dbo].[prod] as p left join [targetpos].[dbo].[prodstock] as s
  on p.prd_id =s.prd_id
    WHERE p.prd_id<> ''

go

INSERT INTO [Finance].[dbo].[WR_STOCK_MASTER]
           ([ITEM_CODE]
           ,[STOCK]
           ,[USER]
           ,[LOCATION]
           ,[BRN_CODE]
           ,[OP_STOCK]
           ,[WR_CODE]
           ,[UPD_FLAG])
    SELECT P.upc_code,S.Stock,'aDMIN',nULL,1,S.Stock,1,'U'
 
     
  FROM [targetpos].[dbo].[prod] as p left join [targetpos].[dbo].[prodstock] as s
  on p.prd_id =s.prd_id
    WHERE p.prd_id<> ''
GO

INSERT INTO [dbo].[DATA_ENTRY]
           ([INVOICE_NO]
           ,[TRAN_NO]
           ,[ACCODE]
           ,[ENAME]
           ,[CURRENCY]
           ,[CRATE]
           ,[ENTRY_TYPE]
           ,[CURDATE]
           ,[G_TOTAL]
          
           ,[NET_AMOUNT]
           ,[FRN_AMOUNT]
           ,[TRN_TYPE]
           ,[ORG_DUP]
           ,[FLAG]
           ,[SALES_CODE]
           ,[BRN_CODE]
           ,[REF_NO]
           ,[WR_CODE]
           ,[to_date]
           ,[USER_NAME]
           ,[NYEAR]
           
           ,[ENTRY_STATUS]
           ,[CASH_PAID]
           ,[OTHER_PAID]
           ,[REMARKS]
          )
SELECT 
ROW_NUMBER() OVER(ORDER BY W.ITEM_CODE ASC) AS Row#,
--RANK () OVER (  order by W.ITEM_CODE )  ,
--(SELECT MAX(INVOICE_NO)+ ROW_NUMBER() OVER(ORDER BY W.ITEM_CODE ASC)   FROM DATA_ENTRY WHERE TRN_TYPE=0) AS INVNO,
--(SELECT MAX(INVOICE_NO)+ ROW_NUMBER() OVER(ORDER BY W.ITEM_CODE ASC) FROM DATA_ENTRY WHERE TRN_TYPE=0) AS TRN_NO,
ROW_NUMBER() OVER(ORDER BY W.ITEM_CODE ASC) AS TRAN_NO,
5016,
'OPENING BALANCE' AS ENAME,
'SR' AS SR,
1 AS RATE,
'OPSTOCK' AS ETYPE,GETDATE() AS DT,
W.STOCK * S.AVG_PUR_PRICE AS GTOT,
W.STOCK * S.AVG_PUR_PRICE AS NET,
W.STOCK * S.AVG_PUR_PRICE AS FNET,
0 AS TRNTYPE,
'O' AS ORG,
'A' AS FLG,
0 AS SLCODE,
1 AS BRN,
0 AS REFNO,
W.WR_CODE AS WH,
GETDATE() AS TDATE,
'1' AS USR,
YEAR(GETDATE()) AS YR,
'O' AS STS,
0 AS CSH,
0 AS OTH,
'OPENING BALANCE' AS REMRK
FROM  WR_STOCK_MASTER AS W LEFT JOIN STOCK_MASTER AS S ON W.ITEM_CODE=S.ITEM_CODE

GO


INSERT INTO [dbo].[DATA_ENTRY_GRID]
           ([REC_NO]
           ,[ROWNUM]
           ,[ITEM_CODE]
           ,[DESCRIPTION]
           ,[QTY]
           ,[PRICE]
           ,[DISC]
           ,[SALE_PUR_AMT]
           ,[ITM_TOTAL]
           ,[BARCODE]
           ,[FRACTION]
           ,[UNIT]
           ,[REMARKS]
           ,[BRN_CODE]
           ,[WR_CODE]
           ,[PROPOSE_PRiCE]
           ,[ENTRY_STATUS]
           ,[HFRACTION]
           ,[ITEM_ID]
           ,[UNIT_QTY]
           ,[UNIT_PRICE]
           ,[COST_CENTER]
           ,[FPRICE]
           ,[UNIT_TRN_AMOUNT]
           ,[TRN_TYPE]
           ,[INVOICE_NO]
           ,[FRN_PRICE]
           ,[EXPENSE_AMT]
          
           )

           

		   SELECT 
ROW_NUMBER() OVER(ORDER BY W.ITEM_CODE ASC) AS Row#,
1,
W.ITEM_CODE,W.ITEM_CODE,
W.STOCK,S.AVG_PUR_PRICE,
0,
0,
W.STOCK * S.AVG_PUR_PRICE,
W.ITEM_CODE,
1,
'PCS',
'OB',
1,
W.WR_CODE,
0,
'P',
1,
W.ITEM_CODE,
W.STOCK,
S.AVG_PUR_PRICE,
0,
S.AVG_PUR_PRICE,
S.AVG_PUR_PRICE,
0,
ROW_NUMBER() OVER(ORDER BY W.ITEM_CODE ASC) AS Row#,
S.AVG_PUR_PRICE,
0


FROM   
WR_STOCK_MASTER AS W  LEFT JOIN STOCK_MASTER AS S ON W.ITEM_CODE=S.ITEM_CODE


