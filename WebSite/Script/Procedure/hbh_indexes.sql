
drop index T_Menu_Sequence on T_Menu;
-- 目录表.序号
create index T_Menu_Sequence
on T_Menu (Sequence)
;
-- 目录表.年龄段
create index T_Menu_AgeGroupName
on T_Menu (AgeGroupName)
;


drop index T_Question_Sequence on T_Question;
-- 问题表.序号
create index T_Question_Sequence
on T_Question (Sequence)
;
-- 问题表.年龄段
create index T_Question_AgeGroupName
on T_Question (AgeGroupName)
;
-- 问题表.目录
create index T_Question_ParentMenu
on T_Question (ParentMenu)
;


drop index T_Solution_Sequence on T_Solution;
-- 答案表.序号
create index T_Solution_Sequence
on T_Solution (Sequence)
;
-- 答案表.问题
create index T_Solution_Question
on T_Solution (Question)
;


-- 导入数据表.GUID
create index T_ImportData_GUID
on T_ImportData (GUID)
;
-- 导入数据表.序号
create index T_ImportData_Sequence
on T_ImportData (Sequence)
;

