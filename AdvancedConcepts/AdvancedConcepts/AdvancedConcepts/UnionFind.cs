using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedConcepts
{
    public class UnionFind<T> where T : IComparable<T>
    {
        public Dictionary<T, int> hashMap;
        int[] sets;


        public UnionFind()
        {
            hashMap = new Dictionary<T, int>();
            sets = new int[100];
        }

        public void Add(T key)
        {
            hashMap.Add(key, hashMap.Count);

            sets[hashMap.Count - 1] = hashMap.Count;
        }

        public void Union(T setKey, T additive)
        {
            int set = hashMap[setKey];
            int get = sets[hashMap[additive]];
            for (int i = 0; i < hashMap.Count; i++)
            {
                if (sets[hashMap.ElementAt(i).Value] != get) continue;

                sets[hashMap.ElementAt(i).Value] = sets[hashMap[setKey]];
            }
        }

        public bool IsConnected(T first, T second)
        {
            return sets[hashMap[first]] == sets[hashMap[second]];
        }

        public int Find(T key)
        {
            return hashMap[key];
        }
    }

    public class QuickUnion<T> where T : IComparable<T>
    {
        public Dictionary<T, int> hashMap;
        int[] parents;

        public QuickUnion()
        {
            hashMap = new Dictionary<T, int>();
            parents = new int[100];
        }

        public void Add(T key)
        {
            hashMap.Add(key, hashMap.Count);
            parents[hashMap.Count - 1] = -1;
        }

        public void Union(T setKey, T additive)
        {
            parents[hashMap[setKey]] = hashMap[additive];
        }

        public bool IsConnected(T first, T second)
        {
            return Find(first) == Find(second);
        }

        public int Find(T key)
        {
            int parent = hashMap[key];
            while (parents[parent] != -1)
            {
                parent = parents[parent];
            }

            return parent;
        }
    }
}
