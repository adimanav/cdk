using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashmap
{
    public class Hashmap<Tkey, Tvalue>
    {
        private LinkedList<Tuple<Tkey, Tvalue>>[] container;
        private int capacity = 1000;

        private int count = 0;
        public int Count
        {
            get
            {
                return count;
            }
        }

        public Hashmap()
        {
            this.container = new LinkedList<Tuple<Tkey, Tvalue>>[this.capacity];
        }

        public Hashmap(int capacity)
        {
            this.capacity = capacity;
            this.container = new LinkedList<Tuple<Tkey, Tvalue>>[capacity];
        }

        private int GetHash(Tkey key)
        {
            return Math.Abs(key.GetHashCode() % capacity);
        }

        public void Add(Tkey key, Tvalue value)
        {
            int hash = GetHash(key);
            if (null == container[hash])
            {
                container[hash] = new LinkedList<Tuple<Tkey, Tvalue>>();
            }

            var p = container[hash].First;
            while (null != p)
            {
                if (p.Value.Item1.Equals(key))
                {
                    container[hash].Remove(p);
                    break;
                }
                p = p.Next;
            }

            container[hash].AddLast(new Tuple<Tkey, Tvalue>(key, value));
            this.count++;
        }

        public void Remove(Tkey key)
        {
            int hash = GetHash(key);

            if (null == container[hash])
                throw new ApplicationException("Key not found.");

            var found = false;
            var p = container[hash].First;
            while (null != p)
            {
                if (p.Value.Item1.Equals(key))
                {
                    container[hash].Remove(p);
                    this.count--;
                    found = true;
                    break;
                }
                p = p.Next;
            }

            if (!found)
                throw new ApplicationException("Key not found.");

        }

        public Tvalue Get(Tkey key)
        {
            Tvalue result = default(Tvalue);

            int hash = GetHash(key);

            if (null == container[hash])
                throw new ApplicationException("Key not found.");

            var found = false;
            var p = container[hash].First;
            while (null != p)
            {
                if (p.Value.Item1.Equals(key))
                {
                    result = p.Value.Item2;
                    found = true;
                    break;
                }
                p = p.Next;
            }

            if (!found)
                throw new ApplicationException("Key not found.");

            return result;
        }
    }
}
