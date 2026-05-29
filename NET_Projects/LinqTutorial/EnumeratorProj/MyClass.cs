using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumeratorProj
{
    public class MyClass
    {
        public IEnumerable<string> BlackAndWhite()
        {
            yield return "black";
            yield return "gray";
            yield return "white";
        }
    }
}
