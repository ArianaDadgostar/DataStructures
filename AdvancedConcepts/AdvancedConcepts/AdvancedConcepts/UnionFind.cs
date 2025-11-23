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


        public UnionFind()
        {
            hashMap = new Dictionary<T, int>();
        }

        public void Add(T key, int value)
        {
            hashMap.Add(key, value);
        }

        public void Union(T setKey, T additive)
        {
            hashMap[additive] = hashMap[setKey];
            //int val = hashMap[additive];
            //for (int i = 0; i < hashMap.Count; i++)
            //{
            //    if (hashMap.ElementAt(i).Value != val) continue;
                   
            //    hashMap[hashMap.ElementAt(i).Key] = hashMap[setKey];
            //}
        }

        public bool IsConnected(T first, T second)
        {
            return hashMap[first] == hashMap[second];
        }

        public int Find(T key)
        {
            return hashMap[key];
        }
    }
}
