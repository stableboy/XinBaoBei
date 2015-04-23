using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
///PubClass 的摘要说明
/// </summary>
public class PubClass
{
    #region 字段转化

    //将字符串转换为指定类型
    /// <summary>
    /// 将字符串转换为指定类型;
    /// </summary>
    /// <param name="objValue">字符串的值</param>
    /// <param name="fieldType">类型</param>
    /// <returns>转换结果</returns>
    public static object GetObjFromString(object objValue, string fieldType)
    {
        if (fieldType != null)
        {
            string strValue = objValue.ToString().Trim();
            switch (fieldType.Trim().ToLower())
            {
                case "string":
                    if (objValue != null)
                    {
                        return objValue.ToString().Trim();
                    }
                    return string.Empty;
                    break;
                case "short":
                    short stResult;
                    bool blstTmp;
                    if (short.TryParse(strValue, out stResult))
                    {
                        return stResult;
                    }
                    else if (bool.TryParse(strValue, out blstTmp))
                    {
                        stResult = Convert.ToInt16(blstTmp);
                        return stResult;
                    }
                    return (short)short.MinValue;
                    break;
                case "int":
                    int iResult;
                    bool bliTmp;
                    if (int.TryParse(strValue, out iResult))
                    {
                        return iResult;
                    }
                    else if (bool.TryParse(strValue, out bliTmp))
                    {
                        iResult = Convert.ToInt32(bliTmp);
                        return iResult;
                    }
                    return (int)int.MinValue;
                    break;
                case "long":
                    long lResult;
                    bool bllTmp;
                    if (long.TryParse(strValue, out lResult))
                    {
                        return lResult;
                    }
                    else if (bool.TryParse(strValue, out bllTmp))
                    {
                        lResult = Convert.ToInt64(bllTmp);
                        return lResult;
                    }
                    return (long)long.MinValue;
                    break;
                case "bool":
                    bool blResult;
                    if (bool.TryParse(strValue, out blResult))
                    {
                        return blResult;
                    }
                    return false;
                    break;
                case "char":
                    char crResult;
                    if (char.TryParse(strValue, out crResult))
                    {
                        return crResult;
                    }
                    return char.MinValue;
                    break;
                case "decimal":
                    decimal decResult;
                    if (decimal.TryParse(strValue, out decResult))
                    {
                        return decResult;
                    }
                    return decimal.Zero;
                    break;
                case "double":
                    double douResult;
                    if (double.TryParse(strValue, out douResult))
                    {
                        return douResult;
                    }
                    return (double)0;
                    break;
                case "float":
                    float fltResult;
                    if (float.TryParse(strValue, out fltResult))
                    {
                        return fltResult;
                    }
                    return (float)0;
                    break;
                case "null":
                    return objValue;
                    break;
                default:
                    return objValue;
                    break;
            }
        }
        //没有设置类型，返回传入参数
        return objValue;
    }

    public static string GetString(object obj)
    {
        if (obj != null)
            return obj.ToString().Trim();

        return string.Empty;
    }

    public static short GetShort(object obj)
    {
        short result = short.MinValue;
        bool blstTmp;
        string strValue = GetString(obj);

        if (!short.TryParse(strValue, out result))
        {
            if (bool.TryParse(strValue, out blstTmp))
            {
                result = Convert.ToInt16(blstTmp);
            }
        }

        return result;
    }

    public static int GetInt(object obj)
    {
        return GetInt(obj, int.MinValue);
    }

    public static int GetInt(object obj,int defaultValue)
    {
        int result = defaultValue;
        bool blstTmp;
        string strValue = GetString(obj);

        if (!int.TryParse(GetString(obj), out result))
        {
            if (bool.TryParse(strValue, out blstTmp))
            {
                result = Convert.ToInt32(blstTmp);
            }
        }

        return result;
    }

    public static long GetLong(object obj)
    {
        return GetLong(obj, long.MinValue);
    }

    public static long GetLong(object obj, long defaultValue)
    {
        long result = defaultValue;
        bool blstTmp;
        string strValue = GetString(obj);

        if (!long.TryParse(GetString(obj), out result))
        {
            if (bool.TryParse(strValue, out blstTmp))
            {
                result = Convert.ToInt32(blstTmp);
            }
        }

        return result;
    }

    public static bool GetBool(object obj)
    {
        bool result = false;

        bool.TryParse(GetString(obj), out result);

        return result;
    }

    public static decimal GetDecimal(object obj)
    {
        //decimal result = decimal.MinValue;
        decimal result = 0;

        decimal.TryParse(GetString(obj), out result);

        return result;
    }

    public static double GetDouble(object obj)
    {
        double result = double.MinValue;

        double.TryParse(GetString(obj), out result);

        return result;
    }

    public static float GetFloat(object obj)
    {
        float result = float.MinValue;

        float.TryParse(GetString(obj), out result);

        return result;
    }

    public static DateTime GetDateTime(object obj, DateTime defaultValue)
    {
        if (obj != null)
        {
            DateTime dt;
            if (DateTime.TryParse(obj.ToString(), out dt)
                )
            {
                return dt;
            }
        }
        return defaultValue;
    }

    public static DateTime? GetDateTime(object obj)
    {
        if (obj != null)
        {
            DateTime dt;
            if (DateTime.TryParse(obj.ToString(), out dt)
                )
            {
                return dt;
            }
        }
        return null;
    }

    // 数字变字符串(去掉无用的0)
    /// <summary>
    ///  数字变字符串(去掉无用的0)
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    public static string GetStringRemoveZero(decimal d)
    {
        return d.ToString("G0");
    }

    #endregion

    public static bool IsNull(string value)
    {
        //if (str == null || str.Trim() == string.Empty)
        //    return true;

        //return false;

        if (value != null)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static Enum TryParse(string value, Enum defaultValue)
    {
        try
        {
            Enum em = (Enum)Enum.Parse(defaultValue.GetType(), value);
            return em;
        }
        catch
        {
            return defaultValue;
        }
    }

}