using System;
using System.Collections.Generic;
using System.Collections;

namespace Utility
{
    public class DMaxHeap<T> : DHeap<T>
        where T : IComparable<T>
    {
        public DMaxHeap(int d = 2) : base(d) { }

        public DMaxHeap(IEnumerable<T> collection, int d = 2) : base(collection, d) { }

        public override void ChangeKey(T item, T newValue)
        {
            int index = this.heap.IndexOf(item);
            if (index == -1)
                return;
            this.heap[index] = newValue;
            int c = item.CompareTo(newValue);
            if (c < 0)
                this.PullUp(index);
            else if (c > 0)
                this.PushDown(index);
        }

        protected override void PushDown(int root)
        {
            int l, c, s;
            while ((l = this.Left(root)) != -1)
            {
                s = root;
                for (int i = 0; i < this.d; i++)
                {
                    c = l + i;
                    if (c < this.heap.Count)
                    {
                        if (this.heap[s].CompareTo(this.heap[c]) < 0)
                            s = c;
                    }
                    else
                        break;
                }
                if (s != root)
                {
                    this.Switch(s, root);
                    root = s;
                }
                else
                    return;
            }
        }

        protected override void PullUp(int start)
        {
            int p;
            while ((p = this.Parent(start)) != -1)
            {
                if (this.heap[start].CompareTo(this.heap[p]) > 0)
                {
                    this.Switch(start, p);
                    start = p;
                }
                else
                    return;
            }
        }
    }

    public class DMinHeap<T> : DHeap<T>
        where T : IComparable<T>
    {
        public DMinHeap(int d = 2) : base(d) { }

        public DMinHeap(IEnumerable<T> collection, int d = 2) : base(collection, d) { }

        public override void ChangeKey(T item, T newValue)
        {
            int index = this.heap.IndexOf(item);
            if (index == -1)
                return;
            this.heap[index] = newValue;
            int c = item.CompareTo(newValue);
            if (c < 0)
                this.PushDown(index);
            else if (c > 0)
                this.PullUp(index);
        }

        protected override void PushDown(int root)
        {
            int l, c, s;
            while ((l = this.Left(root)) != -1)
            {
                s = root;
                for (int i = 0; i < this.d; i++)
                {
                    c = l + i;
                    if (c < this.heap.Count)
                    {
                        if (this.heap[s].CompareTo(this.heap[c]) > 0)
                            s = c;
                    }
                    else
                        break;
                }
                if (s != root)
                {
                    this.Switch(s, root);
                    root = s;
                }
                else
                    return;
            }
        }

        protected override void PullUp(int start)
        {
            int p;
            while ((p = this.Parent(start)) != -1)
            {
                if (this.heap[start].CompareTo(this.heap[p]) < 0)
                {
                    this.Switch(start, p);
                    start = p;
                }
                else
                    return;
            }
        }
    }

    public abstract class DHeap<T> : ICollection<T>
        where T : IComparable<T>
    {
        protected List<T> heap = new List<T>();
        protected int d;

        public DHeap(int d) { this.d = d; }

        public DHeap(IEnumerable<T> collection, int d)
        {
            this.heap = new List<T>(collection);
            this.d = d;
            this.Heapify();
        }

        public void Add(T item)
        {
            this.heap.Add(item);
            this.PullUp(this.heap.Count - 1);
        }

        public T Extract()
        {
            T top = this.heap[0];
            this.heap[0] = this.heap[this.heap.Count - 1];
            this.heap.RemoveAt(this.heap.Count - 1);
            this.PushDown(0);
            return top;
        }

        public T Peek()
        {
            return this.heap[0];
        }

        public void Merge(DHeap<T> other)
        {
            this.heap.AddRange(other.heap);
            this.Heapify();
        }

        protected void Heapify()
        {
            int start = (this.heap.Count - 2) / this.d;

            for (; start >= 0; start--)
                this.PushDown(start);
        }

        public abstract void ChangeKey(T item, T newValue);

        protected abstract void PushDown(int start);

        protected abstract void PullUp(int start);

        protected int Parent(int node)
        {
            int p = (node - 1) / this.d;
            if (p >= 0)
                return p;
            return -1;
        }

        protected int Left(int node)
        {
            int l = node * this.d + 1;
            if (l < this.heap.Count)
                return l;
            return -1;
        }

        protected void Switch(int i1, int i2)
        {
            T temp = this.heap[i1];
            this.heap[i1] = this.heap[i2];
            this.heap[i2] = temp;
        }

        public bool IsReadOnly { get { return false; } }

        public bool Remove(T item)
        {
            int index = this.heap.IndexOf(item);
            if (index == -1)
                return false;

            this.heap[index] = this.heap[this.heap.Count - 1];
            this.heap.RemoveAt(this.heap.Count - 1);
            this.PushDown(index);

            return true;
        }

        public int Count { get { return this.heap.Count; } }

        public bool Contains(T item) { return this.heap.Contains(item); }

        public void CopyTo(T[] array, int arrayIndex) { this.heap.CopyTo(array, arrayIndex); }

        public void Clear() { this.heap.Clear(); }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() { return this.heap.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator() { return this.heap.GetEnumerator(); }
    }
}