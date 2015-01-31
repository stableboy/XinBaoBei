
DROP PROCEDURE IF EXISTS hbh_proc_selectByKeywords;

-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: 导入数据更新正式表
-- --------------------------------------------------------------------------------
DELIMITER $$

create procedure hbh_proc_selectByKeywords
(
	in pi_AgeGroupName varchar(125)
	,in pi_Keywords varchar(125)
)
begin

	set @RegText = replace(pi_Keywords,' ','|') ;
	
		
	select a.ID QuestionID,a.Code,a.AgeGroupName
		-- ,replace(a.Title,pi_Keywords,concat('<font color=''#FF0000''>',pi_Keywords,'</font>')) as Title
		,hbh_fn_processWebColor(a.Title,pi_Keywords,' ','<font color=''#FF0000''>','</font>') as Title
		-- ,replace(ifnull(a.KeyWords,''),pi_Keywords,concat('<font color=''#FF0000''>',pi_Keywords,'</font>')) as KeyWords
		,a.KeyWords
		,a.Questioner,a.Memo,b.ID AnswerID,b.AnCode,b.QuCode
		,case when ifnull(Intro,'') = '' then concat(left(replace(b.SText,'\r\n','<br><br>'),100),'...') 
			else replace(Intro,'\r\n','<br><br>') end as Intro
		,b.SText,b.Answer,b.Memo
		,replace(replace(replace(b.SText,'\r\n','<br><br>'),char(10),'<br>'),char(13),'<br>') STextDiv 
	from T_Question a inner join T_Solution b on a.id=b.Question
	where  1=1  and a.AgeGroupName = pi_AgeGroupName
			and (1=0  
				or a.KeyWords REGEXP  @RegText
				or a.Title REGEXP  @RegText
				)
	order by QuestionID,AnswerID

	;



end



