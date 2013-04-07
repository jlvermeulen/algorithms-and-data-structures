using System;
using System.Collections.Generic;

namespace Utility
{
    public static partial class Graph
    {
        /// <summary>
        /// Calculates the lightest tree that connects all the node in the graph.
        /// </summary>
        /// <param name="graph">The graph to calculate the minimum spanning tree of.</param>
        /// <returns>A list of edges that make up the minimum spanning tree.</returns>
        public static List<IWeightedGraphEdge> MinimumSpanningTree(IGraph<IWeightedGraphEdge> graph)
        {
            List<IWeightedGraphEdge> mst = new List<IWeightedGraphEdge>();
            List<IWeightedGraphEdge> edges = new List<IWeightedGraphEdge>(graph.Edges);
            UnionFind<uint> unionFind = new UnionFind<uint>();

            edges.Sort((x, y) => { return x.Weight.CompareTo(y.Weight); });
            foreach (IGraphNode<IWeightedGraphEdge> n in graph.Nodes)
                unionFind.Make(n.ID);

            foreach (IWeightedGraphEdge e in edges)
                if (unionFind.Find(e.From) != unionFind.Find(e.To))
                {
                    mst.Add(e);
                    unionFind.Union(e.From, e.To);
                }

            return mst;
        }
    }
}