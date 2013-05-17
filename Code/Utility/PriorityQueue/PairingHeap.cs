using System;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// Represents a pairing max-heap.
    /// </summary>
    /// <typeparam name="T">The type of elements in the PairingMaxHeap&ltT>.</typeparam>
    public class PairingMaxHeap<T> : PairingHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Initializes a new instance of the PairingMaxHeap&lt;T> class that is empty.
        /// </summary>
        public PairingMaxHeap() : base() { }

        /// <summary>
        /// Initializes a new instance of the PairingMaxHeap&lt;T> class that contains elements copied from the specified IEnumerable&lt;T>.
        /// </summary>
        /// <param name="collection">The IEnumerable&lt;T> whose elements are copied to the new PairingMaxHeap&lt;T>.</param>
        public PairingMaxHeap(IEnumerable<T> collection) : base(collection) { }

        private PairingMaxHeap(T item) : base(item) { }

        private PairingMaxHeap(T root, LinkedList<PairingHeap<T>> children, int count) : base(root, children, count) { }

        /// <summary>
        /// Adds the specified value to the PairingMaxHeap&lt;T>.
        /// </summary>
        /// <param name="item">The value to add.</param>
        public override void Add(T item) { this.Merge(new PairingMaxHeap<T>(item)); }

        /// <summary>
        /// Merges a heap into this one.
        /// </summary>
        /// <param name="other">The heap to merge into this one.</param>
        public override void Merge(PairingHeap<T> other)
        {
            PairingMaxHeap<T> heap = other as PairingMaxHeap<T>;
            if (heap == null)
            {
                this.AddRange(other);
                return;
            }

            if (heap.Count == 0)
                return;

            if (this.Count == 0)
            {
                this.root = heap.root;
                this.children = heap.children;
                this.Count = heap.Count;
                return;
            }

            this.Count += heap.Count;
            if (this.root.CompareTo(heap.root) >= 0)
            {
                this.children.AddFirst(heap);
                heap.parent = this;
            }
            else
            {
                PairingMaxHeap<T> newHeap = new PairingMaxHeap<T>(this.root, this.children, this.Count);
                this.root = heap.root;
                this.children = heap.children;
                this.children.AddFirst(newHeap);
                newHeap.parent = this;
            }
        }

        /// <summary>
        /// Changes the value of a specified item.
        /// </summary>
        /// <param name="item">The value of the item to be changed.</param>
        /// <param name="newValue">The new value of the item.</param>
        public bool DecreaseKey(T item, T newValue)
        {
            PairingHeap<T> heap;
            LinkedListNode<PairingHeap<T>> node;

            if (this.Find(item, out heap, out node))
            {
                PairingMaxHeap<T> minHeap = (PairingMaxHeap<T>)heap;
                minHeap.root = newValue;
                if (node == null)
                    return true;
                node.List.Remove(node);

                do
                {
                    minHeap = (PairingMaxHeap<T>)minHeap.parent;
                    minHeap.Count -= heap.Count;
                }
                while (minHeap.parent != null);

                this.Merge(heap);
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Represents a pairing min-heap.
    /// </summary>
    /// <typeparam name="T">The type of elements in the PairingMinHeap&ltT>.</typeparam>
    public class PairingMinHeap<T> : PairingHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Initializes a new instance of the PairingMinHeap&lt;T> class that is empty.
        /// </summary>
        public PairingMinHeap() : base() { }

        /// <summary>
        /// Initializes a new instance of the PairingMinHeap&lt;T> class that contains elements copied from the specified IEnumerable&lt;T>.
        /// </summary>
        /// <param name="collection">The IEnumerable&lt;T> whose elements are copied to the new PairingMinHeap&lt;T>.</param>
        public PairingMinHeap(IEnumerable<T> collection) : base(collection) { }

        private PairingMinHeap(T item) : base(item) { }

        private PairingMinHeap(T root, LinkedList<PairingHeap<T>> children, int count) : base(root, children, count) { }

        /// <summary>
        /// Adds the specified value to the PairingMinHeap&lt;T>.
        /// </summary>
        /// <param name="item">The value to add.</param>
        public override void Add(T item) { this.Merge(new PairingMinHeap<T>(item)); }

        /// <summary>
        /// Merges a heap into this one.
        /// </summary>
        /// <param name="other">The heap to merge into this one.</param>
        public override void Merge(PairingHeap<T> other)
        {
            PairingMinHeap<T> heap = other as PairingMinHeap<T>;
            if (heap == null)
            {
                this.AddRange(other);
                return;
            }

            if (heap.Count == 0)
                return;

            if (this.Count == 0)
            {
                this.root = heap.root;
                this.children = heap.children;
                this.Count = heap.Count;
                return;
            }

            this.Count += heap.Count;
            if (this.root.CompareTo(heap.root) <= 0)
            {
                this.children.AddFirst(heap);
                heap.parent = this;
            }
            else
            {
                PairingMinHeap<T> newHeap = new PairingMinHeap<T>(this.root, this.children, this.Count - heap.Count);
                this.root = heap.root;
                this.children = heap.children;
                this.children.AddFirst(newHeap);
                newHeap.parent = this;
            }
        }

        /// <summary>
        /// Decreases the value of a specified item.
        /// </summary>
        /// <param name="item">The value of the item to be changed.</param>
        /// <param name="newValue">The new value of the item.</param>
        public bool DecreaseKey(T item, T newValue)
        {
            PairingHeap<T> heap;
            LinkedListNode<PairingHeap<T>> node;

            if (this.Find(item, out heap, out node))
            {
                PairingMinHeap<T> minHeap = (PairingMinHeap<T>)heap;
                minHeap.root = newValue;
                if (node == null)
                    return true;
                node.List.Remove(node);

                do
                {
                    minHeap = (PairingMinHeap<T>)minHeap.parent;
                    minHeap.Count -= heap.Count;
                }
                while (minHeap.parent != null);

                this.Merge(heap);
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Represents the abstract pairing heap that all other pairing heaps derive from.
    /// </summary>
    /// <typeparam name="T">The type of elements in the PairingHeap&ltT>.</typeparam>
    public abstract class PairingHeap<T> : ICollection<T>
        where T : IComparable<T>
    {
        protected T root;
        protected LinkedList<PairingHeap<T>> children;
        protected PairingHeap<T> parent;

        protected PairingHeap()
        {
            this.children = new LinkedList<PairingHeap<T>>();
            this.Count = 0;
        }

        protected PairingHeap(IEnumerable<T> collection) : this() { this.AddRange(collection); }

        protected PairingHeap(T item)
            : this()
        {
            this.root = item;
            this.Count = 1;
        }

        protected PairingHeap(T root, LinkedList<PairingHeap<T>> children, int count)
        {
            this.root = root;
            this.children = children;
            this.Count = count;
        }

        public abstract void Add(T item);

        /// <summary>
        /// Adds the elements of the specified IEnumerable&lt;T> to the PairingHeap&lt;T>.
        /// </summary>
        /// <param name="collection">The IEnumerable&lt;T> whose values should be added to the PairingHeap&lt;T>.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            foreach (T t in collection)
                this.Add(t);
        }

        public abstract void Merge(PairingHeap<T> other);

        /// <summary>
        /// Returns the item at the top of the PairingHeap&lt;T> without removing it.
        /// </summary>
        /// <returns>The item at the top of the PairingHeap&lt;T>.</returns>
        public T Peek() { return this.root; }

        /// <summary>
        /// Extracts the item at the top of the PairingHeap&lt;T>.
        /// </summary>
        /// <returns>The item at the top of the PairingHeap&lt;T>.</returns>
        public T Extract()
        {
            T top = this.root;
            this.MergePairs();
            return top;
        }

        private void MergePairs()
        {
            List<PairingHeap<T>> list = new List<PairingHeap<T>>();
            LinkedListNode<PairingHeap<T>> next = this.children.First, one, two;
            while (next != null)
            {
                one = next;
                two = next.Next;
                if (two == null)
                {
                    list.Add(one.Value);
                    break;
                }

                one.Value.Merge(two.Value);
                this.children.Remove(two);
                list.Add(one.Value);

                next = next.Next;
            }

            this.Count = 0;
            for (int i = list.Count - 1; i >= 0; i--)
                this.Merge(list[i]);
        }

        protected bool Find(T item, out PairingHeap<T> heap, out LinkedListNode<PairingHeap<T>> node)
        {
            heap = null; node = null;

            if (this.root.CompareTo(item) == 0)
            {
                heap = this;
                return true;
            }

            if (this.children.Count == 0)
                return false;

            Stack<LinkedListNode<PairingHeap<T>>> stack = new Stack<LinkedListNode<PairingHeap<T>>>();
            LinkedListNode<PairingHeap<T>> current = this.children.First;

            stack.Push(new LinkedListNode<PairingHeap<T>>(this));
            while (stack.Count > 0)
            {
                if (current.Value.root.CompareTo(item) == 0)
                {
                    heap = current.Value;
                    node = current;
                    return true;
                }

                if (current.Value.children.Count > 0)
                {
                    if (current.Next != null)
                        stack.Push(current.Next);
                    current = current.Value.children.First;
                }
                else if (current.Next != null)
                    current = current.Next;
                else
                    current = stack.Pop();
            }

            return false;
        }

        /// <summary>
        /// Determines whether the PairingHeap&lt;T> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the PairingHeap&lt;T>.</param>
        /// <returns><code>true</code> if <paramref name="item"/> is found in the PairingHeap&lt;T>; <code>false</code> otherwise.</returns>
        public bool Contains(T item)
        {
            PairingHeap<T> heap;
            LinkedListNode<PairingHeap<T>> node;

            return this.Find(item, out heap, out node);
        }

        /// <summary>
        /// Copies the elements of the PairingHeap&lt;T> to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from the PairingHeap&lt;T>.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException("The number of elements in the PairingHeap is greater than the available space from arrayIndex to the end of the destination array.");

            foreach (T t in this)
                array[arrayIndex++] = t;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the PairingHeap&lt;T>.
        /// </summary>
        /// <param name="item">The object to be removed.</param>
        /// <returns><code>true</code> if item was successfully removed from the PairingHeap&lt;T>; otherwise, <code>false</code>.</returns>
        public bool Remove(T item)
        {
            PairingHeap<T> heap, parent;
            LinkedListNode<PairingHeap<T>> node;

            if (this.Find(item, out heap, out node))
            {
                parent = heap;
                while (parent != null)
                {
                    parent.Count--;
                    parent = parent.parent;
                }

                heap.MergePairs();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes all items from the PairingHeap&lt;T>.
        /// </summary>
        public void Clear() { this.children.Clear(); this.Count = 0; }

        /// <summary>
        /// Gets the number of elements contained in the PairingHeap&lt;T>.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the PairingHeap&lt;T> is read-only.
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An IEnumerator&lt;T> that can be used to iterate through the collection.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() { return new PairingHeapEnumerator(this); }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() { return new PairingHeapEnumerator(this); }

        private class PairingHeapEnumerator : IEnumerator<T>
        {
            private PairingHeap<T> heap;
            private LinkedListNode<PairingHeap<T>> current;
            private Stack<LinkedListNode<PairingHeap<T>>> stack;

            public PairingHeapEnumerator(PairingHeap<T> heap)
            {
                this.heap = heap;
                this.current = null;
                this.stack = new Stack<LinkedListNode<PairingHeap<T>>>();
            }

            public bool MoveNext()
            {
                if (this.current == null)
                {
                    this.current = new LinkedListNode<PairingHeap<T>>(this.heap);
                    return true;
                }

                if (this.current.Value.children.Count > 0)
                {
                    if (this.current.Next != null)
                        this.stack.Push(this.current.Next);
                    this.current = this.current.Value.children.First;
                    return true;
                }

                if (this.current.Next != null)
                {
                    this.current = this.current.Next;
                    return true;
                }

                if (this.stack.Count > 0)
                {
                    this.current = this.stack.Pop();
                    return true;
                }

                return false;
            }

            public void Reset() { this.current = null; this.stack.Clear(); }

            void IDisposable.Dispose() { }

            public T Current { get { return current.Value.root; } }

            object IEnumerator.Current { get { return current.Value.root; } }
        }
    }
}