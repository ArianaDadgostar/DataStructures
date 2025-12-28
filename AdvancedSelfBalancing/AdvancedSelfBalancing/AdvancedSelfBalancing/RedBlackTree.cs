using System;
using System.Collections.Generic;
using System.Linq;
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
    }

    public class RedBlackTree<T> where T : IComparable<T>
    {
        RedBlackNode<T> Head;

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

        public RedBlackNode<T> Insert(T value, RedBlackNode<T> node, bool resizing)
        {
            if (value.Equals(default(T))) return node;
            if (value.Equals(node.value)) return node;
            if (node == null)
            {
                node = new RedBlackNode<T>(value);
                return node;
            }

            if (node.value.CompareTo(value) > 0)
            {
                node.left = Insert(value, node.left, resizing);
                node.left.parent = node;
            }
            else if (node.value.CompareTo(value) < 0)
            {
                node.right = Insert(value, node.right, resizing);
                node.right.parent = node;
            }
            else
            {
                return node;
            }
        }
    }
}
