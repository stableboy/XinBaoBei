


DROP PROCEDURE IF EXISTS hbh_proc_updateImportData;

-- --------------------------------------------------------------------------------
-- Routine DDL
-- Note: 导入数据更新正式表
-- --------------------------------------------------------------------------------
DELIMITER $$

	-- call hbh_proc_updateImportData('d45f4c8a-cb09-46f4-b692-20a89258dd7f');

create procedure hbh_proc_updateImportData
(
	in pi_GUID varchar(125)
)
begin


	-- set pi_GUID = 'd45f4c8a-cb09-46f4-b692-20a89258dd7f'   ;

	-- 年龄段-新增
	insert into T_AgeGroup
	(
		Code,Name,Memo
	)
	select 
		distinct AgeGroupName,AgeGroupName,null
	from T_ImportData
	where GUID = pi_GUID
		and AgeGroupName not in (select tb.Code from T_AgeGroup tb)
	order by AgeGroupName
	;

	/*
	-- 目录-删除重复的
	delete from T_Menu
	where Sequence in (select imp2.Sequence
				from T_ImportData imp2
				where 1=1
					and imp2.GUID = pi_GUID
					and (length(imp2.Sequence) - length(replace(imp2.Sequence,'.',''))) < 3
				)
		and Name in (select menu2.Name
				from (select * from T_Menu) menu2
				group by menu2.Name
				having count(menu2.ID) > 1
				)
	;
	*/
	-- 删除 年龄段所有的数据
	delete from T_Menu
	where AgeGroupName in (select distinct imp2.AgeGroupName
				from T_ImportData imp2
				where 1=1
					and imp2.GUID = pi_GUID
					and (length(imp2.Sequence) - length(replace(imp2.Sequence,'.',''))) < 3
				)
		and Sequence not in (select distinct imp2.Sequence 
				from T_ImportData imp2
				where 1=1
					and imp2.GUID = pi_GUID
					and (length(imp2.Sequence) - length(replace(imp2.Sequence,'.',''))) < 3
					and T_Menu.AgeGroupName = imp2.AgeGroupName
					and imp2.Sequence is not null and imp2.Sequence != ''
				)
	;
	
	-- 创建需更新Menu目录的临时表
	drop table if exists tmp_NeedUpdate_Menu  ;
	create table tmp_NeedUpdate_Menu (
	select T_Menu.ID,imp2.Content as Code,concat(imp2.Sequence,' ',imp2.Content) as Name,imp2.AgeGroupName as AgeGroupName
	from T_Menu,T_ImportData imp2
	where 1=1
		and T_Menu.Sequence = imp2.Sequence
		and imp2.GUID = pi_GUID
		and (length(imp2.Sequence) - length(replace(imp2.Sequence,'.',''))) < 3
		and (T_Menu.Code != imp2.Content 
			or T_Menu.Name != concat(imp2.Sequence,' ',imp2.Content)
			or T_Menu.AgeGroupName != imp2.AgeGroupName
			)
		and imp2.Sequence is not null and imp2.Sequence != ''
		)
	;

	-- 目录-更新
	update T_Menu,tmp_NeedUpdate_Menu imp2
	set
		T_Menu.Code = imp2.Code
		,T_Menu.Name = imp2.Name
		,T_Menu.AgeGroupName = imp2.AgeGroupName
	where T_Menu.ID = imp2.ID
	;

	-- 目录-新增
	insert into T_Menu
	(
		Sequence,Code,Name,ParentMenu,AgeGroupName
	)
	select 
		trim(Sequence),Content,concat(trim(Sequence),' ',Content),null,AgeGroupName
	from T_ImportData
	where GUID = pi_GUID
		and (length(Sequence) - length(replace(Sequence,'.',''))) < 3
		and Sequence not in (select tb.Sequence from T_Menu tb)
	order by Sequence
	;
	-- 更新父目录ID
	drop table if exists tmp_menu 
	;
	create table tmp_menu 
	as select sub.ID as ID,sub.Sequence,min(parent.ID) as ParentID from T_Menu sub,T_Menu parent
		where parent.Sequence = substring_index(sub.Sequence,'.'
						,(length(sub.Sequence) - length(replace(sub.Sequence,'.','')))
												)
			and parent.Sequence is not null and parent.Sequence != ''
		group by sub.ID,sub.Sequence
	;
	/*
	update T_Menu
	set ParentMenu = (select min(parent.ID) from tmp_menu parent 
			where parent.Sequence = substring_index(T_Menu.Sequence,'.'
						,(length(T_Menu.Sequence) - length(replace(T_Menu.Sequence,'.','')))
												)
			)
	where ParentMenu is null
	*/
	update T_Menu,tmp_menu
	set T_Menu.ParentMenu = tmp_menu.ParentID
	where T_Menu.ParentMenu is null
		and T_Menu.ID = tmp_menu.ID
	;
	drop table if exists tmp_menu 
	;


	/*
	-- 目录-删除重复的
	delete from T_Question
	where Sequence in (select imp2.Sequence
				from T_ImportData imp2
				where T_ImportData.GUID = pi_GUID
					and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) = 3
				)
		and Title in (select tb2.Content
				from (select * from T_Question) tb2
				group by tb2.Content
				having count(tb2.ID) > 1
				)
	;
	*/
	
	-- 创建需删除问题的临时表
	drop table if exists tmp_NeedDelete_Question  ;
	create table tmp_NeedDelete_Question (ID bigint);
	insert into tmp_NeedDelete_Question
	select T_Question.ID 
	from T_Question
	where T_Question.AgeGroupName in (select distinct T_ImportData.AgeGroupName
							from T_ImportData
							where T_ImportData.GUID = pi_GUID 
								and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) > 3
							)
		and T_Question.Sequence not in (select distinct imp2.Sequence 
				from T_ImportData imp2
				where 1=1
					and imp2.GUID = pi_GUID
					and (length(imp2.Sequence) - length(replace(imp2.Sequence,'.',''))) > 3
				)
	 ;
	

	-- 删除年龄段的数据
	delete from T_Solution
	where Question in (select tmpDel.ID from tmp_NeedDelete_Question tmpDel)
		or Question not in (select T_Question.ID from T_Question)
	;
	-- 删除年龄段的数据
	delete from T_Question
	where ID in (select tmpDel.ID from tmp_NeedDelete_Question tmpDel)
	;
	drop table if exists tmp_NeedDelete_Question  ;


	-- 创建需更新Question目录的临时表
	drop table if exists tmp_NeedUpdate_Question  ;
	create table tmp_NeedUpdate_Question (
	select T_Question.ID,T_ImportData.Sequence,T_ImportData.Content
			,case when ifnull(T_ImportData.KeyWords,'') = 'null' then null else T_ImportData.KeyWords end as KeyWords
			,T_ImportData.AgeGroupName
			,(select min(parent.ID) from T_Menu parent 
				where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
							,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
													)
				) as ParentMenu
	from T_Question,T_ImportData
	where T_ImportData.GUID = pi_GUID
		and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) = 3
		and T_Question.Sequence = T_ImportData.Sequence
		and (T_Question.Title != T_ImportData.Content 
			or T_Question.Description != T_ImportData.Content
			or T_Question.KeyWords != case when ifnull(T_ImportData.KeyWords,'') = 'null' then null 
										else T_ImportData.KeyWords end
			or T_Question.AgeGroupName != T_ImportData.AgeGroupName
			)
		)
	;
	-- 目录-更新
	update T_Question,tmp_NeedUpdate_Question imp2
	set
		T_Question.Sequence = trim(imp2.Sequence)
		,T_Question.Title = imp2.Content
		,T_Question.Description = imp2.Content
		,T_Question.KeyWords = imp2.KeyWords
		,T_Question.AgeGroupName = imp2.AgeGroupName
		,T_Question.ParentMenu = imp2.ParentMenu
	where T_Question.ID = imp2.ID
	;
	drop table if exists tmp_NeedUpdate_Question 
	;

	-- 问题
	insert into T_Question
	(
		Sequence,Title,Description,KeyWords,AgeGroupName,ParentMenu
	)
	select 
		trim(Sequence),Content,Content,case when ifnull(KeyWords,'') = 'null' then null else KeyWords end
		,AgeGroupName,(select min(parent.ID) from T_Menu parent 
				where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
							,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
													)
				)
	from T_ImportData
	where GUID = pi_GUID
		and (length(Sequence) - length(replace(Sequence,'.',''))) = 3
		and Sequence not in (select tb.Sequence from T_Question tb)
	order by Sequence
	;

	/*
	-- 问题-更新
	update T_Question,T_ImportData
	set 
		T_Question.Sequence = T_ImportData.Sequence
		,T_Question.Title = T_ImportData.Content
		,T_Question.Description = T_ImportData.Content
		,T_Question.KeyWords = case when ifnull(T_ImportData.KeyWords,'') = 'null' then null else T_ImportData.KeyWords end
		,T_Question.AgeGroupName = T_ImportData.AgeGroupName
		,T_Question.ParentMenu = (select min(parent.ID) from T_Menu parent 
				where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
							,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
													)
				)
	where T_ImportData.GUID = pi_GUID
		and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) = 3
		and T_ImportData.Sequence = T_Question.Sequence
	;
	*/


	/*
	-- 目录-删除重复的
	delete from T_Solution
	where Sequence in (select imp2.Sequence
				from T_ImportData imp2
				where T_ImportData.GUID = pi_GUID
					and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) > 3
				)
		and SText in (select tb2.Content
				from (select * from T_Solution) tb2
				group by tb2.Content
				having count(tb2.ID) > 1
				)
	;
	*/
	/*
	-- 删除年龄段的数据
	delete from T_Solution
	where AgeGroupName in (select distinct imp2.AgeGroupName
				from T_ImportData imp2
				where T_ImportData.GUID = pi_GUID
					and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) > 3
				)
	;
	*/

	-- 创建需更新Question目录的临时表
	drop table if exists tmp_NeedUpdate_Solution  ;
	create table tmp_NeedUpdate_Solution (
	select T_Solution.ID,T_ImportData.Sequence,T_ImportData.Content
			,(select min(parent.ID) from T_Question parent 
				where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
							,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
													)
				) as Question
	from T_Solution,T_ImportData
	where T_ImportData.GUID = pi_GUID
		and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) > 3
		and T_ImportData.Sequence = T_Solution.Sequence
		and (T_Solution.SText != T_ImportData.Content 
			)
		)
	;

	-- 目录-更新
	update T_Solution,tmp_NeedUpdate_Solution imp2
	set
		T_Solution.Sequence = trim(imp2.Sequence)
		,T_Solution.SText = imp2.Content
		,T_Solution.Question = imp2.Question
	where T_Solution.ID = imp2.ID
	;
	drop table if exists tmp_NeedUpdate_Solution 
	;

	/*
	-- 答案-更新
	update T_Solution,T_ImportData
	set
		T_Solution.Sequence = T_ImportData.Sequence
		,T_Solution.SText = T_ImportData.Content
		,T_Solution.Intro = null
		,T_Solution.Question = (select min(parent.ID) from T_Question parent 
				where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
							,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
													)
				)
	where T_ImportData.GUID = pi_GUID
		and (length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.',''))) > 3
		and T_ImportData.Sequence = T_Question.Sequence
	;
	*/
	-- 答案
	insert into T_Solution
	(
		Sequence,SText,Intro,Question
	)
	select 
		trim(Sequence),Content,null,(select min(parent.ID) from T_Question parent 
				where parent.Sequence = substring_index(T_ImportData.Sequence,'.'
							,(length(T_ImportData.Sequence) - length(replace(T_ImportData.Sequence,'.','')))
													)
				)
	from T_ImportData
	where GUID = pi_GUID
		and (length(Sequence) - length(replace(Sequence,'.',''))) > 3
		and Sequence not in (select tb.Sequence from T_Solution tb)
	order by Sequence
	;
	-- 更新年龄段表Sequence
	update T_AgeGroup,T_Menu menu
	set T_AgeGroup.Sequence = trim(menu.Sequence)
	where 
		T_AgeGroup.Code = menu.AgeGroupName
		and (length(menu.Sequence) - length(replace(menu.Sequence,'.',''))) = 0
		and menu.Sequence is not null and menu.Sequence != ''
		;

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


    -- 删除没用的年龄段
	delete from T_AgeGroup
	where Code not in (select distinct AgeGroupName from T_Menu menu)
	;


end ;




