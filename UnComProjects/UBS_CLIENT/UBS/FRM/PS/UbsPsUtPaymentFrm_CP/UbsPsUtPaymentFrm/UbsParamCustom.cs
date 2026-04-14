using System;
using UbsService;

namespace UbsBusiness
{
    public class UbsParamCustom : UbsParam
    {
        public UbsParamCustom(object[,] paramItems):base(paramItems)
        {
        }
        public UbsParamCustom(object[] parameters) : base(parameters)
        {
        }

        public string GetParamOutString(string key)
        {
            if (!this.Contains(key) || this.Value(key) == null)
            {
                return string.Empty;
            }

            return Convert.ToString(this.Value(key));
        }

        public int GetParamOutInt(string key)
        {
            if (!this.Contains(key) || this.Value(key) == null)
            {
                return 0;
            }

            return Convert.ToInt32(this.Value(key));
        }

        public bool GetParamOutBool(string key)
        {
            if (!this.Contains(key) || this.Value(key) == null)
            {
                return false;
            }

            return Convert.ToBoolean(this.Value(key));
        }
    }
}
