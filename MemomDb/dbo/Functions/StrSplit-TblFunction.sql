CREATE FUNCTION dbo.Split(@origString varchar(max), @Delimiter char(1))     
returns @temptable TABLE (items varchar(max))     
as     
begin     
    declare @idx int     
    declare @split varchar(max)     

    select @idx = 1     
        if len(@origString )<1 or @origString is null  return     

    while @idx!= 0     
    begin     
        set @idx = charindex(@Delimiter,@origString)     
        if @idx!=0     
            set @split= left(@origString,@idx - 1)     
        else     
            set @split= @origString

        if(len(@split)>0)
            insert into @temptable(Items) values(@split)     

        set @origString= right(@origString,len(@origString) - @idx)     
        if len(@origString) = 0 break     
    end 
return     
end