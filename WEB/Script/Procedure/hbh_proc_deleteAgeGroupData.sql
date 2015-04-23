


DROP PROCEDURE IF EXISTS hbh_proc_deleteAgeGroupData;

-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: 导入数据更新正式表
-- --------------------------------------------------------------------------------
DELIMITER $$

create procedure hbh_proc_deleteAgeGroupData
(
	in pi_GUID varchar(125)
)
begin


	-- 删除 年龄段所有的数据
	delete from T_Menu
	where AgeGroupName in (select imp2.AgeGroupName
				from T_ImportData imp2
				where 1=1
					and imp2.GUID = pi_GUID
					and (length(imp2.Sequence) - length(replace(imp2.Sequence,'.',''))) < 3
				)
	;

	-- 删除年龄段的数据
	delete from T_Question
	where AgeGroupName in (select imp2.AgeGroupName
				from T_ImportData imp2
				where T_ImportData.GUID = pi_GUID
					and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) = 3
				)
	;

	-- 删除年龄段的数据
	delete from T_Solution
	where AgeGroupName in (select imp2.AgeGroupName
				from T_ImportData imp2
				where T_ImportData.GUID = pi_GUID
					and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) > 3
				)
	;


end ;




