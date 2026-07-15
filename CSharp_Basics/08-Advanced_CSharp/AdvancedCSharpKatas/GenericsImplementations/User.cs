using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsImplementations
{
    public class User : IEntity
    {

        public User(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}
