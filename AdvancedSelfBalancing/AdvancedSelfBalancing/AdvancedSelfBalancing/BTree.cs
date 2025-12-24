using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSelfBalancing
{
    public class BNode<T> where T : IComparable<T>
    {
        public T value;
        public BNode<T>[] Children;
    }

    public class BTree<T> where T : IComparable<T>
    {
        BNode<T>[] Head;

        public void Resize(T value, BNode<T>[] nodes)
        {

        }

        public bool Insert(T value, BNode<T>[] nodes)
        {
            if(nodes.Length >= 3)
            {
                //Resize
            }

            bool foundVal = false;
            T update = default(T);
            for (int i = 0; i < nodes.Length; i++)
            {
                if(foundVal)
                {
                    nodes[i].value = update;
                }

                if (nodes[i].value != null && value.CompareTo(nodes[i].value) > 0) continue;

                if (nodes[i].Children == null)
                {
                    foundVal = true;
                    update = nodes[i].value;
                    nodes[i].value = value;
                    continue;
                }

                Insert(value, nodes[i].Children);
            }

            return foundVal;
        }
    }
}
