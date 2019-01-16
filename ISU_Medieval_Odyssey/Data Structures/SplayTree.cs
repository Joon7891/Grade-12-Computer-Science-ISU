using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISU_Medieval_Odyssey
{
    class SplayTree<T> where T : IComparable<T>
    {
        SplayNode<T> root;

        public SplayTree() { }

        private SplayNode<T> RotateLeft(SplayNode<T> node)
        {
            SplayNode<T> temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            return temp;
        }

        private SplayNode<T> RotateRight(SplayNode<T> node)
        {
            SplayNode<T> temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            return temp;
        }

        private SplayNode<T> Splay(SplayNode<T> node, T key)
        {
            if(node == null || node.Key.CompareTo(key) == 0)
            {
                return node;
            }

            if (node.Key.CompareTo(key) > 0)
            {
                if (node.Left == null)
                {
                    return node;
                }

                // left-left rotation
                if (node.Left.Key.CompareTo(key) > 0)
                {
                    node.Left.Left = Splay(node.Left.Left, key);
                    node = RotateRight(node);
                }
                // left-right rotation
                else if (node.Left.Key.CompareTo(key) < 0)
                {
                    node.Left.Right = Splay(node.Left.Right, key);

                    if (node.Left.Right != null)
                    {
                        node.Left = RotateLeft(node.Left);
                    }
                }
                return (node.Left != null ? RotateRight(node) : node);
            }
            else
            {
                if (node.Right == null)
                {
                    return node;
                }
                // right-left rotation
                if (node.Right.Key.CompareTo(key) > 0)
                {
                    node.Right.Left = Splay(node.Right.Left, key);

                    if (node.Right.Left != null)
                    {
                        node.Right = RotateRight(node.Right);
                    }
                }
                // right-right rotation
                else if(node.Right.Key.CompareTo(key) < 0)
                {
                    node.Right.Right = Splay(node.Right.Right, key);
                    node = RotateLeft(node);
                }
                return (node.Right != null ? RotateLeft(node) : node);
            }
        }

        public SplayNode<T> Insert(SplayNode<T> root, T key)
        {
            throw new NotImplementedException();
     



            if(root == null)
            {
                return; 
            }
        }

        public void Delete(T key)
        {

        }

        public SplayNode<T> Search(T key)
        {
            return Splay(root, key);
        }

        /// <summary>
        /// kth smallest
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int FindRank(int rank)
        {
            return -1;
        }
    }
}
