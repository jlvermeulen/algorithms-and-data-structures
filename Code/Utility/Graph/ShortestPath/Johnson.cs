using System.Collections.Generic;
using Utility.DataStructures.PriorityQueue;

namespace Utility
{
    namespace Algorithms
    {
        namespace Graph
        {
            public partial class Graph
            {
                /// <summary>
                /// Finds the lengths of the shortest paths between all pairs of vertices in the graph, if no negative cycles are present.
                /// </summary>
                /// <returns>The lengths of the shortest paths between all pairs of vertices if no negative cycles are present; <code>null</code> otherwise.</returns>
                public Dictionary<uint, Dictionary<uint, int>> Johnson()
                {
                    Dictionary<uint, Dictionary<uint, int>> lengths;
                    Dictionary<uint, Dictionary<uint, uint>> parents;
                    if (!this.Johnson(out lengths, out parents))
                        return null;
                    return lengths;
                }

                /// <summary>
                /// Finds the shortest path between all pairs of vertices in the graph, if no negative cycles are present.
                /// </summary>
                /// <param name="lengths">The lengths of the shortest paths between all pairs of vertices.</param>
                /// <param name="parents">The parent-pointers for the paths between all pairs of vertices.</param>
                /// <returns><code>true</code> if there are no negative cycles; <code>false</code> otherwise.</returns>
                public bool Johnson(out Dictionary<uint, Dictionary<uint, int>> lengths, out Dictionary<uint, Dictionary<uint, uint>> parents)
                {
                    lengths = null;
                    parents = null;

                    uint free = 0;
                    foreach (uint v in this.Vertices.Keys)
                        if (v >= free)
                            free = v + 1;

                    Graph g = new Graph();
                    foreach (Edge e in this.Edges)
                        g.AddEdge(e.From, e.To, false, e.Weight, e.Capacity);
                    foreach (uint v in this.Vertices.Keys)
                        g.AddEdge(free, v, true, 0);

                    Dictionary<uint, int> l;
                    Dictionary<uint, uint> p;
                    if (!this.BellmanFord(free, out l, out p))
                        return false;

                    foreach (Edge e in g.Edges)
                        e.Weight += l[e.From] - l[e.To];

                    g.RemoveVertex(free);

                    lengths = new Dictionary<uint, Dictionary<uint, int>>();
                    parents = new Dictionary<uint, Dictionary<uint, uint>>();
                    foreach (uint v in g.Vertices.Keys)
                    {
                        g.Dijkstra(v, out l, out p);
                        lengths[v] = l;
                        parents[v] = p;
                    }

                    return true;
                }
            }
        }
    }
}