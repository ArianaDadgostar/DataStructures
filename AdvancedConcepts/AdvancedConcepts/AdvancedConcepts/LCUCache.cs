using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedConcepts
{
    public class LCUCache<TKey, TVal>
    {
        Dictionary<TKey, TVal> Map;

        LinkedList<TVal> LinkedList;

        public int Count { get { return LinkedList.Count; } }        

        public int max;

        public LCUCache(int max)
        {
            Map = new Dictionary<TKey, TVal>();
            LinkedList = new LinkedList<TVal>();
            this.max = max;
        }

        public void Add(TKey key, TVal val)
        {
            if(!Map.ContainsKey(key))
            {
                Map.Add(key, val);
                return;
            }
            Map[key] = val;
            LinkedList.AddFirst(val);
            if (Map.Count <= max) return;

            LinkedList.RemoveLast();
        }

        public bool TryGetValue(TKey key, out TVal value)
        {
            if(Map.ContainsKey(key))
            {
                value = Map[key];
                return true;
            }

            value = default(TVal);
            return false;
        }
    }
}
