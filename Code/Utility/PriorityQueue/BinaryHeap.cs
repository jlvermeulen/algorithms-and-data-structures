using System;
using System.Collections.Generic;
using System.Collections;

namespace Utility
{
    public class BinaryMaxHeap<T> : BinaryHeap<T>
        where T : IComparable<T>
    {
        public BinaryMaxHeap() { }

        public BinaryMaxHeap(IEnumerable<T> data) : base(data) { }

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
            int l, r, s;
            while ((l = this.Left(root)) != -1)
            {
                r = l + 1;
                s = root;
                if (this.heap[s].CompareTo(this.heap[l]) < 0)
                    s = l;
                if (r < this.heap.Count && this.heap[s].CompareTo(this.heap[r]) < 0)
                    s = r;
                if (s != root)
                    this.Switch(s, root);
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

    public class BinaryMinHeap<T> : BinaryHeap<T>
        where T : IComparable<T>
    {
        public BinaryMinHeap() { }

        public BinaryMinHeap(IEnumerable<T> data) : base(data) { }

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
            int l, r, s;
            while ((l = this.Left(root)) != -1)
            {
                r = l + 1;
                s = root;
                if (this.heap[s].CompareTo(this.heap[l]) > 0)
                    s = l;
                if (r < this.heap.Count && this.heap[s].CompareTo(this.heap[r]) > 0)
                    s = r;
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

    public abstract class BinaryHeap<T> : ICollection<T>
        where T : IComparable<T>
    {
        protected List<T> heap = new List<T>();

        public BinaryHeap() { }

        public BinaryHeap(IEnumerable<T> data)
        {
            this.heap = new List<T>(data);
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

        public void Merge(BinaryHeap<T> other)
        {
            this.heap.AddRange(other.heap);
            this.Heapify();
        }

        protected void Heapify()
        {
            int start = (this.heap.Count - 2) / 2;

            for (; start >= 0; start--)
                this.PushDown(start);
        }

        public abstract void ChangeKey(T item, T newValue);

        protected abstract void PushDown(int start);

        protected abstract void PullUp(int start);

        protected int Parent(int node)
        {
            int p = (node + 1) / 2 - 1;
            if (p >= 0 && p < this.heap.Count)
                return p;
            return -1;
        }

        protected int Left(int node)
        {
            int l = (node + 1) * 2 - 1;
            if (l < this.heap.Count)
                return l;
            return -1;
        }

        protected int Right(int node)
        {
            int r = (node + 1) * 2;
            if (r < this.heap.Count)
                return r;
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
            int index = -1;
            for (int i = 0; i < this.heap.Count; i++)
                if (item.CompareTo(this.heap[i]) == 0)
                    index = i;

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