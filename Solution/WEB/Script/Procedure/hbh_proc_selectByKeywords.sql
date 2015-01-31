-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: comments before and after the routine body will not be stored by the server
-- --------------------------------------------------------------------------------
DELIMITER $$

CREATE DEFINER=`baby`@`%` PROCEDURE `hbh_proc_selectByKeywords`(
	in pi_AgeGroupName varchar(125)
	,in pi_Keywords varchar(125)
	,in pi_PageSize int
	,in pi_PageIndex int
)
begin

	set @RegText = replace(pi_Keywords,' ','|') ;
	set @StartIndex = GREATEST(0, ( (pi_PageIndex - 1) * pi_PageSize)) ;
	set @PageSize = pi_PageSize;
	set @AgeGroupName = pi_AgeGroupName;

	/* http://rains8231.blog.163.com/blog/static/208656009201310253115756/
		开始以为是.*，其实.*连在一起就意味着任意数量的不包含换行的字符。 
		所以可以使用 [\s\S]* 它的意思是任意空白字符和非空白字符。
		同理，也可以用 "[\d\D]*"、"[\w\W]*" 来表示。 d和D，w和W，s和S都是反义，加起来就是全部字符了
	*/
	-- 空查询时，匹配所有的正则表达式
	if(@RegText = '') then
		set @RegText = '[\s\S]*' ;
	end if;
	
	set @SQUERY = "
	select a.ID QuestionID,a.Code,a.AgeGroupName
		-- ,replace(a.Title,pi_Keywords,concat('<font color=''#FF0000''>',pi_Keywords,'</font>')) as Title
		-- ,hbh_fn_processWebColor(a.Title,pi_Keywords,' ','<font color=''#FF0000''>','</font>') as Title
		,a.Title
		-- ,replace(ifnull(a.KeyWords,''),pi_Keywords,concat('<font color=''#FF0000''>',pi_Keywords,'</font>')) as KeyWords
		,a.KeyWords
		,a.Questioner,a.Memo,b.ID AnswerID,b.AnCode,b.QuCode
		,case when ifnull(Intro,'') = '' then concat(left(replace(b.SText,'\r\n','<br><br>'),100),'...') 
			else replace(Intro,'\r\n','<br><br>') end as Intro
		,b.SText
		,b.Answer,b.Memo
		,replace(replace(replace(b.SText,'\r\n','<br><br>'),char(10),'<br>'),char(13),'<br>') STextDiv 
	from T_Question a inner join T_Solution b on a.id=b.Question
	where  1=1  and a.AgeGroupName = ?
			and (1=0  
				or a.KeyWords REGEXP  ?
				or a.Title REGEXP  ?
				-- or b.SText REGEXP  ?
				)
	order by QuestionID,AnswerID
	limit ? , ?
	;" ;

	PREPARE STMT FROM @SQUERY;
    -- EXECUTE STMT USING @AgeGroupName,@RegText,@RegText,@RegText,@StartIndex,@PageSize;
    EXECUTE STMT USING @AgeGroupName,@RegText,@RegText,@StartIndex,@PageSize;

	
	select 
		count(a.ID) as TotalCount
	from T_Question a inner join T_Solution b on a.id=b.Question
	where  1=1  and a.AgeGroupName = @AgeGroupName
			and (1=0  
				or a.KeyWords REGEXP  @RegText
				or a.Title REGEXP  @RegText
				-- or b.SText REGEXP  @RegText
				)

	;

end