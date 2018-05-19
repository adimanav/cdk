using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashmap
{
    public class Trie<Tvalue>
    {
        private char keyChar;
        private Tvalue value;
        private const int numChars = 62;
        private bool isLeaf;
        private int numChildren;

        private int count;
        public int Count
        {
            get
            {
                return count;
            }
        }

        public Trie<Tvalue>[] Children { get; }

        public Trie()
        {
            this.Children = new Trie<Tvalue>[numChars] ;
            this.isLeaf = false;
            this.count = 0;
            this.numChildren = 0;
        }

        public void Add(string key, Tvalue value)
        {
            var p = this;
            for (int i = 0; i < key.Length; i++)
            {
                char ch = key[i];
                var ord = (int) ch;
                if (null == p.Children[ord])
                {
                    p.Children[ord] = new Trie<Tvalue>();
                }
                p.Children[ord].keyChar = ch;
                p.numChildren++;
                if (key.Length - 1 == i)
                {
                    p.Children[ord].value = value;
                    p.Children[ord].isLeaf = true;
                }
                else
                {
                    p = p.Children[ord];
                }
            }
            this.count++;
        }

        public void Remove(string key)
        {
            var stack = new Stack<Trie<Tvalue>>(key.Length);
            var p = this;
            for (int i = 0; i < key.Length; i++)
            {
                char ch = key[i];
                var ord = (int)ch;
                var child = p.Children[ord];
                if (null == child || ch != child.keyChar)
                {
                    throw new ApplicationException("Key not found.");
                }
                if (key.Length - 1 == i)
                {
                    if (!child.isLeaf)
                        throw new ApplicationException("Key not found.");

                    int j = key.Length - 1;
                    while (stack.Count > 0)
                    {
                        var node = stack.Pop();
                        if (child.numChildren == 0) node.Children[key[j]] = null;
                        node.numChildren--;
                        if (node.numChildren > 0) break;
                        j--;
                    }
                }
                else
                {
                    stack.Push(child);
                    p = child;
                }
            }
            this.count--;
        }

        public Tvalue Get(string key)
        {
            Tvalue result = default(Tvalue);
            var p = this;
            for (int i = 0; i < key.Length; i++)
            {
                char ch = key[i];
                var ord = (int)ch;
                var child = p.Children[ord];
                if (null == child || ch != child.keyChar)
                {
                    throw new ApplicationException("Key not found.");
                }
                if (key.Length - 1 == i)
                {
                    if (!child.isLeaf)
                        throw new ApplicationException("Key not found.");

                    result = child.value;
                    break;
                }
                p = p.Children[ord];
            }
            return result;
        }
    }
}
