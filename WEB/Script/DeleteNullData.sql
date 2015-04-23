
/*
create table hbh_tmp_T_Menu_15011901
select *
from T_Menu
;
create table hbh_tmp_T_AgeGroup_15011901
select *
from T_AgeGroup
;

insert into T_Menu
select *
from hbh_tmp_T_Menu_15011901
*/

-- 删除空的菜单
delete from T_Menu 
where 1=0
	-- 没有序号
	or (Sequence is null or Sequence = '')
	-- 没有年龄段
	or (AgeGroupName is null or AgeGroupName = '')
	-- 有.但是没有父节点
	or (Sequence like '%.%' and ParentMenu not in (select parent.ID  from hbh_tmp_T_Menu_15011901 parent)  ) 
;


-- 删除空的年龄段
delete from T_AgeGroup
where Code not in (select distinct AgeGroupName from T_Menu menu)
;





