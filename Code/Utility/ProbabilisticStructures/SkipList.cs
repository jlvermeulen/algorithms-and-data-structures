using System;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    public class SkipList<T> : ICollection<T>
        where T : IComparable<T>
    {
        SkipNode head;
        float p;
        Random random;

        public SkipList(float p = 0.25f) { this.Initialise(p); }

        public SkipList(IEnumerable<T> collection, float p = 0.25f)
        {
            this.Initialise(p);
            foreach (T t in collection)
                this.Add(t);
        }

        private void Initialise(float p)
        {
            this.p = p;
            this.head = new SkipNode(default(T));
            this.random = new Random();
        }

        public void Add(T item)
        {
            SkipNode node = this.head, previous = null;

            while (node != null)
                if (node.Next != null && item.CompareTo(node.Next.Value) > 0)
                {
                    previous = node;
                    node = node.Next;
                    break;
                }
                else
                {
                    previous = node;
                    node = node.Down;
                }

            while (node != null)
            {
                if (node.Next != null)
                {
                    int c = item.CompareTo(node.Next.Value);
                    if (c == 0)
                    {
                        node = node.Next;
                        this.Count++;
                        while (node != null)
                        {
                            node.Count++;
                            node = node.Down;
                        }
                        return;
                    }
                    else if (c > 0)
                    {
                        previous = node;
                        node = node.Next;
                    }
                    else
                    {
                        previous = node;
                        node = node.Down;
                    }
                    
                }
                else
                {
                    previous = node;
                    node = node.Down;
                }
            }

            node = previous;
            SkipNode newNode = new SkipNode(item);
            if (node.Next != null)
            {
                node.Next.Previous = newNode;
                newNode.Next = node.Next;
            }
            node.Next = newNode;
            newNode.Previous = node;
            this.Count++;

            while (this.random.NextDouble() < p)
            {
                node = new SkipNode(item);
                newNode.Up = node;
                node.Down = newNode;
                newNode = node;

                node = newNode.Down.Previous.PredecessorUp();
                if (node == null)
                {
                    node = new SkipNode(default(T));
                    this.head.Up = node;
                    node.Down = this.head;
                    this.head = node;
                }
                else if (node.Next != null)
                {
                    node.Next.Previous = newNode;
                    newNode.Next = node.Next;
                }

                node.Next = newNode;
                newNode.Previous = node;
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            foreach (T t in collection)
                this.Add(t);
        }

        public bool Remove(T item)
        {
            SkipNode node = this.Find(item), pre = node;
            if (node == null)
                return false;
            this.Count--;

            if (node.Count > 1)
                while (pre != null)
                    pre = pre.Up;
            else
                while (pre != null)
                {
                    pre.Next.Previous = pre.Previous;
                    pre.Previous.Next = pre.Next;
                    pre = pre.Up;
            }

            return true;
        }

        public bool Contains(T item) { return this.Find(item) != null; }

        public void Clear() { this.head = null; }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException("The number of elements in the SkipList is greater than the available space from arrayIndex to the end of the destination array.");

            foreach (T t in this)
                array[arrayIndex++] = t;
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get { return false; } }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() { return new SLEnumerator(this); }

        IEnumerator IEnumerable.GetEnumerator() { return new SLEnumerator(this); }

        private SkipNode Find(T item)
        {
            SkipNode node = this.head;

            while (node != null)
                if (item.CompareTo(node.Next.Value) >= 0)
                {
                    node = node.Next;
                    break;
                }

            while (node != null)
            {
                if (node.Next != null)
                {
                    int c = item.CompareTo(node.Next.Value);
                    if (c == 0)
                        return node;
                    else if (c > 0)
                        node = node.Next;
                    else
                        node = node.Down;
                }
                else
                    node = node.Down;
            }
            return null;
        }

        private class SkipNode
        {
            public SkipNode(T value)
            {
                this.Value = value;
                this.Count = 1;
                this.Next = null;
                this.Previous = null;
                this.Down = null;
                this.Up = null;
            }

            public T Value { get; set; }
            public int Count { get; set; }
            public SkipNode Next { get; set; }
            public SkipNode Previous { get; set; }
            public SkipNode Up { get; set; }
            public SkipNode Down { get; set; }

            public SkipNode PredecessorUp()
            {
                if (this.Previous == null)
                    return this.Up;
                if (this.Up == null)
                    return this.Previous.PredecessorUp();
                return this.Up;
            }

            public override string ToString() { return this.Value.ToString(); }
        }

        private class SLEnumerator : IEnumerator<T>
        {
            private SkipList<T> skipList;
            private SkipNode currentNode;
            private int currentCount;

            public SLEnumerator(SkipList<T> skipList)
            {
                this.skipList = skipList;
                this.currentNode = skipList.head;
                while (this.currentNode.Down != null)
                    this.currentNode = this.currentNode.Down;
                this.currentCount = 1;
            }

            public bool MoveNext()
            {
                if (currentCount < currentNode.Count)
                {
                    currentCount++;
                    return true;
                }
                currentNode = currentNode.Next;
                currentCount = 1;
                return currentNode != null;
            }

            public void Reset() { this.currentNode = null; }

            void IDisposable.Dispose() { }

            public T Current { get { return currentNode.Value; } }

            object IEnumerator.Current { get { return currentNode.Value; } }
        }
    }
}