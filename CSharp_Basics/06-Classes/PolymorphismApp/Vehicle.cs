
namespace MehodOverloadingProject
{
    internal class Vehicle
    {
        protected void PrintSomething()
        {
            Console.WriteLine("I print something");
        }
        public virtual void GetVehicleName()
        {
            Console.WriteLine("I am vehicle");
        }
        public void StartEngine()
        {
        }

        public void StopEngine()
        {
        }
    }
}
