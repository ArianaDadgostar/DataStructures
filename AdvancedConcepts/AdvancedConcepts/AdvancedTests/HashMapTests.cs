using AdvancedConcepts;

namespace AdvancedTests
{
    public class HashMapTests
    {
        [Theory]
        [InlineData(new string[] { "a", "b", "c", "d" }, new string[] { "apple", "banana", "clover", "daphne" })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya" }, new string[] { "nah", "yah", "yaya" })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "marceda", "lalala", "yayaya" }, new string[] { "nah", "yah", "yaya", "j", "y", "g" })]
        public void AdditionMatchTest(string[] keys, params string[] values)
        {
            HashMap<string, string> map = new HashMap<string, string>();
            for (int i = 0; i < keys.Length; i++)
            {
                map.Add(keys[i], values[i]);
            }
            ;
            //for (int i = 0; i < keys.Length; i++)
            //{
            //    int index = Math.Abs(keys[i].GetHashCode());
            //    index %= map.backingArray.Length;

            ////    if (map.nodes[index].neighbor == null)
            ////    {
            ////        Assert.True(map.nodes[index].value == values[i]);
            ////        continue;
            ////    }

            ////    HashNode<string> current = map.nodes[index];

            ////    while (current != null)
            ////    {
            ////        if (current.key == keys[i])
            ////        {
            ////            Assert.True(current.value == values[i]);
            ////        }
            ////        current = current.neighbor;
            ////    }
            //}
        }

        [Theory]
        [InlineData(new string[] { "a", "b", "c", "d" }, new string[] { "apple", "banana", "clover", "daphne" })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya" }, new string[] { "nah", "yah", "yaya" })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "marceda", "lalala", "yayaya" }, new string[] { "nah", "yah", "yaya", "j", "y", "g" })]
        public void ContainsTests(string[] keys, params string[] values)
        {
            HashMap<string, string> map = new HashMap<string, string>();
            for (int i = 0; i < keys.Length; i++)
            {
                map.Add(keys[i], values[i]);
            }

            for (int i = 0; i < keys.Length; i++)
            {
                Assert.True(map.Contains(keys[i], values[i]));
            }
        }


        [Theory]
        [InlineData(new string[] { "a", "b", "c", "d" }, new int[] { 1, 2, 3, 4 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya" }, new int[] { 700, 400, 100 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "marceda", "lalala" }, new int[] { 432, 71, 53, 15, 63})]
        public void UnionFindTest(string[] keys, params int[] values)
        {
            UnionFind<string> map = new UnionFind<string>();
            for (int i = 0; i < keys.Length; i++)
            {
                map.Add(keys[i]);
            }

            for (int i = 0; i < keys.Length; i++)
            {
                Assert.True(map.Find(keys[i]) == i);
            }
        }

        [Theory]
        [InlineData(new string[] { "a", "b", "c", "d" }, new int[] { 1, 2, 3, 4 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "boi" }, new int[] { 700, 400, 100, 5 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "marceda", "lalala", "hi" }, new int[] { 432, 71, 53, 15, 63, 287 })]
        public void UnionConnectTest(string[] keys, params int[] values)
        {
            UnionFind<string> map = new UnionFind<string>();
            for (int i = 0; i < keys.Length; i++)
            {
                map.Add(keys[i]);
            }

            map.Union(keys[0], keys[1]);
            map.Union(keys[1], keys[3]);

            Assert.True(map.IsConnected(keys[0], keys[3]));
        }

        [Theory]
        [InlineData(new string[] { "a", "b", "c", "d" }, new int[] { 1, 2, 3, 4 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya" }, new int[] { 700, 400, 100 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "marceda", "lalala" }, new int[] { 432, 71, 53, 15, 63 })]
        public void QuickUnionFindTest(string[] keys, params int[] values)
        {
            QuickUnion<string> map = new QuickUnion<string>();
            for (int i = 0; i < keys.Length; i++)
            {
                map.Add(keys[i]);
            }

            for (int i = 0; i < keys.Length; i++)
            {
                Assert.True(map.Find(keys[i]) == i);
            }
        }

        [Theory]
        [InlineData(new string[] { "a", "b", "c", "d" }, new int[] { 1, 2, 3, 4 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "boi" }, new int[] { 700, 400, 100, 5 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "marceda", "lalala", "hi" }, new int[] { 432, 71, 53, 15, 63, 287 })]
        public void QuickUnionConnectTest(string[] keys, params int[] values)
        {
            QuickUnion<string> map = new QuickUnion<string>();
            for (int i = 0; i < keys.Length; i++)
            {
                map.Add(keys[i]);
            }

            map.Union(keys[0], keys[1]);
            map.Union(keys[1], keys[3]);

            Assert.True(map.IsConnected(keys[0], keys[3]));
        }

        [Theory]
        [InlineData(new string[] { "a", "b", "c", "d" }, new int[] { 1, 2, 3, 4 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "boi" }, new int[] { 700, 400, 100, 5 })]
        [InlineData(new string[] { "abra", "cadabra", "yayaya", "marceda", "lalala", "hi" }, new int[] { 432, 71, 53, 15, 63, 287 })]
        public void BloomFilterTest(string[] keys, params int[] values)
        {
            BloomFilter<string> map = new BloomFilter<string>(100);
            for (int i = 0; i < keys.Length; i++)
            {
                map.Add(keys[i]);
            }

            for (int i = 0; i < keys.Length; i++)
            {
                Assert.True(map.ProbablyContains(keys[i]));
            }
        }
    }
}