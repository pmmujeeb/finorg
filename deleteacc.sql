delete from  TRN_ACCOUNTS  where doc_no in (select doc_no from TRN_ACCOUNTS where  ACC_NO in (28,94,96,2001,2002,2003))
go
delete from TRN_ITM_DETAIL where trn_NO in (select trn_NO from TRN_MASTER where CUS_CODE in (28,94,96,2001,2002,2003))
go
delete from TRN_MASTER where CUS_CODE in (28,94,96,2001,2002,2003)
go
delete from ACCOUNTS where ACC_NO in (28,94,96,2001,2002,2003)
go
delete from DATA_ENTRY_GRID where REC_NO in (select REC_NO from DATA_ENTRY where acCODE in (28,94,96,2001,2002,2003))
go
delete from  DATA_ENTRY where acCODE in (28,94,96,2001,2002,2003)
go


