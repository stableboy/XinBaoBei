using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class CommonHelper
    {
        #region Object与值类型的转换

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
            int result = int.MinValue;
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
            long result = long.MinValue;
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
            decimal result = decimal.MinValue;

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

        public static DateTime GetDateTime(object obj)
        {
            DateTime result = new DateTime(1999,1,1);

            DateTime.TryParse(GetString(obj), out result);

            return result;
        }


        #endregion

        #region MD5


        /// <summary>
        /// MD5不可逆加密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static string GetMD5(string message)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = Encoding.UTF8.GetBytes(message);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string result = string.Empty;
            for (int i = 0; i < bytHash.Length; i++)
            {
                result += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return result;
        }

        #endregion

        #region 数据库相关

        public static string GetStringWithNull(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "null";
            }
            else
            {
                return str;
            }
        }

        #endregion
    }
}