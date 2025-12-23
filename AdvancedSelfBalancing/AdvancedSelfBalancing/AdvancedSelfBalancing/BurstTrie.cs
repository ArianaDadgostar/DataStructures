using System.Net.Http.Headers;
using System.Reflection.Metadata;
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

        public Queue<T> InOrderTransversalRecursive(Node<T> curr, Queue<T> result)
        {
            if (curr == null) return result;

            // do this stuff and check null so no if statements
            InOrderTransversalRecursive(curr.Left, result);

            result.Enqueue(curr.value);

            InOrderTransversalRecursive(curr.Right, result);

            return result;
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
            Count--;
            return true;
        }

        public void RemoveNode(Node<T> theRoot, Node<T> removed, bool onLeft)
        {
            if (removed.Right == null && removed.Left == null)
            {
                if(theRoot == Root)
                {
                    Root = null;
                    return;
                }
                if (onLeft)
                {
                    theRoot.Left = null;
                    return;
                }
                theRoot.Right = null;
            }
            else if (removed.Left == null && removed.Right != null)
            {
                if(theRoot == Root)
                {
                    Root = removed.Right;
                    return;
                }
                if (onLeft)
                {
                    theRoot.Left = removed.Right;
                    return;
                }
                theRoot.Right = removed.Right;
            }
            else if (removed.Right == null && removed.Left != null)
            {
                if (theRoot == Root)
                {
                    Root = removed.Left;
                    return;
                }
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
        public abstract Node<string>? Search(string prefix, int index);
        public abstract List<string> GetAll(List<string> output);
    }

    public class ContainerNode : BurstNode
    {
        public Tree<string> BST { get; set; }

        public ContainerNode()
        {
            BST = new Tree<string>();
        }

        private Node<string> Traverse(string value, int index, Node<string> current, bool isPrefix)
        {
            if (current == null)
            {
                return new Node<string>(value);
                BST.Count++;
            }
            else if(value == current.value) return current;

            else if (value.Length <= index)
            {
                if (isPrefix) return current;
                current.Left = Traverse(value, index, current.Left, isPrefix);
            }

            else if (current.value.Length <= index || value[index] > current.value[index])
            {
                current.Right = Traverse(value, index, current.Right, isPrefix);
            }
            else if (value[index] < current.value[index])
            {
                current.Left = Traverse(value, index, current.Left, isPrefix);
            }
            else if (value[index] == current.value[index])
            {
                current = Traverse(value, index + 1, current, isPrefix);
            }

            return current;
        }

        public override BurstNode Insert(string value, int index)
        {
            BST.Root = Traverse(value, index, BST.Root, false);

            BurstNode node = BurstTrie.CheckForSize(this);
            if (node != null) return node;

            return this;
        }

        public override BurstNode? Remove(string value, int index)
        {
            BST.Remove(value);
            return this;
        }

        public override Node<string>? Search(string prefix, int index)
        {
            return Traverse(prefix, index, BST.Root, true);
        }

        public override List<string> GetAll(List<string> output)
        {
            output = BST.InOrderTransversalRecursive(BST.Root, new Queue<string>(output)).ToList();

            return output;
        }
    }

    public class InternalNode : BurstNode
    {
        BurstNode[] Children { get; set; }

        public InternalNode(int childrenSize)
        {
            Children = new BurstNode[childrenSize];
        }

        public override BurstNode Insert(string value, int index)
        {
            int childIndex = value[index] - 'a';

            if (Children[childIndex] == null)
            {
                Children[childIndex] = new ContainerNode();
            }

            Children[childIndex] = Children[childIndex].Insert(value, index);

            return this;
        }

        public override BurstNode? Remove(string value, int index)
        {
            int childIndex = value[index] - 'a';

            Children[childIndex].Remove(value, index);

            return this;
        }

        public override Node<string>? Search(string prefix, int index)
        {
            int childIndex = prefix[index] - 'a';

            return Children[childIndex].Search(prefix, index);
        }

        public override List<string> GetAll(List<string> output)
        {
            foreach (var child in Children)
            {
                if (child == null) continue;

                output = child.GetAll(output);
            }

            return output;
        }
    }

    public class BurstTrie
    {
        const int MAXBST = 5;
        public BurstNode Head { get; set; }

        public static BurstNode CheckForSize(ContainerNode node)
        {
            if (node.BST.Count <= MAXBST) return node;

            InternalNode newInternal = new InternalNode(26);
            Queue<string> allValues = node.BST.InOrderTransversalRecursive(node.BST.Root, new Queue<string>());
            while (allValues.Count > 0)
            {
                string value = allValues.Dequeue();
                newInternal.Insert(value, 0);
            }
            return newInternal;
        }

        public BurstTrie()
        {
            Head = new ContainerNode();
        }

        public void Insert(string value)
        {
            Head = Head.Insert(value, 0);
        }
    }
}
