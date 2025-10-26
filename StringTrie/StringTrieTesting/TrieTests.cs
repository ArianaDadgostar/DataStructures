using StringTrie;

namespace StringTrieTesting
{
    public class TrieTests
    {
        [Fact]
        public void SearchTest()
        {
            Trie trie = new Trie();
            TrieNode val = new TrieNode('o');

            trie.Sentinal.Children.Add('o', val);
            val.Children.Add('s', new TrieNode('s'));

            Assert.True(trie.SearchNode("os").letter == 's');
        }

        [Theory]
        [InlineData( "blah", "baha", "blaho", "blahobo" )]
        [InlineData( "oo", "op", "oop", "oolala", "oolo" )]

        public void InsertAndMatchingTest(params string[] array)
        {
            Trie trie = new Trie();
            foreach(string item in array)
            {
                trie.Insert(item);
            }

            List<string> list = trie.GetAllMatchingPrefix(array[0]);
            foreach(string item in list)
            {
                Assert.True(item.Contains(array[0]));
            }
        }

        [Theory]
        [InlineData("flap", "flack", "flapo", "flop", "fand")]
        [InlineData("blob", "black", "blobo", "lala", "bloop")]

        public void RemovalTest(params string[] array)
        {
            Trie trie = new Trie();
            foreach(string item in array)
            {
                trie.Insert(item);
            }
            trie.Remove(array[2]);

            List<string> vals = trie.GetAllMatchingPrefix(array[0]);
            Assert.True(vals.Count == 1);
        }
    }
}