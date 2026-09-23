using System;
using System.Collections.Generic;
using System.Text;

namespace TraficLightStateMachine
{
    public static class PermissionsHelper
    {
        public static bool CanWrite(Permissions p)
        {
            return p.HasFlag(Permissions.Write);
        }
    }
}
