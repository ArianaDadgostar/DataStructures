using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedConcepts
{
    public class LCUCache<TKey, TVal>
    {
        Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TVal>>> Map;

        LinkedList<KeyValuePair<TKey, TVal>> LinkedList;

        public int Count { get { return LinkedList.Count; } }        

        public int max;

        public LCUCache(int max)
        {
            Map = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TVal>>>();
            LinkedList = new LinkedList<KeyValuePair<TKey, TVal>>();
            this.max = max;
        }

        public void Add(TKey key, TVal val)
        {
            LinkedList.AddFirst(new KeyValuePair<TKey, TVal> (key, val));
            if (!Map.ContainsKey(key))
            {
                Map.Add(key, LinkedList.First);
            }
            else
            {
                LinkedList.Remove(Map[key]);
                Map[key] = LinkedList.First;
            }

            if (Map.Count <= max) return;

            Map.Remove(LinkedList.Last.Value.Key);
            LinkedList.RemoveLast();
        }

        public bool TryGetValue(TKey key, out TVal value)
        {
            if (Map.ContainsKey(key))
            {
                value = Map[key].Value.Value;
                LinkedList.Remove(Map[key]);
                LinkedList.AddFirst(new KeyValuePair<TKey, TVal>(key, value));

                return true;
            }

            value = default;
            return false;
        }
    }
}
