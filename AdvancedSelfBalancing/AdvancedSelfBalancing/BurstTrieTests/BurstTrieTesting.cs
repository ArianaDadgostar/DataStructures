using AdvancedSelfBalancing;
using System.ComponentModel;

namespace BurstTrieTests
{
    public class BurstTrieTesting
    {
        [Theory]
        [InlineData("ad", "ac", "a", "ga", "le")]
        [InlineData("dea", "der", "dhf", "gim", "too")]
        public void BurstTrieInsertTest(params string[] array)
        {            
            BurstTrie node = new BurstTrie();
            InternalNode internalNode = new InternalNode(26);

            //check type of the burst node example
            /*
            BurstNode example = new InternalNode(67);
            
            Assert.True(example.GetType() == typeof(ContainerNode));
            Assert.True(example is ContainerNode);
            */

            foreach (var item in array)
            {
                node.Insert(item);
                internalNode.Insert(item, 0);
            }

            Assert.True(node.Head is InternalNode);

            ;
        }

        [Theory]
        [InlineData("ad", "ac", "a", "ga")]
        [InlineData("dea", "der", "dhf", "gim")]
        public void BurstTrieRemoveTest(params string[] array)
        {
            ContainerNode node = new ContainerNode();
            InternalNode internalNode = new InternalNode(26);

            foreach (var item in array)
            {
                node.Insert(item, 0);
                internalNode.Insert(item, 0);
            }

            foreach (var item in array)
            {
                node.Remove(item, 0);
                internalNode.Remove(item, 0);

                ;
            }

        }

        [Theory]
        [InlineData("ad", "ac", "a", "ga")]
        [InlineData("dea", "der", "dhf", "gim")]
        public void BurstTrieSearchTest(params string[] array)
        {
            ContainerNode node = new ContainerNode();
            InternalNode internalNode = new InternalNode(26);

            foreach (var item in array)
            {
                node.Insert(item, 0);
                internalNode.Insert(item, 0);
            }

            foreach (var item in array)
            {
                Node<string> result = node.Search(item, 0);
                Node<string> resultAlso = internalNode.Search(item, 0);

                ;
            }

        }

        [Theory]
        [InlineData("ad", "ac", "a", "ga")]
        [InlineData("dea", "der", "dhf", "gim")]
        public void BurstTrieGetAllTest(params string[] array)
        {
            ContainerNode node = new ContainerNode();
            InternalNode internalNode = new InternalNode(26);

            foreach (var item in array)
            {
                node.Insert(item, 0);
                internalNode.Insert(item, 0);
            }

            List<string> output1 = new List<string>();
            output1 = node.GetAll(output1);
            List<string> output2 = new List<string>();
            output2 = internalNode.GetAll(output2);

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
        public void BurstTrieCheckSizeTest(params string[] array)
        {
            ContainerNode node = new ContainerNode();
            InternalNode internalNode = new InternalNode(26);
            BurstNode result;

            foreach (var item in array)
            {
                result = node.Insert(item, 0);
                internalNode.Insert(item, 0);
            }

            //Assert.True(result == );
        }
    }
}