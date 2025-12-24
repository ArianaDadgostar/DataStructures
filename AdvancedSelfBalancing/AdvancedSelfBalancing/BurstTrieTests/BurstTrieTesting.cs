using AdvancedSelfBalancing;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BurstTrieTests
{
    //check type of the burst node example
    /*
    BurstNode example = new InternalNode(67);
            
    Assert.True(example.GetType() == typeof(ContainerNode));
    Assert.True(example is ContainerNode);
    */

    public class BurstTrieTesting
    {
        [Theory]
        [InlineData("ad", "ac", "a", "ga", "le")]
        [InlineData("dea", "der", "dhf", "gim", "too")]
        public void BurstTrieInsertTest(params string[] array)
        {            
            BurstTrie burstTrie = new BurstTrie();
            

            foreach (var item in array)
            {
                burstTrie.Insert(item);            
            }

            burstTrie.Insert("a");

            Assert.True(burstTrie.Head is InternalNode);
        
            ;
        }

        [Theory]
        [InlineData("ad", "ac", "a", "ga")]
        [InlineData("dea", "der", "dhf", "gim")]
        public void BurstTrieRemoveTest(params string[] array)
        {
            BurstTrie trie = new BurstTrie();

            foreach (var item in array)
            {
                trie.Insert(item);
            }

            foreach (var item in array)
            {
                bool result = trie.Remove(item);

                Assert.True(trie.Search(item) == null && result);
            }
        }

        [Theory]
        [InlineData("ad", "ac", "after", "ga")]
        [InlineData("dea", "der", "dhf", "gim")]
        public void BurstTrieSearchTest(params string[] array)
        {
            BurstTrie trie = new BurstTrie();

            foreach (var item in array)
            {
                trie.Insert(item);
            }

            foreach (var item in array)
            {
                Node<string> result = trie.Search(item);
                Assert.True(result.value == item);
            }

        }

        [Theory]
        [InlineData("ad", "ac", "a", "ga")]
        [InlineData("dea", "der", "dhf", "gim")]
        public void BurstTrieGetAllTest(params string[] array)
        {
            BurstTrie trie = new BurstTrie();

            foreach (var item in array)
            {
                trie.Insert(item);
            }

            List<string> output1 = new List<string>();
            output1 = trie.GetAll(output1);
            
            string previous = "";
            foreach (var item in output1)
            {
                Assert.True(item.CompareTo(previous) > 0);
                previous = item;
            }
        }


        [Theory]
        [InlineData("ad", "ac", "agatha", "ga", "car")]
        [InlineData("dea", "der", "dhf", "gim", "dar")]
        public void BurstTrieRestructureTest(params string[] array)
        {
            BurstTrie node = new BurstTrie();
            InternalNode internalNode = new InternalNode(26);

            foreach (var item in array)
            {
                node.Insert(item);
                bool result = false;
                internalNode.Insert(item, 0, ref result);
            }

            List<string> nodeOutput = new List<string>();
            List<string> internalOutput = new List<string>();

            Assert.True(node.Head is InternalNode);
            
            nodeOutput = node.Head.GetAll(nodeOutput);
            internalOutput = internalNode.GetAll(internalOutput);

            for (int i = 0; i < nodeOutput.Count; i++)
            {
                Assert.True(nodeOutput[i] == internalOutput[i]);
            }
        }
    }
}