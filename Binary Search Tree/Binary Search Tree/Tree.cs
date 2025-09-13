namespace Binary_Search_Tree
{
    public class Tree<T> where T : IComparable<T>
    {
        public Node<T> Root { get; set; }
        public int length { get; set; }
        public int Count { get; set; }

        public Tree()
        {
            length = 0;
        }

        public void Add(T value)
        {
            if (Root == null)
            {
                Root = new Node<T>(value);
                length = 1;
                return;
            }

            Node<T> theRoot = Root;
            Node<T> tester = Root;
            bool onLeft = false;

            while (tester != null)
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
                else
                {
                    return;
                }
            }

            if (onLeft)
            {
                theRoot.Left = new Node<T>(value);
            }
            else
            {
                theRoot.Right = new Node<T>(value);
            }
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

        public T[] LevelOrderTransversal() //fix
        {
            Queue<Node<T>> storage = new Queue<Node<T>>();
            Queue<T> result = new Queue<T>();
            storage.Enqueue(Root);
            while (storage.Count != 0)
            {
                var curr = storage.Dequeue();
                result.Enqueue(curr.value);

                if (curr.Left != null)
                {
                    storage.Enqueue(curr.Left);
                }
                if (curr.Right != null)
                {
                    storage.Enqueue(curr.Right);
                }
            }
            return result.ToArray();
        }

        public T[] PreOrderTransversal() //fix
        {
            Stack<Node<T>> storage = new Stack<Node<T>>();
            Queue<T> result = new Queue<T>();
            storage.Push(Root);
            while (storage.Count != 0)
            {
                var curr = storage.Pop();
                result.Enqueue(curr.value);

                if (curr.Right != null)
                {
                    storage.Push(curr.Right);
                }
                if (curr.Left != null)
                {
                    storage.Push(curr.Left);
                }
            }
            return result.ToArray();
        }

        public T[] PreOrder()
        {
            Queue<T> result = new Queue<T>();
            PreOrderTransversalRecursive(Root, result);

            return result.ToArray();
        }

        public void PreOrderTransversalRecursive(Node<T> curr, Queue<T> result)
        {
            if (curr == null) return;

            result.Enqueue(curr.value);

            PreOrderTransversalRecursive(curr.Left, result);

            PreOrderTransversalRecursive(curr.Right, result);
        }

        public T[] PostOrderTransversal() //fix
        {
            Stack<Node<T>> storage = new Stack<Node<T>>();
            Stack<T> result = new Stack<T>();
            storage.Push(Root);
            while (storage.Count != 0)
            {
                var curr = storage.Pop();
                result.Push(curr.value);

                if (curr.Left != null)
                {
                    storage.Push(curr.Left);
                }
                if (curr.Right != null)
                {
                    storage.Push(curr.Right);
                }
            }
            return result.ToArray();
        }

        public T[] PostOrder()
        {
            Stack<T> result = new Stack<T>();
            PostOrderTransversalRecursive(Root, result);

            return result.ToArray();
        }

        public void PostOrderTransversalRecursive(Node<T> curr, Stack<T> result)
        {
            if (curr == null) return;

            result.Push(curr.value);

            PostOrderTransversalRecursive(curr.Right, result);

            PostOrderTransversalRecursive(curr.Left, result);
        }

        public T[] InOrderTransversal()
        {
            Stack<Node<T>> storage = new Stack<Node<T>>();
            Queue<T> result = new Queue<T>();
            storage.Push(Root);
            Node<T> curr = Root;
            bool canGoLeft = true;
            while (storage.Count != 0)
            {
                if (curr.Left != null && canGoLeft == true)
                {
                    storage.Push(curr);
                    curr = curr.Left;
                    continue;
                }
                else if (curr.Right != null)
                {
                    result.Enqueue(curr.value);
                    curr = curr.Right;
                    canGoLeft = true;
                    continue;
                }

                canGoLeft = false;
                result.Enqueue(curr.value);
                curr = storage.Pop();
            }
            return result.ToArray();
        }

        public T[] InOrder()
        {
            Queue<T> queue = new Queue<T>();
            InOrderTransversalRecursive(Root, queue);

            return queue.ToArray();
        }

        public void InOrderTransversalRecursive(Node<T> curr, Queue<T> result)
        {
            if (curr == null) return;

            // do this stuff and check null so no if statements
            InOrderTransversalRecursive(curr.Left, result);

            result.Enqueue(curr.value);

            InOrderTransversalRecursive(curr.Right, result);
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
                }
                else
                {
                    theRoot.Right = null;
                }
            }
            else if (removed.Left == null && removed.Right != null)
            {
                if (onLeft)
                {
                    theRoot.Left = removed.Right;
                }
                else
                {
                    theRoot.Right = removed.Right;
                }
            }
            else if (removed.Right == null && removed.Left != null)
            {
                if (onLeft)
                {
                    theRoot.Left = removed.Left;
                }
                else
                {
                    theRoot.Right = removed.Left;
                }
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
                removed.value = tester.value;
                testingRoot.Right = tester.Left;
            }
        }
    }
}
