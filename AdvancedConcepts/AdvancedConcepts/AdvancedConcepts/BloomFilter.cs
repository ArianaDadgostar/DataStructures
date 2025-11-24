using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedConcepts
{
    public class BloomFilter<T> where T : IComparable<T>
    {
        public bool[] filter;

        public BloomFilter(int cap)
        {
            filter = new bool[cap];
        }

        public int[] CalculateIndexes(ref T item)
        {
            int[] indexes = new int[3];
            indexes[0] = Math.Abs(HashFuncOne(item) % filter.Length);
            indexes[1] = Math.Abs(HashFuncTwo(item) % filter.Length);
            indexes[2] = Math.Abs(HashFuncThree(item) % filter.Length);

            return indexes;
        }

        public void Add(T item)
        {
            int[] indexes = CalculateIndexes(ref item);

            filter[indexes[0]] = true;
            filter[indexes[1]] = true;
            filter[indexes[2]] = true;
        }

        public bool ProbablyContains(T item)
        {
            int[] indexes = CalculateIndexes(ref item);

            if (!filter[indexes[0]] || !filter[indexes[1]] || !filter[indexes[2]]) return false;

            return true;
        }

        private int HashFuncOne(T item)
        {
            return item.GetHashCode();
        }

        private int HashFuncTwo(T item)
        {
            string dummyString = "dummystring";
            return (dummyString, item).GetHashCode();
        }

        private int HashFuncThree(T item)
        {
            int hash = 17;

            hash *= (HashFuncOne(item), HashFuncTwo(item)).GetHashCode();

            return hash;

        }
    }
}
