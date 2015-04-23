using System;
using System.Collections.Generic;
using System.Data;
// using System.Linq;
using System.Text;
// using System.Threading.Tasks;

namespace DLL
{
    [Serializable]
    public class User
    {

        /// 实体 T_User 属性设计  by  xu , Inc.

        // 主键
        private long iD;
        /// <summary>
        /// 主键
        /// </summary>
        public virtual long ID
        {
            get
            {
                return iD;
            }
            set
            {
                if (iD != value)
                {
                    iD = value;
                }
            }
        }

        // 编号
        private string code;
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Code
        {
            get
            {
                return code;
            }
            set
            {
                if (code != value)
                {
                    code = value;
                }
            }
        }

        // 姓名
        private string name;
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }

        // 密码
        private string passWord;
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string PassWord
        {
            get
            {
                return passWord;
            }
            set
            {
                if (passWord != value)
                {
                    passWord = value;
                }
            }
        }

        // 用户类型
        private string type;
        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (type != value)
                {
                    type = value;
                }
            }
        }

        // 权限
        private int power;
        /// <summary>
        /// 权限
        /// </summary>
        public virtual int Power
        {
            get
            {
                return power;
            }
            set
            {
                if (power != value)
                {
                    power = value;
                }
            }
        }

        // 是否可用
        private bool ifUse;
        /// <summary>
        /// 是否可用
        /// </summary>
        public virtual bool IfUse
        {
            get
            {
                return ifUse;
            }
            set
            {
                if (ifUse != value)
                {
                    ifUse = value;
                }
            }
        }

        #region Disuse 照片

        //// 照片
        //private image photo;
        ///// <summary>
        ///// 照片
        ///// </summary>
        //public virtual image Photo
        //{
        //    get
        //    {
        //        return photo;
        //    }
        //    set
        //    {
        //        if (photo != value)
        //        {
        //            photo = value;
        //        }
        //    }
        //}

        #endregion

        // 备注
        private string memo;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo
        {
            get
            {
                return memo;
            }
            set
            {
                if (memo != value)
                {
                    memo = value;
                }
            }
        }

        public static User GetEntityFromDataRow(DataRow row)
        {
            if (row != null)
            {
                User entity = new User();

                entity.ID = Common.CommonHelper.GetLong(row["ID"]);
                entity.Code = Common.CommonHelper.GetString(row["Code"]);
                entity.IfUse = Common.CommonHelper.GetBool(row["IfUse"]);
                entity.Memo = Common.CommonHelper.GetString(row["Memo"]);
                entity.Name = Common.CommonHelper.GetString(row["Name"]);
                entity.PassWord = Common.CommonHelper.GetString(row["PassWord"]);
                entity.Power = Common.CommonHelper.GetInt(row["Power"]);
                entity.Type = Common.CommonHelper.GetString(row["Type"]);

                return entity;
            }
            return null;
        }


        // 当前帐套
        private SetOfBookType setOfBook = SetOfBookType.Offical;
        /// <summary>
        /// 当前帐套
        /// </summary>
        public SetOfBookType SetOfBook
        {
            get { return setOfBook; }
            set { setOfBook = value; }
        }

    }
}

