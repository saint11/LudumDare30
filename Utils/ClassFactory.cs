using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones.Utils
{
    public class ClassFactory
    {
        Dictionary<string, Type> lookup = new Dictionary<string, Type>();

        public ClassFactory()
        {

        }

        public ClassFactory Add<T>() where T : Entity
        {
            lookup.Add(typeof(T).Name, typeof(T));
            return this;
        }

        public Entity Create(string name, object[] parameters)
        {
            return (Entity)Activator.CreateInstance(lookup[name],parameters);
        }
    }
}
