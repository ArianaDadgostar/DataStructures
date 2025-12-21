using AdvancedSelfBalancing;
using System.ComponentModel;

namespace BurstTrieTests
{
    public class BurstTrieTesting
    {
        [Theory]
        [InlineData("ad", "ac", "a", "ga")]
        [InlineData("dea", "der", "dhf", "gim")]
        public void BurstTrieInsertTest(params string[] array)
        {
            ContainerNode node = new ContainerNode();
            InternalNode internalNode = new InternalNode(26);
            
            foreach (var item in array)
            {
                node.Insert(item, 0);
                internalNode.Insert(item, 0);
            }

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
    }
}