using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace AdvancedSelfBalancing
{
    public class Node<T>
    {
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public T value { get; set; }

        public Node(T value)
        {
            this.value = value;
        }
    }

    public class Tree<T> where T : IComparable<T>
    {
        public Node<T> Root { get; set; }
        public int length { get; set; }
        public int Count { get; set; }

        public Tree()
        {
            length = 0;
        }

        public Node<T> Search(T value)
        {
            Node<T> tester = Root;

            while (tester != null)
            {
                if (value.CompareTo(tester.value) > 0)
                {
                    tester = tester.Right;
                }

                else if (value.CompareTo(tester.value) < 0)
                {
                    tester = tester.Left;
                }

                else if (value.Equals(tester.value))
                {
                    return tester;
                }
            }

            return null;
        }

        public bool Contains(T value)
        {
            Node<T> result = Search(value);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public T Minimum(Node<T> node)
        {
            Node<T> tester = node;
            while (tester.Left != null)
            {
                tester = tester.Left;
            }
            return tester.value;
        }

        public T Maximum(Node<T> node)
        {
            Node<T> tester = node;
            while (tester.Right != null)
            {
                tester = tester.Right;
            }
            return tester.value;
        }

        public bool Remove(T value)
        {
            Node<T> theRoot = Root;
            Node<T> tester = Root;
            bool onLeft = false;
            while (tester.value.CompareTo(value) != 0)
            {
                if (value.CompareTo(tester.value) > 0)
                {
                    theRoot = tester;
                    tester = tester.Right;
                    onLeft = false;
                }
                else if (value.CompareTo(tester.value) < 0)
                {
                    theRoot = tester;
                    tester = tester.Left;
                    onLeft = true;
                }

                if (tester.value == null)
                {
                    return false;
                }
            }
            RemoveNode(theRoot, tester, onLeft);
            return true;
        }

        public void RemoveNode(Node<T> theRoot, Node<T> removed, bool onLeft)
        {
            if (removed.Right == null && removed.Left == null)
            {
                if (onLeft)
                {
                    theRoot.Left = null;
                    return;
                }
                theRoot.Right = null;
            }
            else if (removed.Left == null && removed.Right != null)
            {
                if (onLeft)
                {
                    theRoot.Left = removed.Right;
                    return;
                }
                theRoot.Right = removed.Right;
            }
            else if (removed.Right == null && removed.Left != null)
            {
                if (onLeft)
                {
                    theRoot.Left = removed.Left;
                    return;
                }
                theRoot.Right = removed.Left;
            }
            else if (removed.Right != null && removed.Left != null)
            {
                Node<T> testingRoot = removed;
                Node<T> tester = removed.Left;
                while (tester.Right != null)
                {
                    testingRoot = tester;
                    tester = tester.Right;
                }
                if(testingRoot == removed)
                {
                    removed.value = tester.value;
                    removed.Left = tester.Left;
                    return;
                }
                removed.value = tester.value;
                testingRoot.Right = tester.Left;
            }
        }
    }

    public abstract class BurstNode
    {
        internal BurstTrie ParentTrie { get; set; }

        public abstract BurstNode Insert(string value, int index);
        public abstract BurstNode? Remove(string value, int index);
        //public abstract BurstNode? Search(string value, int index);
        //internal abstract void GetAll(List<string> output);
    }

    public class ContainerNode : BurstNode
    {
        public Tree<string> BST { get; set; }

        public ContainerNode()
        {
            BST = new Tree<string>();
        }

        private Node<string> Traverse(string value, int index, Node<string> current)
        {
            if (current == null)
            {
                return new Node<string>(value);
            }
            else if(value == current.value) return current;

            else if (value.Length <= index)
            {
                current.Left = Traverse(value, index, current.Left);
            }

            else if (current.value.Length <= index || value[index] > current.value[index])
            {
                current.Right = Traverse(value, index, current.Right);
            }
            else if (value[index] < current.value[index])
            {
                current.Left = Traverse(value, index, current.Left);
            }
            else if (value[index] == current.value[index])
            {
                current = Traverse(value, index + 1, current);
            }

            return current;
        }

        public override BurstNode Insert(string value, int index)
        {
            BST.Root = Traverse(value, index, BST.Root);

            return this;
        }

        public override BurstNode? Remove(string value, int index)
        {
            BST.Remove(value);
            return this;
        }
    }

    public class InternalNode : BurstNode
    {
        ContainerNode[] Children { get; set; }

        public InternalNode(int childrenSize)
        {
            Children = new ContainerNode[childrenSize];
        }

        public override BurstNode Insert(string value, int index)
        {
            int childIndex = value[index] - 'a';

            if (Children[childIndex] == null)
            {
                Children[childIndex] = new ContainerNode();
            }

            Children[childIndex].Insert(value, index);

            return this;
        }

        public override BurstNode? Remove(string value, int index)
        {
            int childIndex = value[index] - 'a';

            Children[childIndex].Remove(value, index);

            return this;
        }
    }

    public class BurstTrie
    {
        BurstNode Head { get; set; }


    }
}
