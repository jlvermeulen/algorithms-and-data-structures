using System;
using System.Collections.Generic;

namespace Utility
{
    public static partial class Graph
    {
        /// <summary>
        /// Returns <code>true</code> if there is a path between two given nodes in the given, unweighted graph; <code>false</code> otherwise.
        /// </summary>
        /// <param name="graph">The graph to find the path in.</param>
        /// <param name="start">The start of the path.</param>
        /// <param name="end">The end of the path.</param>
        /// <param name="path">A list containing the edges of the path that was found, in order. Empty if there is no path.</param>
        /// <returns><code>true</code> if there is a path; <code>false</code> otherwise.</returns>
        public static bool ShortestPath(IGraph<IGraphEdge> graph, IGraphNode<IGraphEdge> start, IGraphNode<IGraphEdge> end, out List<IGraphEdge> path)
        {
            path = new List<IGraphEdge>();
            Queue<BFSNode> open = new Queue<BFSNode>();
            open.Enqueue(new BFSNode(start, null, null));

            HashSet<uint> closed = new HashSet<uint>();
            closed.Add(start.ID);

            BFSNode node;
            while (open.Count > 0)
            {
                node = open.Dequeue();
                foreach (IGraphEdge e in node.Node.Neighbours)
                {
                    if (closed.Contains(e.To))
                        continue;
                    else if (e.To == end.ID)
                    {
                        path.Add(e);
                        while (node.Parent != null)
                        {
                            path.Add(node.Edge);
                            node = node.Parent;
                        }
                        path.Reverse();
                        return true;
                    }
                    open.Enqueue(new BFSNode(graph.Nodes[e.To], e, node));
                }
            }

            return false;
        }

        /// <summary>
        /// Returns <code>true</code> if there is a path between two given nodes in the given, weighted graph; <code>false</code> otherwise.
        /// </summary>
        /// <param name="graph">The graph to find the path in.</param>
        /// <param name="start">The start of the path.</param>
        /// <param name="end">The end of the path.</param>
        /// <param name="path">A list containing the edges of the path that was found, in order. Empty if there is no path.</param>
        /// <returns><code>true</code> if there is a path; <code>false</code> otherwise.</returns>
        public static bool ShortestPath(IGraph<IWeightedGraphEdge> graph, IGraphNode<IWeightedGraphEdge> start, IGraphNode<IWeightedGraphEdge> end, out List<IWeightedGraphEdge> path)
        {
            path = new List<IWeightedGraphEdge>();
            DMinHeap<DijkstraNode> open = new DMinHeap<DijkstraNode>();
            open.Add(new DijkstraNode(start, null, null));

            HashSet<uint> closed = new HashSet<uint>();
            closed.Add(start.ID);

            DijkstraNode node;
            while (open.Count > 0)
            {
                node = open.Extract();
                foreach (IWeightedGraphEdge e in node.Node.Neighbours)
                {
                    if (closed.Contains(e.To))
                        continue;
                    else if (e.To == end.ID)
                    {
                        path.Add(e);
                        while (node.Parent != null)
                        {
                            path.Add(node.Edge);
                            node = node.Parent;
                        }
                        path.Reverse();

                        return true;
                    }
                    open.Add(new DijkstraNode(graph.Nodes[e.To], e, node));
                }
            }

            return false;
        }

        private class BFSNode
        {
            public BFSNode(IGraphNode<IGraphEdge> node, IGraphEdge edge, BFSNode parent)
            {
                this.Node = node;
                this.Edge = edge;
                this.Parent = parent;
            }

            public IGraphNode<IGraphEdge> Node { get; private set; }
            public IGraphEdge Edge { get; private set; }
            public BFSNode Parent { get; private set; }
        }

        private class DijkstraNode : IComparable<DijkstraNode>
        {
            public DijkstraNode(IGraphNode<IWeightedGraphEdge> node, IWeightedGraphEdge edge, DijkstraNode parent)
            {
                this.Node = node;
                this.Edge = edge;
                this.Parent = parent;

                if (parent != null)
                    this.Weight = parent.Weight + edge.Weight;
                else
                    this.Weight = 0;
            }

            public int CompareTo(DijkstraNode other) { return this.Weight.CompareTo(other.Weight); }

            public IGraphNode<IWeightedGraphEdge> Node { get; private set; }
            public IWeightedGraphEdge Edge { get; private set; }
            public DijkstraNode Parent { get; private set; }
            public uint Weight { get; private set; }
        }
    }
}