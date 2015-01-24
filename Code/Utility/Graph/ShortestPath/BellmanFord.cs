using System.Collections.Generic;

namespace Utility
{
    namespace Algorithms
    {
        namespace Graph
        {
            public partial class Graph
            {
                /// <summary>
                /// Finds the lengths of the shortest paths from a given vertex to all other vertices, if no negative cycle is reachable from the start.
                /// </summary>
                /// <param name="from">The vertex to calculate the lengths of the shortest paths from.</param>
                /// <returns>The lengths of the shortest paths if no negative cycles are present; <code>null</code> otherwise.</returns>
                public Dictionary<uint, int> BellmanFord(uint from)
                {
                    Dictionary<uint, int> lengths;
                    Dictionary<uint, uint> parents;
                    if (!this.BellmanFord(from, out lengths, out parents))
                        return null;
                    return lengths;
                }

                /// <summary>
                /// Finds the shortest path from a given vertex to all other vertices, if no negative cycle is reachable from the start.
                /// </summary>
                /// <param name="from">The vertex to calculate the shortest paths from.</param>
                /// <param name="lengths">The lengths of the shortest paths to all vertices.</param>
                /// <param name="parents">The parent-pointers for the paths to all vertices.</param>
                /// <returns><code>true</code> if there are no negative cycles reachable from the start; <code>false</code> otherwise.</returns>
                public bool BellmanFord(uint from, out Dictionary<uint, int> lengths, out Dictionary<uint, uint> parents)
                {
                    lengths = new Dictionary<uint, int>();
                    parents = new Dictionary<uint, uint>();

                    foreach (uint v in this.Vertices.Keys)
                        lengths[v] = int.MaxValue;
                    lengths[from] = 0;

                    
                    for (int i = 0; i < this.Vertices.Count - 1; i++)
                    {
                        bool changed = false;
                        foreach (Edge e in this.Edges)
                        {
                            int newDist = lengths[e.From] + e.Weight;
                            if (newDist < lengths[e.To])
                            {
                                lengths[e.To] = newDist;
                                parents[e.To] = e.From;
                                changed = true;
                            }
                        }

                        if (!changed)
                            break;
                    }

                    foreach (Edge e in this.Edges)
                        if (lengths[e.From] + e.Weight < lengths[e.To])
                            return false;

                    return true;
                }
            }
        }
    }
}