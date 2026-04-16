using System;
using System.Collections.Generic;
using UbsService;

namespace UbsBusiness
{
    public class UbsParamCustom : UbsParam
    {
        public readonly DateTime MinDateValue = new DateTime(2222, 1, 1);
        public UbsParamCustom() : base()
        {
        }
        public UbsParamCustom(object[,] paramItems):base(paramItems)
        {
        }
        public UbsParamCustom(object[] parameters) : base(parameters)
        {
        }
        public UbsParamCustom(object[,] paramItems, bool comArray) : base(paramItems, comArray)
        {
        }
        public UbsParamCustom(string xmlparameters) : base(xmlparameters)
        {
        }
        public UbsParamCustom(KeyValuePair<string, object>[] parametersKVP) : base(parametersKVP)
        {
        }
        public UbsParamCustom(Dictionary<string,object> parametersDic) : base(parametersDic)
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

        public DateTime GetParamOutDateTime(string key)
        {
            if (!this.Contains(key) || this.Value(key) == null)
            {
                return MinDateValue;
            }

            return Convert.ToDateTime(this.Value(key));
        }

        public decimal GetParamOutDecimal(string key)
        {
            if (!this.Contains(key) || this.Value(key) == null)
            {
                return 0m;
            }

            return Convert.ToDecimal(this.Value(key));
        }
    }
}
