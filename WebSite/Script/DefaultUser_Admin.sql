
if not exists(select 1 from T_User where Code = 'Admin')
begin
	insert into T_User (
		Code,Name,Type,PassWord,Power
	)values(
		-- 其中密码是密文的
		'Admin','Admin','管理','E10ADC3949BA59ABBE56E057F20F883E',3
	)

end


