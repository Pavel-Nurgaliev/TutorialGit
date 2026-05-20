using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp
{
    abstract class Car
    {
        public Car()
        {
            Console.WriteLine("I Car");
        }
        public virtual void Run()
        {
            Console.WriteLine("I Run");
        }
    }
}
