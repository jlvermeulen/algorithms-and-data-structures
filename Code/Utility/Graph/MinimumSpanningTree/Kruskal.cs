using System;
using System.Collections.Generic;
using Utility.DataStructures.DisjointSet;

namespace Utility
{
    namespace Algorithms
    {
        namespace Graph
        {
            public partial class Graph
            {
                /// <summary>
                /// Returns the lightest tree that connects all the vertices in the graph.
                /// </summary>
                /// <returns>A list of edges that make up the minimum spanning tree.</returns>
                public List<Edge> Kruskal()
                {
                    List<Edge> mst = new List<Edge>();
                    List<Edge> edges = new List<Edge>();
                    UnionFind<uint> unionFind = new UnionFind<uint>();
                    foreach (Vertex v in this.Vertices.Values)
                        foreach (Edge e in v.Neighbours.Values)
                            edges.Add(e);

                    edges.Sort((x, y) => { return x.Weight.CompareTo(y.Weight); });
                    foreach (Vertex v in this.Vertices.Values)
                        unionFind.Make(v.ID);

                    foreach (Edge e in edges)
                        if (unionFind.Find(e.From) != unionFind.Find(e.To))
                        {
                            mst.Add(e);
                            unionFind.Union(e.From, e.To);
                        }

                    return mst;
                }
            }
        }
    }
}