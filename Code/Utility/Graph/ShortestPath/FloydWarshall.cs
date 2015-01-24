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
                /// Finds the lengths of the shortest paths between all pairs of vertices in the graph, if no negative cycles are present.
                /// </summary>
                /// <returns>The lengths of the shortest paths between all pairs of vertices if no negative cycles are present; <code>null</code> otherwise.</returns>
                public Dictionary<uint, Dictionary<uint, int>> FloydWarshall()
                {
                    Dictionary<uint, Dictionary<uint, int>> lengths;
                    Dictionary<uint, Dictionary<uint, uint>> parents;
                    if (!this.FloydWarshall(out lengths, out parents))
                        return null;
                    return lengths;
                }

                /// <summary>
                /// Finds the shortest path between all pairs of vertices in the graph, if no negative cycles are present.
                /// </summary>
                /// <param name="lengths">The lengths of the shortest paths between all pairs of vertices.</param>
                /// <param name="parents">The parent-pointers for the paths between all pairs of vertices.</param>
                /// <returns><code>true</code> if there are no negative cycles; <code>false</code> otherwise.</returns>
                public bool FloydWarshall(out Dictionary<uint, Dictionary<uint, int>> lengths, out Dictionary<uint, Dictionary<uint, uint>> parents)
                {
                    lengths = new Dictionary<uint, Dictionary<uint, int>>();
                    parents = new Dictionary<uint, Dictionary<uint, uint>>();

                    foreach (Vertex x in this.Vertices.Values)
                    {
                        uint u = x.ID;
                        lengths[u] = new Dictionary<uint, int>();
                        parents[u] = new Dictionary<uint, uint>();
                        foreach (uint v in this.Vertices.Keys)
                            lengths[u][v] = int.MaxValue;

                        lengths[u][u] = 0;
                        foreach (Edge e in x.Neighbours.Values)
                        {
                            lengths[u][e.To] = e.Weight;
                            parents[u][e.To] = e.To;
                        }
                    }

                    foreach (uint k in this.Vertices.Keys)
                        foreach (uint i in this.Vertices.Keys)
                            foreach (uint j in this.Vertices.Keys)
                            {
                                int ik = lengths[i][k];
                                if (ik == int.MaxValue)
                                    continue;

                                int kj = lengths[k][j];
                                if (kj == int.MaxValue)
                                    continue;

                                int newDist = ik + kj;
                                if (newDist < lengths[i][j])
                                {
                                    lengths[i][j] = newDist;
                                    parents[i][j] = parents[i][k];
                                }
                            }

                    foreach (uint v in this.Vertices.Keys)
                        if (lengths[v][v] < 0)
                            return false;

                    return true;
                }
            }
        }
    }
}