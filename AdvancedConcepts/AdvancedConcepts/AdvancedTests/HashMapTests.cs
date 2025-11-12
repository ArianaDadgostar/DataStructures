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
            HashMap<string,string> map = new HashMap<string,string>();
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


    }
}