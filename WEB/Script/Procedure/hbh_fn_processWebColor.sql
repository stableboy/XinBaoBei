

DROP function IF EXISTS hbh_fn_processWebColor;

-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: 字符串分割
-- --------------------------------------------------------------------------------
DELIMITER $$

create function hbh_fn_processWebColor
(
	orignalString varchar(1000)
	,keywords varchar(125)
	,split varchar(2)
	,prefix varchar(125)
	,suffix varchar(125)
)
returns 
 	varchar(125)
begin
	-- set @result = orignalString;

	while(instr(keywords,split)<>0) 
	do
		set @curKeyword = substring(keywords,1,instr(keywords,split) - 1) ;
		set orignalString = replace(orignalString,@curKeyword
								,concat(prefix,@curKeyword,suffix));
		set keywords = substring(keywords,instr(keywords,split) + 1
									,length(keywords)-instr(keywords,split)) ;
		-- select @curKeyword,keywords,orignalString;
	end while;
	set @curKeyword = keywords;
	set orignalString = replace(orignalString,@curKeyword
							,concat(prefix,@curKeyword,suffix));
	 -- select @curKeyword,keywords,orignalString;
	 return orignalString ;
End 
;


