using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractClassesAndMethodsProject
{
    internal sealed class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Circle");
        }
    }
}
