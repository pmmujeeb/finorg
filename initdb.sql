UPDATE    Veh_Master
SET              Ins_Cmp_Name = '""', Ins_Start_date = NULL, Ins_end_date = NULL, Rent_Start_From = NULL, Rent_End_Date = NULL, Customer_code = 0, Rent_Amount = 0, 
                      Thum_Cus_code = 0, Thum_No = 0, Rent_total_Amount = 0, Rent_received_Amount = 0, Veh_Expect_Return = NULL, Rent_Adv_Amount = 0, Total_Income = 0, 
                      Total_Exp = 0, Veh_Issue_No = 0, Total_KMExcess_amt = 0, Tot_damage_amount = 0, Veh_Issue_counter = 0;
                     
                      go
                      truncate table dbo.Veh_ODO
                      go
                      truncate table dbo.Veh_Received_det
                      go
                      truncate table dbo.Veh_Received_Monthly
                  
                     go
                      truncate table dbo.Veh_Service_det
                      go
                      truncate table dbo.Veh_Thum
                      go
                      truncate table dbo.Veh_Tran
                      go
 
 truncate table dbo.Veh_issue_return
                      go                       
delete from  accounts where acc_type_code=1
go

