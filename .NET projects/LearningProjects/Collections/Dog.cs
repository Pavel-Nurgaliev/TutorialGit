using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class Dog
    {
        public Dog(string name, bool isNaughtyDog)
        {
            this.Name = name;
            this.IsNaughtyDog = isNaughtyDog;
        }
        public string Name { get; set; }
        public bool IsNaughtyDog { get; set; }

        public void GiveTreat(int treatsNumber)
        {
            Console.WriteLine("Dog: {0} said wuoff {1} times!", Name, treatsNumber);
        }
    }

    class DogShelter:IEnumerable<Dog>
    {
        private List<Dog> dogs;

        public DogShelter()
        {
            dogs = new List<Dog>()
                {
                    new Dog("Casper", false),
                    new Dog("Sif", true),
                    new Dog("Oreo", false),
                    new Dog("Pixel", false),
                };
        }

        public IEnumerator<Dog> GetEnumerator()
        {
            return dogs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
