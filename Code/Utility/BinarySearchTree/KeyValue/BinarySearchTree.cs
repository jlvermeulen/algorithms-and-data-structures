using System;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    public class BinarySearchTree<TKey, TValue> : BinaryTree, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public BinarySearchTree() { }

        public BinarySearchTree(IDictionary<TKey, TValue> collection)
        {
            foreach (KeyValuePair<TKey, TValue> t in collection)
                this.Add(t.Key, t.Value);
        }

        public virtual void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            KeyValueTreeNode node = this.Add(new KeyValueTreeNode(key, value));
            if (node == null)
                throw new ArgumentException("An item with the same key already exists in the collection.");
        }

        public virtual bool Remove(TKey key)
        {
            KeyValueTreeNode current = this.Find(key);

            if (current == null)
                return false;

            if (current.Left == null || current.Right == null)
                this.Delete(current);
            else
            {
                KeyValueTreeNode replace = (KeyValueTreeNode)this.Predecessor(current);
                if (replace == null)
                    replace = (KeyValueTreeNode)this.Successor(current);

                current.KeyValuePair = replace.KeyValuePair;
                this.Delete(replace);
            }

            this.Count--;
            return true;
        }

        public virtual bool ContainsKey(TKey key)
        {
            return false;
        }

        public virtual bool ContainsValue(TValue value)
        {
            return false;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() { return new BSTEnumerator(this); }

        IEnumerator IEnumerable.GetEnumerator() { return new BSTEnumerator(this); }

        protected virtual KeyValueTreeNode Add(KeyValueTreeNode node)
        {
            this.Count++;
            if (this.root == null)
            {
                this.root = node;
                return node;
            }
            KeyValueTreeNode current = (KeyValueTreeNode)this.root;
            while (current != null)
            {
                int c = node.Key.CompareTo(current.Key);
                if (c < 0)
                {
                    if (current.Left != null)
                        current = current.Left;
                    else
                    {
                        current.Left = node;
                        break;
                    }
                }
                else if (c > 0)
                {
                    if (current.Right != null)
                        current = current.Right;
                    else
                    {
                        current.Right = node;
                        break;
                    }
                }
                else
                    return null;
            }
            node.Parent = current;
            return node;
        }

        protected virtual KeyValueTreeNode Find(TKey key)
        {
            KeyValueTreeNode current = (KeyValueTreeNode)this.root;
            while (current != null)
            {
                int c = key.CompareTo(current.KeyValuePair.Key);
                if (c < 0)
                    current = current.Left;
                else if (c > 0)
                    current = current.Right;
                else
                    break;
            }

            return current;
        }

        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>(this.Count);
                foreach (KeyValuePair<TKey, TValue> kvp in this)
                    keys.Add(kvp.Key);
                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>(this.Count);
                foreach (KeyValuePair<TKey, TValue> kvp in this)
                    values.Add(kvp.Value);
                return values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                KeyValueTreeNode node = this.Find(key);
                if (node == null)
                    throw new KeyNotFoundException("The given key was not present in the collection.");
                return node.Value;
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key");
                KeyValueTreeNode node = this.Find(key);
                if (node == null)
                    throw new KeyNotFoundException("The given key was not present in the collection.");
                node.Value = value;
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            KeyValueTreeNode node = this.Find(key);
            if (node == null)
            {
                value = default(TValue);
                return false;
            }
            value = node.Value;
            return true;
        }

        protected class KeyValueTreeNode : TreeNode
        {
            public KeyValueTreeNode(TKey key, TValue value) { this.KeyValuePair = new KeyValuePair<TKey, TValue>(key, value); }

            public KeyValuePair<TKey, TValue> KeyValuePair { get; set; }
            public TKey Key { get { return this.KeyValuePair.Key; } }
            public TValue Value
            {
                get { return this.KeyValuePair.Value; }
                set { this.KeyValuePair = new KeyValuePair<TKey, TValue>(this.Key, value); }
            }
            public new KeyValueTreeNode Parent { get { return (KeyValueTreeNode)base.Parent; } set { base.Parent = value; } }
            public new KeyValueTreeNode Left { get { return (KeyValueTreeNode)base.Left; } set { base.Left = value; } }
            public new KeyValueTreeNode Right { get { return (KeyValueTreeNode)base.Right; } set { base.Right = value; } }
        }

        private class BSTEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private BinarySearchTree<TKey, TValue> tree;
            private KeyValueTreeNode currentNode;

            public BSTEnumerator(BinarySearchTree<TKey, TValue> tree)
            {
                this.tree = tree;
                this.currentNode = null;
            }

            public bool MoveNext()
            {
                KeyValueTreeNode successor;
                if (tree.root == null)
                    return false;
                if (currentNode == null)
                {
                    currentNode = (KeyValueTreeNode)tree.root;
                    while (currentNode.Left != null)
                        currentNode = currentNode.Left;
                    return true;
                }

                if ((successor = (KeyValueTreeNode)tree.Successor(currentNode)) != null)
                {
                    currentNode = successor;
                    return true;
                }

                return false;
            }

            public void Reset() { this.currentNode = null; }

            void IDisposable.Dispose() { }

            public KeyValuePair<TKey, TValue> Current { get { return currentNode.KeyValuePair; } }

            object IEnumerator.Current { get { return currentNode.KeyValuePair; } }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) { throw new NotSupportedException(); }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] item, int i) { throw new NotSupportedException(); }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) { throw new NotSupportedException(); }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) { throw new NotSupportedException(); }
    }
}