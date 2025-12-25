using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSelfBalancing
{
    public class BNode<T> where T : IComparable<T>
    {
        public BNode<T> parent;
        public T[] value;
        public BNode<T>[] Children;
        public int valueSize;

        public BNode()
        {
            value = new T[4];
            Children = new BNode<T>[5];
            valueSize = 0;
        }
    }

    public class BTree<T> where T : IComparable<T>
    {
        public BNode<T> Head;

        public BNode<T> Resize(T value, BNode<T> node)
        {
            if (node.parent == null)
            {
                node.parent = new BNode<T>();
                node.parent.value[0] = node.value[1];
                node.parent.valueSize++;

                for (int i = 0; i <= node.parent.valueSize; i++)
                {
                    node.parent.Children[i] = new BNode<T>();
                }
            }
            else
            {
                Insert(node.value[1], node.parent); //WONT WORK CUZ ITS GONNA GO TO CHILD AGAIN (INFINITE LOOP + CRASH)
            }

            node.value[1] = default(T);

            for (int i = 0; i < node.value.Length; i++)
            {
                if (node.value[i].Equals(default(T))) continue;

                T val = node.value[i];
                node.value[i] = default(T);
                node.valueSize--;
                Insert(val, node.parent);
            }

            return node.parent;
        }

        public void PhysicalInsert(T value, int index, BNode<T> node)
        {
            T replace = value;
            T current = node.value[index];
            while (!replace.Equals(default(T)))
            {
                node.value[index] = replace;
                index++;
                replace = current;
                if (replace.Equals(default(T))) continue;

                current = node.value[index];
            }

            node.valueSize++;
        }

        public BNode<T> Insert(T value, BNode<T> node)
        {
            if (value.Equals(default(T))) return node;
            if(node == null)
            {
                node = new BNode<T>();
                node.value[0] = value;
                node.valueSize++;
                return node;
            }

            bool inserted = false;

            for (int i = 0; i < node.valueSize; i++)
            {
                if (value.CompareTo(node.value[i]) >= 0 && !node.value[i].Equals(default(T))) continue;

                if (node.Children[i] != null)
                {
                    Insert(value, node.Children[i]);
                    inserted = true;
                    break;
                }

                PhysicalInsert(value, i, node);
                inserted = true;
                break;
            }

            if (node.Children[node.valueSize] != null && !inserted)
            {
                node.Children[node.valueSize] = Insert(value, node.Children[node.valueSize]);
            }
            else if (!inserted)
            {
                PhysicalInsert(value, node.valueSize, node);
            }

            if (node.valueSize <= 3) return node;

            return Resize(value, node);;
        }

        public BNode<T> Search(T value, BNode<T> node)
        {
            for (int i = 0; i <= node.valueSize; i++)
            {
                if(value.Equals(node.value[i])) return node;

                if (value.CompareTo(node.value[i]) >= 0) continue;

                return Search(value, node.Children[i]);
            }

            return Search(value, node.Children[node.valueSize]);
        }
    }
}
