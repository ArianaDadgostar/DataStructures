using System;
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
    }

    public class RedBlackTree<T> where T : IComparable<T>
    {
        public RedBlackNode<T> Head;

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

        public void RotateLeft(RedBlackNode<T> node)
        {
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

        public void PhysicalRemoval(RedBlackNode<T> node)
        {
            RedBlackNode<T> replacement = null;
            if(node.left != null && node.right != null)
            {
                replacement = node.right;
                while(replacement.left != null)
                {
                    replacement = replacement.left;
                }

                if (node == node.parent.left)
                {
                    node.parent.left = replacement;
                }
                else
                {
                    node.parent.right = replacement;
                }

                replacement.left = node.left;
                if (replacement.right != null) return;

                replacement.right = node.right;
                return;
            }

            if(node.left != null)
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
                return;
            }

            if (node == node.parent.left)
            {
                node.parent.left = replacement;
            }
            else
            {
                node.parent.right = replacement;
            }

            replacement.left = node.left;
            if (replacement.right != null) return;

            replacement.right = node.right;
            return;
        }

        public bool Remove(T value, RedBlackNode<T> node)
        {
            if(node.value.Equals(value) && node.isRed)
            {

            }

            if(node.left != null && node.left.isRed)
            {
                RotateRight(node);
            }

            if(value.CompareTo(node.value) < 0)
            {
                if (node.left == null) return false;
                
                if(!node.left.isRed && node.left.left != null && !node.left.left.isRed)
                {
                    MoveRedLeft(node);
                }

                Remove(value, node.left);
            }

            if (value.CompareTo(node.value) > 0)
            {
                if (node.right == null) return false;

                if(!node.right.isRed && node.left != null && !node.left.isRed)
                {
                    RotateRight(node);
                }

                Remove(value, node.right);
            }
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
    }
}
