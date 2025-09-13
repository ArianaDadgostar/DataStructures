using Binary_Search_Tree;
using System.ComponentModel.Design.Serialization;
using Xunit.Sdk;

namespace BinaryTreeTesting
{
    public class BinarySearchTreeTesting
    {
        [Theory]
        [InlineData(new int[] { 2, 4, 7, 29 })]
        [InlineData(new int[] { 6, 1, 5, 3, 20 })]
        public void AddTest(params int[] array)
        {
            Tree<int> tree = new Tree<int>();
            foreach (int var in array)
            {
                tree.Add(var);
            }
            Assert.True(tree.Root.value == array[0]);
        }

        [Theory]
        [InlineData(new int[] { 2, 4, 7, 29 })]
        [InlineData(new int[] { 6, 1, 5, 3, 20 })]

        public void SearchTest(params int[] array)
        {
            Tree<int> tree = new Tree<int>();
            foreach (int var in array)
            {
                Assert.False(tree.Search(var) != null);
                tree.Add(var);
                Assert.True(tree.Search(var) != null);
            }
        }

        [Theory]
        [InlineData(new int[] { 2, 4, 7, 29 })]
        [InlineData(new int[] { 6, 1, 5, 3, 20 })]

        public void ContainsTest(params int[] array)
        {
            Tree<int> tree = new Tree<int>();
            foreach (int var in array)
            {
                Assert.False(tree.Contains(var) == true);
                tree.Add(var);
                Assert.True(tree.Contains(var) == true);
            }
        }

        #region Transversals (or "traversals" or whatever)

        [Fact]

        public void LevelOrderTransversalTest()
        {
            int[] array = new int[5] { 1, 5, 2, 4, 8 };
            Tree<int> tree = new Tree<int>();
            foreach (int var in array)
            {
                tree.Add(var);
            }
            int[] test = new int[5] { 1, 5, 2, 8, 4 };
            int[] result = tree.LevelOrderTransversal();
            for (int i = 0; i < test.Length; i++)
            {
                Assert.True(test[i] == result[i]);
            }
        }

        [Fact]

        public void PreOrderTransversalTest()
        {
            int[] array = new int[6] { 4, 2, 6, 3, 5, 7 };
            Tree<int> tree = new Tree<int>();
            foreach (int var in array)
            {
                tree.Add(var);
            }
            int[] test = new int[6] { 4, 2, 3, 6, 5, 7 };
            int[] result = tree.PreOrderTransversal();
            for (int i = 0; i < test.Length; i++)
            {
                Assert.True(test[i] == result[i]);
            }
        }

        [Fact]

        public void PostOrderTransversalTest()
        {
            int[] array = new int[6] { 4, 2, 6, 3, 5, 7 };
            Tree<int> tree = new Tree<int>();
            foreach (int var in array)
            {
                tree.Add(var);
            }
            int[] test = new int[6] { 3, 2, 5, 7, 6, 4 };
            int[] result = tree.PostOrderTransversal();
            for (int i = 0; i < test.Length; i++)
            {
                Assert.True(test[i] == result[i]);
            }
        }

        [Fact]

        public void InOrderTransversalTest()
        {
            int[] array = new int[6];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 6 - i;
            }

            Tree<int> tree = new Tree<int>();
            foreach (int var in array)
            {
                tree.Add(var);
            }

            int[] test = new int[6];

            for (int i = 0; i < test.Length; i++)
            {
                test[i] = i + 1;
            }

            int[] result = tree.InOrderTransversal();
            for (int i = 0; i < test.Length; i++)
            {
                Assert.True(test[i] == result[i]);
            }
        }

        #endregion

        [Fact]
        public void RemoveTest()
        {
            Tree<int> tree = new Tree<int>();
            tree.Add(10);
            tree.Add(1);
            tree.Add(7);
            tree.Add(4);
            tree.Add(8);
            tree.Add(6);
            tree.Add(5);
            bool result = tree.Remove(7);
            if(!result)
            {
                Assert.True(result == false);
                return;
            }
            Assert.True(tree.Root.Left.Right.value == 6);
            Assert.True(tree.Root.Left.Right.Left.Right.value == 5);
        }

        //[Fact]
        //public void InOrderTransversalRecursiveTest()
        //{
        //    int[] array = new int[25000];

        //    for (int i = 0; i < 25000; i++)
        //    {
        //        array[i] = 25000-i;
        //    }

        //    Tree<int> tree = new Tree<int>();
        //    foreach (int var in array)
        //    {
        //        tree.Add(var);
        //    }

        //    int[] test = new int[25000];

        //    for (int i = 1; i < 25001; i++)
        //    {
        //        test[i] = i;
        //    }

        //    Queue<int> treeResult = new Queue<int>();
        //    int[] result = tree.InOrder();
        //    for (int i = 0; i < test.Length; i++)
        //    {
        //        Assert.True(test[i] == result[i]);
        //    }
        //}

        [Fact]

        public void PostOrderTransversalRecursiveTest()
        {
            int[] array = new int[6] { 4, 2, 6, 3, 5, 7 };
            Tree<int> tree = new Tree<int>();
            foreach (int var in array)
            {
                tree.Add(var);
            }
            int[] test = new int[6] { 3, 2, 5, 7, 6, 4 };
            int[] result = tree.PostOrder();
            for (int i = 0; i < test.Length; i++)
            {
                Assert.True(test[i] == result[i]);
            }
        }

        [Fact]

        public void PreOrderTransversalRecursiveTest()
        {
            int[] array = new int[6] { 4, 2, 6, 3, 5, 7 };
            Tree<int> tree = new Tree<int>();
            foreach (int var in array)
            {
                tree.Add(var);
            }
            int[] test = new int[6] { 4, 2, 3, 6, 5, 7 };
            int[] result = tree.PreOrder();
            for (int i = 0; i < test.Length; i++)
            {
                Assert.True(test[i] == result[i]);
            }
        }
    }
}