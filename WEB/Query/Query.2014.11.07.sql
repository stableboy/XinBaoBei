/*
delete from t_menu;
*/
/*
concat(Content,'(',AgeGroupName,')')
	,(length(Sequence) - length(replace(Sequence,'.',''))) ,length(Sequence),length(replace(Sequence,'.',''))
*/
-- [b7e6e392-92e2-4ad9-93a1-11ee0d84af63]
-- 年龄段
insert into T_AgeGroup
(
	Code,Name,Memo
)
select 
	distinct AgeGroupName,AgeGroupName,null
from T_ImportData
where GUID = '{0}'
	and AgeGroupName not in (select tb.Code from T_AgeGroup tb)
order by AgeGroupName
;
-- 目录
insert into t_menu
(
	Sequence,Code,Name,ParentMenu,AgeGroupName
)
select 
	Sequence,Content,concat(Sequence,' ',Content),null,AgeGroupName
from T_ImportData
where GUID = '{0}'
	and (length(Sequence) - length(replace(Sequence,'.',''))) < 3
	and Sequence not in (select tb.Sequence from t_menu tb)
order by Sequence
;
-- 更新父目录ID
drop table if exists tmp_menu 
;
create table tmp_menu as select ID,Sequence from t_menu
;
update t_menu
set ParentMenu = (select min(parent.ID) from tmp_menu parent 
		where parent.Sequence = substring_index(t_menu.Sequence,'.'
					,(length(t_menu.Sequence) - length(replace(t_menu.Sequence,'.','')))
											)
		)
where ParentMenu is null
;
drop table if exists tmp_menu 
;
-- 问题
insert into t_question
(
	Sequence,Title,Description,KeyWords,AgeGroupName,ParentMenu
)
select 
	Sequence,Content,Content,null,AgeGroupName,(select min(parent.ID) from t_menu parent 
			where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
						,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
												)
			)
from T_ImportData
where GUID = '{0}'
	and (length(Sequence) - length(replace(Sequence,'.',''))) = 3
	and Sequence not in (select tb.Sequence from t_question tb)
order by Sequence
;
-- 答案
insert into t_solution
(
	Sequence,SText,Intro,Question
)
select 
	Sequence,Content,null,(select min(parent.ID) from t_question parent 
			where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
						,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
												)
			)
from T_ImportData
where GUID = '{0}'
	and (length(Sequence) - length(replace(Sequence,'.',''))) > 3
	and Sequence not in (select tb.Sequence from t_solution tb)
order by Sequence
