using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractClassesAndMethodsProject
{
    internal abstract class Shape
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public abstract void Draw();
    }
}
