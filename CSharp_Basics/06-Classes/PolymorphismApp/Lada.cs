using System;
using System.Collections.Generic;
using System.Text;

namespace MehodOverloadingProject
{
    internal class Lada : Vehicle
    {
        public override void GetVehicleName()
        {
            Console.WriteLine("I am Lada");
        }
    }
}
