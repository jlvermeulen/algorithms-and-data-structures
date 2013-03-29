﻿using System;
using System.Collections.Generic;

namespace Utility
{
    public class ScapegoatTree<T> : BinarySearchTree<T>
        where T : IComparable<T>
    {
        private float alpha;
        private int size, maxSize;

        public ScapegoatTree(float alpha = 1f)
        {
            this.alpha = alpha;
            this.size = 0;
            this.maxSize = 0;
        }

        public ScapegoatTree(IEnumerable<T> collection, float alpha = 0.75f)
            : base()
        {
            this.alpha = alpha;
            this.size = 0;
            this.maxSize = 0;
            foreach (T t in collection)
                this.Add(t);
        }

        public override void Add(T item)
        {
            ValueTreeNode node = new ValueTreeNode(item);
            this.Add(node);
        }

        protected override ValueTreeNode Add(ValueTreeNode node)
        {
            int depth = 0;
            this.Count++;
            if (this.root == null)
            {
                this.root = node;
                this.size++;
                return node;
            }
            ValueTreeNode current = (ValueTreeNode)this.root;
            while (current != null)
            {
                depth++;
                if (node.Value.CompareTo(current.Value) < 0)
                {
                    if (current.Left != null)
                        current = current.Left;
                    else
                    {
                        current.Left = node;
                        break;
                    }
                }
                else if (node.Value.CompareTo(current.Value) > 0)
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
                {
                    current.Count++;
                    return current;
                }
            }
            this.size++;
            this.maxSize = Math.Max(size, maxSize);
            node.Parent = current;

            if (depth > Math.Log(this.size, 1 / alpha))
                this.Rebalance(node);

            return node;
        }

        public override bool Remove(T item)
        {
            int count = this.Count;
            bool removed = base.Remove(item);
            if (count != this.Count)
            {
                this.size--;
                if (this.size <= this.maxSize / 2)
                {
                    this.Flatten((ValueTreeNode)this.root, this.size);
                    this.maxSize = this.size;
                }
            }
            return removed;
        }

        private void Rebalance(ValueTreeNode node)
        {
            int size = 1, leftSize, rightSize;
            bool leftC;
            while (node.Parent != null)
            {
                leftC = node.Parent.Left == node;
                node = node.Parent;
                if (leftC)
                {
                    rightSize = this.Size(node.Right);
                    leftSize = size;
                }
                else
                {
                    leftSize = this.Size(node.Left);
                    rightSize = size;
                }
                size = leftSize + rightSize + 1;

                if (leftSize > alpha * size || rightSize > alpha * size)
                {
                    this.Flatten(node, size);
                    return;
                }
            }
        }

        private void Flatten(ValueTreeNode node, int size)
        {
            ValueTreeNode[] nodes = new ValueTreeNode[size];
            int index = 0;
            this.Flatten(node, nodes, ref index);
            this.InsertMedians(nodes, node.Parent);
        }

        private void Flatten(ValueTreeNode node, ValueTreeNode[] array, ref int index)
        {
            if (node == null)
                return;
            this.Flatten(node.Left, array, ref index);
            array[index++] = node;
            this.Flatten(node.Right, array, ref index);
        }

        private void InsertMedians(ValueTreeNode[] array, ValueTreeNode root)
        {
            int median = array.Length / 2;
            ValueTreeNode medianNode = new ValueTreeNode(array[median].Value);
            medianNode.Count = array[median].Count;
            medianNode.Parent = root;
            if (root == null)
                this.root = medianNode;
            else if (medianNode.Value.CompareTo(root.Value) < 0)
                root.Left = medianNode;
            else
                root.Right = medianNode;
            this.InsertMedians(array, medianNode, 0, median, true);
            this.InsertMedians(array, medianNode, median + 1, array.Length, false);
        }

        private void InsertMedians(ValueTreeNode[] array, ValueTreeNode root, int start, int end, bool left)
        {
            if (end == start)
                return;
            int median = (end - start) / 2 + start;
            ValueTreeNode node = new ValueTreeNode(array[median].Value);
            node.Count = array[median].Count;
            this.AddAt(node, root, left);
            this.InsertMedians(array, node, start, median, true);
            this.InsertMedians(array, node, median + 1, end, false);
        }

        private void AddAt(ValueTreeNode node, ValueTreeNode root, bool left)
        {
            if (left)
                root.Left = node;
            else
                root.Right = node;
            node.Parent = root;
        }

        private int Size(ValueTreeNode node)
        {
            if (node == null)
                return 0;
            return this.Size(node.Left) + this.Size(node.Right) + 1;
        }
    }
}