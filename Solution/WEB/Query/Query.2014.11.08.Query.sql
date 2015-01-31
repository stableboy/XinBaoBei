
select *
from t_menu
;

select *
from t_question
;

select *
from t_solution
;

select *
from T_ImportData
;

/*
delete from t_menu ;
delete from t_question ;
delete from t_solution ;
-- delete from T_ImportData ;
*/


SELECT 0 as TreeType,0 as ParentTreeType,ID,Code,Name,ParentMenu,Sequence,substring_index(Sequence,'.',1) seq FROM T_Menu
union 
select 1 as TreeType,0 as ParentTreeType,ID,Code,Title as Name,ParentMenu,Sequence,substring_index(Sequence,'.',1) seq from T_Question 
union 
select 1 as TreeType,0 as ParentTreeType,ID,Code,Title as Name,ParentMenu,Sequence,substring_index(Sequence,'.',1) seq from T_Question 
union 
select 2 as TreeType,1 as ParentTreeType,ID,AnCode as Code
  ,case when ifnull(Intro,'') = '' then concat(left(SText,100),'...') else Intro end as Name
  ,Question as ParentMenu,Sequence,substring_index(Sequence,'.',1) seq from T_Solution
order by length(substring_index(Sequence,'.',1)),substring_index(Sequence,'.',1),Sequence,ParentMenu,Name

