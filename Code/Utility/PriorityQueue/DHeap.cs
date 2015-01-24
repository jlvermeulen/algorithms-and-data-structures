using System;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    namespace DataStructures
    {
        namespace PriorityQueue
        {
            /// <summary>
            /// Represents a d-ary max-heap.
            /// </summary>
            /// <typeparam name="TKey">The type of value used to establish the order of priority in the DHeap&lt;TKey, TValue&gt;.</typeparam>
            /// <typeparam name="TValue">The type of elements in the DMinHeap&lt;TKey, TValue&gt;.</typeparam>
            public class DMaxHeap<TKey, TValue> : DHeap<TKey, TValue>
                where TKey : IComparable<TKey>
            {
                /// <summary>
                /// Initialises a new instance of the DMaxHeap&lt;TKey, TValue&gt; class that is empty.
                /// </summary>
                /// <param name="d">The order of the DMaxHeap.</param>
                public DMaxHeap(int d = 2) : base(d) { }

                /// <summary>
                /// Initialises a new instance of the DMaxHeap&lt;TKey, TValue&gt; that contains the elements copied from the specified IEnumerable&lt;TKey, TValue&gt;.
                /// </summary>
                /// <param name="collection">The IEnumerable&lt;TKey, TValue&gt; whose elements are copied to the new DMaxHeap&lt;TKey, TValue&gt;.</param>
                /// <param name="d">The order of the DMaxHeap&lt;TKey, TValue&gt;.</param>
                public DMaxHeap(IEnumerable<KeyValuePair<TKey, TValue>> collection, int d = 2) : base(collection, d) { }

                /// <summary>
                /// Changes the priority of a specified item.
                /// </summary>
                /// <param name="item">The item to change the priority of.</param>
                /// <param name="priority">The new priority of the item.</param>
                public override void ChangeKey(TValue item, TKey priority)
                {
                    int index;
                    if (!this.itemToIndex.TryGetValue(item, out index))
                        return;

                    int c = this.heap[index].Key.CompareTo(priority);
                    this.heap[index] = new KeyValuePair<TKey, TValue>(priority, item);

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
                                if (this.heap[s].Key.CompareTo(this.heap[c].Key) < 0)
                                    s = c;
                            }
                            else
                                break;
                        }
                        if (s != root)
                        {
                            this.Swap(s, root);
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
                        if (this.heap[start].Key.CompareTo(this.heap[p].Key) > 0)
                        {
                            this.Swap(start, p);
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
            /// <typeparam name="TKey">The type of value used to establish the order of priority in the DHeap&lt;TKey, TValue&gt;.</typeparam>
            /// <typeparam name="TValue">The type of elements in the DMinHeap&lt;TKey, TValue&gt;.</typeparam>
            public class DMinHeap<TKey, TValue> : DHeap<TKey, TValue>
                where TKey : IComparable<TKey>
            {
                /// <summary>
                /// Initialises a new instance of the DMinHeap&lt;TKey, TValue&gt; class that is empty.
                /// </summary>
                /// <param name="d">The order of the DMinHeap.</param>
                public DMinHeap(int d = 2) : base(d) { }

                /// <summary>
                /// Initialises a new instance of the DMinHeap&lt;TKey, TValue&gt; that contains the elements copied from the specified IEnumerable&lt;TKey, TValue&gt;.
                /// </summary>
                /// <param name="collection">The IEnumerable&lt;TKey, TValue&gt; whose elements are copied to the new DMinHeap&lt;TKey, TValue&gt;.</param>
                /// <param name="d">The order of the DMinHeap&lt;TKey, TValue&gt;.</param>
                public DMinHeap(IEnumerable<KeyValuePair<TKey, TValue>> collection, int d = 2) : base(collection, d) { }

                /// <summary>
                /// Changes the priority of a specified item.
                /// </summary>
                /// <param name="item">The item to change the priority of.</param>
                /// <param name="priority">The new priority of the item.</param>
                public override void ChangeKey(TValue item, TKey priority)
                {
                    int index;
                    if (!this.itemToIndex.TryGetValue(item, out index))
                        return;

                    int c = this.heap[index].Key.CompareTo(priority);
                    this.heap[index] = new KeyValuePair<TKey, TValue>(priority, item);

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
                                if (this.heap[s].Key.CompareTo(this.heap[c].Key) > 0)
                                    s = c;
                            }
                            else
                                break;
                        }
                        if (s != root)
                        {
                            this.Swap(s, root);
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
                        if (this.heap[start].Key.CompareTo(this.heap[p].Key) < 0)
                        {
                            this.Swap(start, p);
                            start = p;
                        }
                        else
                            return;
                    }
                }
            }

            /// <summary>
            /// Represents the abstract d-ary heap that all other d-ary heaps derive from.
            /// </summary>
            /// <typeparam name="TKey">The type of value used to establish the order of priority in the DHeap&lt;TKey, TValue&gt;.</typeparam>
            /// <typeparam name="TValue">The type of elements in the DMinHeap&lt;TKey, TValue&gt;.</typeparam>
            public abstract class DHeap<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>
                where TKey : IComparable<TKey>
            {
                protected List<KeyValuePair<TKey, TValue>> heap = new List<KeyValuePair<TKey, TValue>>();
                protected int d;
                protected Dictionary<TValue, int> itemToIndex = new Dictionary<TValue, int>();

                protected DHeap(int d) { this.d = d; }

                protected DHeap(IEnumerable<KeyValuePair<TKey, TValue>> collection, int d)
                {
                    this.heap = new List<KeyValuePair<TKey, TValue>>(collection);
                    for (int i = 0; i < this.heap.Count; i++)
                        this.itemToIndex[this.heap[i].Value] = i;

                    this.d = d;
                    this.Heapify();
                }

                /// <summary>
                /// Adds the specified value to the DHeap&lt;TKey, TValue&gt; with a specific priority.
                /// </summary>
                /// <param name="item">The value to add.</param>
                /// <param name="item">The priority of the added value.</param>
                public void Add(TValue item, TKey priority) { this.Add(new KeyValuePair<TKey, TValue>(priority, item)); }

                /// <summary>
                /// Adds the specified priority-value pair to the DHeap&lt;TKey, TValue&gt;.
                /// </summary>
                /// <param name="item">The pair of priority and value to be added.</param>
                public void Add(KeyValuePair<TKey, TValue> item)
                {
                    this.itemToIndex[item.Value] = this.heap.Count;
                    this.heap.Add(item);
                    this.PullUp(this.heap.Count - 1);
                }

                /// <summary>
                /// Adds the elements of the specified IEnumerable&lt;TKey, TValue&gt; to the DHeap&lt;TKey, TValue&gt;.
                /// </summary>
                /// <param name="collection">The IEnumerable&lt;TKey, TValue&gt; whose values should be added to the DHeap&lt;TKey, TValue&gt;.</param>
                public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> collection)
                {
                    int count = this.heap.Count;
                    this.heap.AddRange(collection);
                    for (int i = this.heap.Count - count; i < this.heap.Count; i++)
                        this.itemToIndex[this.heap[i].Value] = i;

                    this.Heapify();
                }

                /// <summary>
                /// Extracts the item at the top of the DHeap&lt;TKey, TValue&gt;.
                /// </summary>
                /// <returns>The item at the top of the DHeap&lt;TKey, TValue&gt;.</returns>
                public TValue Extract()
                {
                    KeyValuePair<TKey, TValue> top = this.heap[0];
                    this.Swap(0, this.heap.Count - 1);
                    this.heap.RemoveAt(this.heap.Count - 1);
                    this.itemToIndex.Remove(top.Value);
                    this.PushDown(0);
                    return top.Value;
                }

                /// <summary>
                /// Returns the item at the top of the DHeap&lt;TKey, TValue&gt; without removing it.
                /// </summary>
                /// <returns>The item at the top of the DHeap&lt;TKey, TValue&gt;.</returns>
                public TValue Peek() { return this.heap[0].Value; }

                /// <summary>
                /// Merges a DHeap&lt;TKey, TValue&gt; into this one.
                /// </summary>
                /// <param name="other">The DHeap&lt;TKey, TValue&gt; to merge into this one.</param>
                public void Merge(DHeap<TKey, TValue> other)
                {
                    this.AddRange(other.heap);
                    this.Heapify();
                }

                protected void Heapify()
                {
                    int start = (this.heap.Count - 2) / this.d;

                    for (; start >= 0; start--)
                        this.PushDown(start);
                }

                public abstract void ChangeKey(TValue item, TKey newValue);

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

                protected void Swap(int i1, int i2)
                {
                    KeyValuePair<TKey, TValue> temp = this.heap[i1];
                    this.heap[i1] = this.heap[i2];
                    this.heap[i2] = temp;

                    this.itemToIndex[this.heap[i1].Value] = i1;
                    this.itemToIndex[this.heap[i2].Value] = i2;
                }

                /// <summary>
                /// Gets a value indicating whether the DHeap&lt;TKey, TValue&gt; is read-only.
                /// </summary>
                public bool IsReadOnly { get { return false; } }

                /// <summary>
                /// Removes the first occurrence of a specific object from the DHeap&lt;TKey, TValue&gt;.
                /// </summary>
                /// <param name="item">The object to be removed.</param>
                /// <returns><code>true</code> if item was successfully removed from the DHeap&lt;TKey, TValue&gt;; otherwise, <code>false</code>.</returns>
                public bool Remove(TValue item)
                {
                    int index;
                    if (!this.itemToIndex.TryGetValue(item, out index))
                        return false;

                    this.Swap(index, this.heap.Count - 1);

                    this.itemToIndex.Remove(this.heap[this.heap.Count - 1].Value);
                    this.heap.RemoveAt(this.heap.Count - 1);

                    this.PushDown(index);

                    return true;
                }

                /// <summary>
                /// Removes the first occurrence of a specific object from the DHeap&lt;TKey, TValue&gt;.
                /// </summary>
                /// <param name="item">The object to be removed.</param>
                /// <returns><code>true</code> if item was successfully removed from the DHeap&lt;TKey, TValue&gt;; otherwise, <code>false</code>.</returns>
                public bool Remove(KeyValuePair<TKey, TValue> item) { return this.Remove(item.Value); }

                /// <summary>
                /// Gets the number of elements contained in the DHeap&lt;TKey, TValue&gt;.
                /// </summary>
                public int Count { get { return this.heap.Count; } }

                /// <summary>
                /// Determines whether the DHeap&lt;TKey, TValue&gt; contains a specific value.
                /// </summary>
                /// <param name="item">The object to locate in the DHeap&lt;TKey, TValue&gt;.</param>
                /// <returns><code>true</code> if <paramref name="item"/> is found in the DHeap&lt;TKey, TValue&gt;; <code>false</code> otherwise.</returns>
                public bool ContainsValue(TValue item) { return this.itemToIndex.ContainsKey(item); }

                /// <summary>
                /// Determines whether the DHeap&lt;TKey, TValue&gt; contains a specific key-value pair.
                /// </summary>
                /// <param name="item">The key-value pair to locate in the DHeap&lt;TKey, TValue&gt;.</param>
                /// <returns><code>true</code> if <paramref name="item"/> is found in the DHeap&lt;TKey, TValue&gt;; <code>false</code> otherwise.</returns>
                public bool Contains(KeyValuePair<TKey, TValue> item)
                {
                    int index;
                    if (!this.itemToIndex.TryGetValue(item.Value, out index))
                        return false;

                    return this.heap[index].Key.Equals(item.Key);
                }

                /// <summary>
                /// Copies the elements of the DHeap&lt;TKey, TValue&gt; to an Array, starting at a particular Array index.
                /// </summary>
                /// <param name="array">The one-dimensional Array that is the destination of the elements copied from the DHeap&lt;TKey, TValue&gt;.</param>
                /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
                public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { this.heap.CopyTo(array, arrayIndex); }

                /// <summary>
                /// Removes all items from the DHeap&lt;TKey, TValue&gt;.
                /// </summary>
                public void Clear() { this.heap.Clear(); this.itemToIndex.Clear(); }

                /// <summary>
                /// Returns an enumerator that iterates through the collection.
                /// </summary>
                /// <returns>An IEnumerator&lt;TKey, TValue&gt; that can be used to iterate through the collection.</returns>
                IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() { return this.heap.GetEnumerator(); }

                /// <summary>
                /// Returns an enumerator that iterates through the collection.
                /// </summary>
                /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
                IEnumerator IEnumerable.GetEnumerator() { return this.heap.GetEnumerator(); }
            }
        }
    }
}