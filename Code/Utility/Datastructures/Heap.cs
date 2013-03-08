using System;
using System.Collections.Generic;

namespace Utility
{
    using System;
using System.Collections.Generic;

public class MaxHeap<T> : Heap<T>
    where T : IComparable<T>
{
    public MaxHeap() { }

    public MaxHeap(ICollection<T> data) : base(data) { }

    protected override void Heapify(int root)
    {
        int p = this.Parent(root);

        if (p != -1 && this.heap[p].CompareTo(this.heap[root]) < 0)
        {
            this.Switch(p, root);
            this.Heapify(p);
        }
        else
        {
            int l = this.Left(root);
            int r = this.Right(root);

            if (l != -1 && r != -1)
            {
                int max = this.heap[l].CompareTo(this.heap[r]) > 0 ? l : r;
                if(this.heap[max].CompareTo(this.heap[root]) > 0)
                {
                    this.Switch(max, root);
                    this.Heapify(max);
                }
            }
            else if (l != -1 && this.heap[l].CompareTo(this.heap[root]) > 0)
            {
                this.Switch(l, root);
                this.Heapify(l);
            }
            else if (r != -1 && this.heap[r].CompareTo(this.heap[root]) > 0)
            {
                this.Switch(r, root);
                this.Heapify(r);
            }
        }
    }
}

public class MinHeap<T> : Heap<T>
    where T : IComparable<T>
{
    public MinHeap() { }

    public MinHeap(ICollection<T> data) : base(data) { }

    protected override void Heapify(int root)
    {
        int p = this.Parent(root);

        if (p != -1 && this.heap[p].CompareTo(this.heap[root]) > 0)
        {
            this.Switch(p, root);
            this.Heapify(p);
        }
        else
        {
            int l = this.Left(root);
            int r = this.Right(root);

            if (l != -1 && r != -1)
            {
                int max = this.heap[l].CompareTo(this.heap[r]) < 0 ? l : r;
                if(this.heap[max].CompareTo(this.heap[root]) < 0)
                {
                    this.Switch(max, root);
                    this.Heapify(max);
                }
            }
            else if (l != -1 && this.heap[l].CompareTo(this.heap[root]) < 0)
            {
                this.Switch(l, root);
                this.Heapify(l);
            }
            else if (r != -1 && this.heap[r].CompareTo(this.heap[root]) < 0)
            {
                this.Switch(r, root);
                this.Heapify(r);
            }
        }
    }
}

public abstract class Heap<T>
    where T : IComparable<T>
{
    protected List<T> heap = new List<T>();

    public Heap() { }

    public Heap(ICollection<T> data)
    {
        foreach (T t in data)
            Add(t);
    }

    public void Add(T item)
    {
        this.heap.Add(item);
        this.Heapify(this.heap.Count - 1);
    }

    public T Top()
    {
        T top = this.heap[0];
        this.heap[0] = this.heap[this.heap.Count - 1];
        this.heap.RemoveAt(this.heap.Count - 1);
        this.Heapify(0);
        return top;
    }

    public T Peek()
    {
        return this.heap[0];
    }

    public int Count { get { return this.heap.Count; } }

    protected abstract void Heapify(int root);

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
}
}
