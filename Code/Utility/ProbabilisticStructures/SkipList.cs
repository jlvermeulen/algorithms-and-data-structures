using System;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    namespace DataStructures
    {
        namespace Probabilistic
        {
            /// <summary>
            /// Represents a skip list.
            /// </summary>
            /// <typeparam name="T">The type of the values in the SkipList&lt;T>.</typeparam>
            public class SkipList<T> : ICollection<T>
                where T : IComparable<T>
            {
                private SkipNode head;
                private float p;
                private Random random;

                /// <summary>
                /// Initializes a new instance of the SkipList&lt;T> class that is empty.
                /// </summary>
                /// <param name="p">The probability of adding new items to a higher layer.</param>
                public SkipList(float p = 0.25f) { this.Initialise(p); }

                /// <summary>
                /// Initializes a new instance of the SkipList&lt;T> class that contains elements copied from the specified IEnumerable&lt;T>.
                /// </summary>
                /// <param name="collection">The IEnumerable&lt;T> whose elements are copied to the new SkipList&lt;T>.</param>
                /// <param name="p">The probability of adding new items to a higher layer.</param>
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

                /// <summary>
                /// Adds the specified value to the SkipList&lt;T>.
                /// </summary>
                /// <param name="item">The value to add.</param>
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

                /// <summary>
                /// Adds the elements of the specified IEnumerable&lt;T> to the SkipList&lt;T>.
                /// </summary>
                /// <param name="collection">The IEnumerable&lt;T> whose values should be added to the SkipList&lt;T>.</param>
                public void AddRange(IEnumerable<T> collection)
                {
                    if (collection == null)
                        throw new ArgumentNullException("collection");

                    foreach (T t in collection)
                        this.Add(t);
                }

                /// <summary>
                /// Removes the first occurrence of a specific object from the SkipList&lt;T>.
                /// </summary>
                /// <param name="item">The object to be removed.</param>
                /// <returns><code>true</code> if item was successfully removed from the SkipList&lt;T>; otherwise, <code>false</code>.</returns>
                public bool Remove(T item)
                {
                    SkipNode node = this.Find(item), pre = node;
                    if (node == null)
                        return false;
                    this.Count--;

                    if (node.Count > 1)
                        while (pre != null)
                        {
                            pre.Count--;
                            pre = pre.Up;
                        }
                    else
                    {
                        while (pre != null)
                        {
                            if (pre.Next != null)
                                pre.Next.Previous = pre.Previous;
                            else if (pre.Previous.Previous == null)
                            {
                                this.head = pre.Previous.Down;
                                this.head.Up = null;
                                break;
                            }
                            pre.Previous.Next = pre.Next;
                            pre = pre.Up;
                        }
                    }

                    return true;
                }

                /// <summary>
                /// Determines whether the SkipList&lt;T> contains a specific value.
                /// </summary>
                /// <param name="item">The object to locate in the SkipList&lt;T>.</param>
                /// <returns><code>true</code> if <paramref name="item"/> is found in the SkipList&lt;T>; <code>false</code> otherwise.</returns>
                public bool Contains(T item) { return this.Find(item) != null; }

                /// <summary>
                /// Removes all items from the SkipList&lt;T>.
                /// </summary>
                public void Clear() { this.head = new SkipNode(default(T)); this.Count = 0; }

                /// <summary>
                /// Copies the elements of the SkipList&lt;T> to an Array, starting at a particular Array index.
                /// </summary>
                /// <param name="array">The one-dimensional Array that is the destination of the elements copied from the SkipList&lt;T>.</param>
                /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
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

                /// <summary>
                /// Gets the number of elements contained in the SkipList&lt;T>.
                /// </summary>
                public int Count { get; private set; }

                /// <summary>
                /// Gets a value indicating whether the SkipList&lt;T> is read-only.
                /// </summary>
                public bool IsReadOnly { get { return false; } }

                /// <summary>
                /// Returns an enumerator that iterates through the collection.
                /// </summary>
                /// <returns>An IEnumerator&lt;T> that can be used to iterate through the collection.</returns>
                IEnumerator<T> IEnumerable<T>.GetEnumerator() { return new SLEnumerator(this); }

                /// <summary>
                /// Returns an enumerator that iterates through the collection.
                /// </summary>
                /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
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
                        else
                            node = node.Down;

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
    }
}