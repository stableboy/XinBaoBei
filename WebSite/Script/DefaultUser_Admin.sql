
if not exists(select 1 from T_User where Code = 'Admin')
begin
	insert into T_User (
		Code,Name,Type,PassWord,Power
	)values(
		-- �������������ĵ�
		'Admin','Admin','����','E10ADC3949BA59ABBE56E057F20F883E',3
	)

end


