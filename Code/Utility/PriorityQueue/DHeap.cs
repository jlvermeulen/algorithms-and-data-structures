using System;
using System.Collections.Generic;
using System.Collections;

namespace Utility
{
    /// <summary>
    /// Represents a d-ary max-heap.
    /// </summary>
    /// <typeparam name="T">The type of elements in the DMaxHeap&ltT>.</typeparam>
    public class DMaxHeap<T> : DHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Initializes a new instance of the DMaxHeap&lt;T> class that is empty.
        /// </summary>
        /// <param name="d">The order of the DMaxHeap.</param>
        public DMaxHeap(int d = 2) : base(d) { }

        /// <summary>
        /// Initialises a new instance of the DMaxHeap&lt;T> that contains the elements copied from the specified IEnumerable&ltT>.
        /// </summary>
        /// <param name="collection">The IEnumerable&lt;T> whose elements are copied to the new DMaxHeap&lt;T>.</param>
        /// <param name="d">The order of the DMaxHeap&ltT>.</param>
        public DMaxHeap(IEnumerable<T> collection, int d = 2) : base(collection, d) { }

        /// <summary>
        /// Changes the value of a specified item.
        /// </summary>
        /// <param name="item">The value of the item to be changed.</param>
        /// <param name="newValue">The new value of the item.</param>
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

    /// <summary>
    /// Represents a d-ary min-heap.
    /// </summary>
    /// <typeparam name="T">The type of elements in the DMinHeap&ltT>.</typeparam>
    public class DMinHeap<T> : DHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Initializes a new instance of the DMinHeap&lt;T> class that is empty.
        /// </summary>
        /// <param name="d">The order of the DMinHeap.</param>
        public DMinHeap(int d = 2) : base(d) { }

        /// <summary>
        /// Initialises a new instance of the DMinHeap&lt;T> that contains the elements copied from the specified IEnumerable&ltT>.
        /// </summary>
        /// <param name="collection">The IEnumerable&lt;T> whose elements are copied to the new DMinHeap&lt;T>.</param>
        /// <param name="d">The order of the DMinHeap&ltT>.</param>
        public DMinHeap(IEnumerable<T> collection, int d = 2) : base(collection, d) { }

        /// <summary>
        /// Changes the value of a specified item.
        /// </summary>
        /// <param name="item">The value of the item to be changed.</param>
        /// <param name="newValue">The new value of the item.</param>
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

        protected DHeap(int d) { this.d = d; }

        protected DHeap(IEnumerable<T> collection, int d)
        {
            this.heap = new List<T>(collection);
            this.d = d;
            this.Heapify();
        }

        /// <summary>
        /// Adds the specified value to the heap.
        /// </summary>
        /// <param name="item">The value to add.</param>
        public void Add(T item)
        {
            this.heap.Add(item);
            this.PullUp(this.heap.Count - 1);
        }

        /// <summary>
        /// Extracts the item at the top of the heap.
        /// </summary>
        /// <returns>The item at the top of the heap.</returns>
        public T Extract()
        {
            T top = this.heap[0];
            this.heap[0] = this.heap[this.heap.Count - 1];
            this.heap.RemoveAt(this.heap.Count - 1);
            this.PushDown(0);
            return top;
        }

        /// <summary>
        /// Returns the item at the top of the heap without removing it.
        /// </summary>
        /// <returns>The item at the top of the heap.</returns>
        public T Peek()
        {
            return this.heap[0];
        }

        /// <summary>
        /// Merges two heaps into one.
        /// </summary>
        /// <param name="other">The heap to merge into the other.</param>
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

        /// <summary>
        /// Gets a value indicating whether the DHeap&lt;T> is read-only.
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Removes the first occurrence of a specific object from the DHeap&lt;T>.
        /// </summary>
        /// <param name="item">The object to be removed.</param>
        /// <returns>true if item was successfully removed from the DHeap&lt;T>; otherwise, false.</returns>
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

        /// <summary>
        /// Gets the number of elements contained in the DHeap&lt;T>.
        /// </summary>
        public int Count { get { return this.heap.Count; } }

        /// <summary>
        /// Determines whether the DHeap&lt;T> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the DHeap&lt;T>.</param>
        /// <returns>true if <paramref name="item"/> is found in the DHeap&lt;T>; false otherwise.</returns>
        public bool Contains(T item) { return this.heap.Contains(item); }

        /// <summary>
        /// Copies the elements of the DHeap&lt;T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from the DHeap&lt;T>.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex) { this.heap.CopyTo(array, arrayIndex); }

        /// <summary>
        /// Removes all items from the DHeap&lt;T>.
        /// </summary>
        public void Clear() { this.heap.Clear(); }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An IEnumerator&lt;T> that can be used to iterate through the collection.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() { return this.heap.GetEnumerator(); }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() { return this.heap.GetEnumerator(); }
    }
}