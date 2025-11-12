using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AdvancedConcepts
{
    public class HashNode<T>
    {
        public T key;
        public T value;

        public HashNode<T> neighbor;
    }

    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        //public int nodeCount;
        //public int totalNodeCount;
        //public HashNode<T>[] nodes;
        private int count;
        private LinkedList<KeyValuePair<TKey, TValue>>[] backingArray;

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => count;

        public bool IsReadOnly => throw new NotImplementedException();

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public HashMap()
        {
            backingArray = new LinkedList<KeyValuePair<TKey, TValue>>[5];
            count = 0;
        }

        //public void ExpandNodeAddition(HashNode<T>[] newNodes, int index, T key, T value)
        //{
        //    if (newNodes[index] == null)
        //    {
        //        newNodes[index] = new HashNode<T> { key = key, value = value };
        //        return;
        //    }

        //    HashNode<T> current = newNodes[index];

        //    while (current.neighbor != null)
        //    {
        //        current = current.neighbor;
        //    }

        //    current.neighbor = new HashNode<T> { key = key, value = value };
        //}

        //public HashNode<T>[] Expand()
        //{
        //    HashNode<T>[] expanded = new HashNode<T>[nodeCount*=2];

        //    foreach(HashNode<T> node in nodes)
        //    {
        //        if (node == null) continue;
        //        HashNode<T> current = node;
        //        while(current != null)
        //        {
        //            int index = Math.Abs(current.key.GetHashCode());
        //            index %= nodeCount;
        //            ExpandNodeAddition(expanded, index, current.key, current.value);
        //            current = current.neighbor;
        //        }
        //    }

        //    return expanded;
        //}

        //public void AddNode(T key, T value)
        //{
        //    if(totalNodeCount >= nodeCount)
        //    {
        //        nodes = Expand();
        //    }
        //    int index = Math.Abs(key.GetHashCode());
        //    index %= nodeCount;

        //    if (nodes[index] == null)
        //    {
        //        nodes[index] = new HashNode<T> { key = key, value = value }; //DOES NOT DELETE CLONED "YAYAYA"
        //        totalNodeCount++;
        //        return;
        //    }

        //    HashNode<T> current = nodes[index];

        //    while(current.neighbor != null)
        //    {
        //        if (current.key.Equals(key)) throw new ArgumentException();
        //        current = current.neighbor;
        //    }

        //    current.neighbor = new HashNode<T> { key = key, value = value };
        //    totalNodeCount++;
        //}

        public void Add(TKey key, TValue value)
        {
            if (count >= backingArray.Length)
            {
                ReHash();
            }

            int index = Math.Abs(key.GetHashCode());
            index %= backingArray.Length;
            if (backingArray[index] == null)
            {
                backingArray[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }
            backingArray[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));
            count++;
            //Possible Expansion
        }

        public void ReHash()
        {
            int newCount = backingArray.Length * 2;

            LinkedList<KeyValuePair<TKey, TValue>>[] expanded = new LinkedList<KeyValuePair<TKey, TValue>>[newCount];

            for (int i = 0; i < backingArray.Length; i++)
            {
                if (backingArray[i] == null) continue;

                foreach (KeyValuePair<TKey, TValue> val in backingArray[i])
                {

                    int index = Math.Abs(val.GetHashCode());
                    index %= backingArray.Length;
                    if (expanded[index] == null)
                    {
                        expanded[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
                    }
                    backingArray[index].AddLast(new KeyValuePair<TKey, TValue>(val.Key, val.Value));
                }

            }

            backingArray = expanded;
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key, TValue value)
        {
            int index = Math.Abs(key.GetHashCode());
            index %= backingArray.Length;

            if (!backingArray[index].Contains(new KeyValuePair<TKey, TValue>(key, value))) return false;

            backingArray[index].Remove(new KeyValuePair<TKey, TValue>(key, value));
            return true;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TKey key, TValue value)
        {
            int index = Math.Abs(key.GetHashCode());
            index %= backingArray.Length;

            if (backingArray[index].Contains(new KeyValuePair<TKey, TValue>(key, value))) return true;
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Contains(item.Key, item.Value);
        }
    }
}
