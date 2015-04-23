using System;
using System.Collections.Generic;
// using System.Linq;
using System.Web;

/// <summary>
///Judge_User 的摘要说明
/// </summary>
public class Judge_User
{
	public Judge_User()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    private long id;

    public long ID
    {
        get { return id; }
        set { id = value; }
    }

    private string account;

    public string Account
    {
        get { return account; }
        set { account = value; }
    }

    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private string region;

    public string Region
    {
        get { return region; }
        set { region = value; }
    }

    private string passwd;

    public string Passwd
    {
        get { return passwd; }
        set { passwd = value; }
    }

    private string sex;

    public string Sex
    {
        get { return sex; }
        set { sex = value; }
    }

    private string age;

    public string Age
    {
        get { return age; }
        set { age = value; }
    }

    private string birthday;

    public string Birthday
    {
        get { return birthday; }
        set { birthday = value; }
    }

    private string pic;

    public string Pic
    {
        get { return pic; }
        set { pic = value; }
    }


    private string address;

    public string Address
    {
        get { return address; }
        set { address = value; }
    }
    private string tel;

    public string Tel
    {
        get { return tel; }
        set { tel = value; }
    }
    //private long branch_id;

    //public long Branch_ID
    //{
    //    get { return branch_id; }
    //    set { branch_id = value; }
    //}
    //private string branch_name;

    //public string Branch_Name
    //{
    //    get { return branch_name; }
    //    set { branch_name = value; }
    //}
    //private long unit_id;

    //public long Unit_id
    //{
    //    get { return unit_id; }
    //    set { unit_id = value; }
    //}
}