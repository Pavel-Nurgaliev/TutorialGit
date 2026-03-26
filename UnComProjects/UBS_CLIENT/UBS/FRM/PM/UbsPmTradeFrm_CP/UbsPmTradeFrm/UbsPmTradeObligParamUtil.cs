using System;

namespace UbsBusiness
{
    internal static class UbsPmTradeObligParamUtil
    {
        public static bool IsObjectParamPart2Key(string key, string objectPrefix)
        {
            if (key == null || objectPrefix == null) return false;
            if (key.Length <= objectPrefix.Length)
                return false;
            if (!key.StartsWith(objectPrefix, StringComparison.Ordinal))
                return false;
            return key[objectPrefix.Length] == '2';
        }
    }
}
