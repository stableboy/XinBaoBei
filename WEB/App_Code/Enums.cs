using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
///Enums 的摘要说明
/// </summary>
public class Enums
{

}

// 数据库类型
/// <summary>
/// 数据库类型
/// </summary>
public enum DatabaseTypeEnum
{ 
    // Mysql
    /// <summary>
    /// Mysql
    /// </summary>
    MySql = 0,

    // SqlServer
    /// <summary>
    /// SqlServer
    /// </summary>
    SqlServer = 1,
}

// 用户权限枚举
/// <summary>
/// 用户权限枚举
/// </summary>
public enum UserAuthorityEnum
{ 
    // 来宾，只能浏览查询
    /// <summary>
    /// 来宾，只能浏览查询
    /// </summary>
    Guest = 0,

    // 维护，维护档案权限
    /// <summary>
    /// 维护，维护档案权限
    /// </summary>
    Maintainer = 1,

    // 管理员，所有权限
    /// <summary>
    /// 管理员，所有权限
    /// </summary>
    Administrator = 9,
}

// 菜单类型
public enum MenuTreeTypeEnum
{
    // 目录
    /// <summary>
    /// 目录
    /// </summary>
    Menu = 0,

    // 问题
    /// <summary>
    /// 问题
    /// </summary>
    Question = 1,

    // 答案
    /// <summary>
    /// 答案
    /// </summary>
    Answer = 2,
}

// 帐套
/// <summary>
/// 帐套
/// </summary>
public enum SetOfBookType
{ 
    // 正式
    /// <summary>
    /// 正式
    /// </summary>
    Offical = 0,

    // 测试
    /// <summary>
    /// 测试
    /// </summary>
    Test = 1,

    /// <summary>
    /// 测评系统
    /// </summary>
    NuoHeTest = 2,

}
