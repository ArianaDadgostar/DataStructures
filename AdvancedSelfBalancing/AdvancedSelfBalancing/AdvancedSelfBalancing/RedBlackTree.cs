using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSelfBalancing
{
    public class RedBlackNode<T> where T : IComparable<T>
    {
        public T value;
        public RedBlackNode<T> left;
        public RedBlackNode<T> right;
        public RedBlackNode<T> parent;
        public bool isRed;
        public RedBlackNode(T val)
        {
            value = val;
            left = null;
            right = null;
            parent = null;
            isRed = false;
        }

        public RedBlackNode()
        {
            isRed = false;
        }
    }

    public class RedBlackTree<T> : ISortedSet<T> where T : IComparable<T>
    {
        public RedBlackNode<T> Head;

        public IComparer<T> Comparer => throw new NotImplementedException();

        public int Count { get; } = 0;

        #region InsertAid

        public void FlipColor(RedBlackNode<T> node)
        {
            node.isRed = !node.isRed;

            if (node.left != null)
            {
                node.left.isRed = !node.left.isRed;
            }

            if (node.right == null) return;

            node.right.isRed = !node.right.isRed;
        }

        void parentLessRotate(RedBlackNode<T> node, bool left)
        {
            if(left)
            {
                if(node.right == null) return;

                RedBlackNode<T> replacement = node.right.left;
                node.right.left = node;
                node.right = replacement;
                
                return;
            }
            if(node.left == null) return;

            RedBlackNode<T> lReplacement = node.left.right;
            node.left.right = node;
            node.left = lReplacement;
        }

        public void RotateLeft(RedBlackNode<T> node)
        {
            if(node.parent == null)
            {
                parentLessRotate(node, true);
                return;
            }

            RedBlackNode<T> newParent = node.parent.parent;
            if (node.left != null)
            {
                node.parent.right = node.left;
                node.left.parent = node.parent;
            }
            else
            {
                node.parent.right = null;
            }

            node.left = node.parent;
            node.left.parent = node;
            node.parent = newParent;

            bool oldParentIsRed = node.isRed;
            node.isRed = node.left.isRed;
            node.left.isRed = oldParentIsRed;
        }

        public void RotateRight(RedBlackNode<T> node)
        {
            if(node.parent == null)
            {
                parentLessRotate(node, false);
                return;
            }

            RedBlackNode<T> newParent = node.parent.parent;
            if (node.right != null)
            {
                node.parent.left = node.right;
                node.right.parent = node.parent;
            }
            else
            {
                node.parent.left = null;
            }
            node.right = node.parent;
            node.right.parent = node;
            node.parent = newParent;

            bool oldParentIsRed = node.isRed;
            node.isRed = node.right.isRed;
            node.right.isRed = oldParentIsRed;
        }

        #endregion

        public RedBlackNode<T> Insert(T value, RedBlackNode<T> node)
        {
            if (value.Equals(default(T))) return node;

            if (node == null)
            {
                node = new RedBlackNode<T>(value);
                return node;
            }

            if (value.Equals(node.value)) return node;

            if(node.left != null && node.left.isRed && node.right != null && node.right.isRed)
            {
                FlipColor(node);
            }

            if (node.value.CompareTo(value) > 0)
            {
                node.left = Insert(value, node.left);
                node.left.parent = node;
            }
            else if (node.value.CompareTo(value) < 0)
            {
                node.right = Insert(value, node.right);
                node.right.parent = node;
            }
            else
            {
                return node;
            }

            if (node.left != null && node.left.isRed) return node;

            if(node.right != null && node.right.isRed)
            {
                RotateLeft(node);
            }

            return node;
        }

        public void MoveRedRight(RedBlackNode<T> node)
        {
            FlipColor(node);
            if (!node.isRed || node.left == null || !node.left.isRed) return;

            RotateRight(node);
        }

        public void MoveRedLeft(RedBlackNode<T> node)
        {
            FlipColor(node);
            if (!node.isRed || node.right == null || !node.right.isRed) return;

            RotateLeft(node);
        }

        public RedBlackNode<T> PhysicalRemoval(RedBlackNode<T> node)
        {
            RedBlackNode<T> replacement = null;
            if(node.left != null && node.right != null)
            {
                replacement = node.right;
                while(replacement.left != null)
                {
                    replacement = replacement.left;
                }

                replacement.parent.left = replacement.right;
            }
            else if(node.left != null)
            {
                replacement = node.left;
            }
            else if(node.right != null)
            {
                replacement = node.right;
            }
            else
            {
                node = null;
                return node;
            }

            if (node.parent != null && node == node.parent.left)
            {
                node.parent.left = replacement;
            }
            else if(node.parent != null)
            {
                node.parent.right = replacement;
            }
            else
            {
                node = replacement;
            }

            replacement.left = node.left;
            if (replacement.right != null) return node;

            replacement.right = node.right;
            return node;
        }

        public void FixUp(RedBlackNode<T> node)
        {
            if((node.left == null || !node.left.isRed) && node.right != null && node.right.isRed)
            {
                RotateLeft(node);
            }

            if(node.left == null || !node.left.isRed || node.right == null || !node.right.isRed) return;
            
            FlipColor(node);
        }

        public RedBlackNode<T> Remove(T value, RedBlackNode<T> node)
        {
            if(node == null) return node;
            if(node.left != null && node.left.isRed)
            {
                RotateRight(node);
            }

            if(value.CompareTo(node.value) < 0)
            {
                if (node.left == null) return null;
                
                if(!node.left.isRed && node.left.left != null && !node.left.left.isRed)
                {
                    MoveRedLeft(node);
                }

                node.left = Remove(value, node.left);
                FixUp(node);
            }

            if (value.CompareTo(node.value) >= 0)
            {
                if (node.right == null) return null;

                if(!node.right.isRed && node.left != null && !node.left.isRed)
                {
                    MoveRedRight(node);
                }

                if(value.CompareTo(node.value) == 0)
                {
                    node = PhysicalRemoval(node);
                    return node;
                }

                node.right = Remove(value, node.right);
                FixUp(node);
            }

            return node;
        }

        public RedBlackNode<T> Find(T value, RedBlackNode<T> node)
        {
            if (node == null) return null;
            if (value.Equals(node.value)) return node;

            if (node.left != null)
            {
                return Find(value, node.left);
            }

            if (node.right != null)
            {
                return Find(value, node.right);
            }

            return null;
        }
        public void InOrderTransversalRecursive(RedBlackNode<T> curr, ref Queue<T> result)
        {
            if (curr == null) return;

            // do this stuff and check null so no if statements
            InOrderTransversalRecursive(curr.left, ref result);

            result.Enqueue(curr.value);

            InOrderTransversalRecursive(curr.right, ref result);
        }

        #region Testing

        public bool ColorTesting(RedBlackNode<T> current)
        {
            if (current == null) return true;

            if (current.right != null && current.right.isRed && (current.left == null || !current.left.isRed)) return false;

            if(!ColorTesting(current.right)) return false;

            if (!ColorTesting(current.left)) return false;

            return true;
        }

        public bool Search(T value, RedBlackNode<T> node)
        {
            if (node == null) return false;
            if (value.Equals(node.value)) return true;

            if(node.left != null)
            {
                if (Search(value, node.left)) return true;
            }

            if (node.right != null)
            {
                if (Search(value, node.right)) return true;
            }

            return false;
        }

        #endregion

        public void Clear()
        {
            Head = null;
        }

        public bool Add(T item)
        {
            Head = Insert(item, Head);
            if (Search(item, Head)) return true;

            return false;
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach(T item in items)
            {
                Head = Insert(item, Head);
            }
        }

        public bool Contains(T item)
        {
            return Search(item, Head);
        }

        public bool Remove(T item)
        {
            return Remove(item);
        }

        public T Max()
        {
            RedBlackNode<T> node = Head;
            while(node.right != null)
            {
                node = node.right;
            }

            return node.value;
        }

        public T Min()
        {
            RedBlackNode<T> node = Head;
            while(node.left != null)
            {
                node = node.left;
            }

            return node.value;
        }

        public T Ceiling(T item)
        {
            Queue<T> queue = new Queue<T>();
            InOrderTransversalRecursive(Head, ref queue);

            while(!queue.Peek().Equals(item))
            {
                if (queue.Peek().CompareTo(item) > 0) break;

                queue.Dequeue();
            }

            return queue.Peek();
        }

        public T Floor(T item)
        {
            Queue<T> queue = new Queue<T>();
            InOrderTransversalRecursive(Head, ref queue);

            T previous = default(T);

            while (!previous.Equals(item))
            {
                if (queue.Peek().CompareTo(item) > 0) break;

                previous = queue.Dequeue();
            }

            return previous;
        }

        public ISortedSet<T> Union(ISortedSet<T> other)
        {
            foreach(T item in other)
            {
                Head = Insert(item, Head);
            }

            return this;
        }

        public ISortedSet<T> Intersection(ISortedSet<T> other)
        {
            RedBlackTree<T> intersection = new RedBlackTree<T>();
            foreach(T item in other)
            {
                if(!Search(item, Head)) continue;

                intersection.Head = intersection.Insert(item, intersection.Head);
            }

            return intersection;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
