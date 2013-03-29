using System;
using System.Collections.Generic;

namespace Utility
{
    public class SplayTree<TKey, TValue> : BinarySearchTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public SplayTree() { }

        public SplayTree(IDictionary<TKey, TValue> collection) : base(collection) { }

        public override void Add(TKey key, TValue value)
        {
            KeyValueTreeNode node = new KeyValueTreeNode(key, value);
            node = base.Add(node);
            this.Splay(node);
        }

        public override bool Remove(TKey key)
        {
            KeyValueTreeNode current = (KeyValueTreeNode)this.root, previous = null;
            while (current != null)
            {
                previous = current;
                if (key.CompareTo(current.Key) < 0)
                    current = current.Left;
                else if (key.CompareTo(current.Key) > 0)
                    current = current.Right;
                else
                    break;
            }

            if (current == null)
            {
                this.Splay(previous);
                return false;
            }

            if (current.Left == null || current.Right == null)
                this.Delete(current);
            else
            {
                KeyValueTreeNode replace = (KeyValueTreeNode)this.Predecessor(current);
                if (replace == null)
                    replace = (KeyValueTreeNode)this.Successor(current);

                current.Value = replace.Value;
                this.Delete(replace);
            }

            this.Count--;
            return true;
        }

        protected override void Delete(TreeNode current)
        {
            base.Delete(current);
            this.Splay((KeyValueTreeNode)current.Parent);
        }

        protected override KeyValueTreeNode Find(TKey key)
        {
            KeyValueTreeNode current = (KeyValueTreeNode)this.root, previous = null;
            while (current != null)
            {
                int c = key.CompareTo(current.Key);
                previous = current;
                if (c < 0)
                    current = current.Left;
                else if (c > 0)
                    current = current.Right;
                else
                    break;
            }

            this.Splay(previous);
            return current;
        }

        private void Splay(KeyValueTreeNode start)
        {
            bool leftC, leftPC;

            if (this.root == start)
                return;

            leftC = start == start.Parent.Left;

            if (start.Parent.Parent == null)
            {
                if (leftC)
                    this.RotateRight(start.Parent);
                else
                    this.RotateLeft(start.Parent);
                return;
            }

            leftPC = start.Parent == start.Parent.Parent.Left;

            if (leftC && leftPC)
            {
                this.RotateRight(start.Parent.Parent);
                this.RotateRight(start.Parent);
            }
            else if (!leftC && !leftPC)
            {
                this.RotateLeft(start.Parent.Parent);
                this.RotateLeft(start.Parent);
            }
            else if (!leftC && leftPC)
            {
                this.RotateLeft(start.Parent);
                this.RotateRight(start.Parent);
            }
            else
            {
                this.RotateRight(start.Parent);
                this.RotateLeft(start.Parent);
            }

            this.Splay(start);
        }
    }
}