CREATE procedure [dbo].[Usp_BindController]    
@mode nvarchar(100)=''
as    
begin 


if(@mode='Getcontdetails')
begin
select 'OTH_STUFF'   as [Key],'Other Stuff'    as [Value],1 as 'id'    
union select 'I_DIRECTOR'   as [Key],'Director Interface'  as [Value],2 as 'id'     
union select 'DIRECTOR'   as [Key],'Director'    as [Value],3 as 'id'       
union select 'CONTROLER'   as [Key],'Controller'    as [Value],4 as 'id'      
union select 'MODEL'    as [Key],'Angular Model'   as [Value],5 as 'id'    
union select 'ANG_ENDPOINTS'  as [Key],'Angular Endpoints'   as [Value],6 as 'id'    
union select 'ANG_SERVICE'  as [Key],'Angular Sevice'   as [Value],7 as 'id'     
union select 'ANG_COMPONANT'  as [Key],'Angular Component'  as [Value],8 as 'id'     
union select 'HTML'    as [Key],'HTML'     as [Value],9 as 'id'     
order by id 
end

if(@mode='checkcontrols')
begin

select 'MODEL'    as [Key]
union select 'ANG_SERVICE' as [Key]
union select 'ANG_COMPONANT'  as [Key]

end

end