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
                /// Finds the shortest, weighted path between two vertices, if one exists. Assumes there are no negative-weight edges in the graph.
                /// </summary>
                /// <param name="from">The start of the path.</param>
                /// <param name="to">The end of the path.</param>
                /// <param name="path">The path that was found. <code>null</code> if there is no path.</param>
                /// <returns><code>true</code> if there is a path; <code>false</code> otherwise.</returns>
                public bool Dijkstra(uint from, uint to, out Path path)
                {
                    path = null;
                    DMinHeap<int, uint> fringe = new DMinHeap<int, uint>();
                    Dictionary<uint, uint> parents = new Dictionary<uint, uint>();
                    Dictionary<uint, int> dist = new Dictionary<uint, int>();

                    this.DijkstraInit(from, dist, fringe);

                    uint current;
                    while (fringe.Count > 0)
                    {
                        current = fringe.Extract();

                        if (current == to)
                        {
                            path = Path.FromParents(from, to, parents, this);
                            return true;
                        }

                        this.DijkstraRelax(current, dist, parents, fringe);
                    }

                    return false;
                }

                /// <summary>
                /// Find the shortest, weighted paths from a given vertex to all other vertices. Assumes there are no negative-weight edges in the graph.
                /// </summary>
                /// <param name="from">The vertex to calculate the shortest paths from.</param>
                /// <param name="lengths">The lengths of the shortest paths to all vertices.</param>
                /// <param name="parents">The parent-pointers for the paths to all vertices.</param>
                public void Dijkstra(uint from, out Dictionary<uint, int> lengths, out Dictionary<uint, uint> parents)
                {
                    DMinHeap<int, uint> fringe = new DMinHeap<int, uint>();
                    parents = new Dictionary<uint, uint>();
                    lengths = new Dictionary<uint, int>();

                    this.DijkstraInit(from, lengths, fringe);

                    uint current;
                    while (fringe.Count > 0)
                    {
                        current = fringe.Extract();
                        this.DijkstraRelax(current, lengths, parents, fringe);
                    }
                }

                private void DijkstraInit(uint from, Dictionary<uint, int> dist, DMinHeap<int, uint> fringe)
                {
                    foreach (uint v in this.Vertices.Keys)
                    {
                        if (v == from)
                            dist[v] = 0;
                        else
                            dist[v] = int.MaxValue;
                        fringe.Add(v, dist[v]);
                    }
                }

                private void DijkstraRelax(uint current, Dictionary<uint, int> dist, Dictionary<uint, uint> parents, DMinHeap<int, uint> fringe)
                {
                    foreach (Edge e in this.Vertices[current].Neighbours.Values)
                    {
                        if (!fringe.ContainsValue(e.To))
                            continue;

                        int newDist = dist[current] + e.Weight;
                        if (newDist < dist[e.To])
                        {
                            dist[e.To] = newDist;
                            parents[e.To] = current;
                            fringe.ChangeKey(e.To, newDist);
                        }
                    }
                }
            }
        }
    }
}