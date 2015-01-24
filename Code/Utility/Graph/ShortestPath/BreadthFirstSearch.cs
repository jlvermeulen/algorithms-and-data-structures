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
                /// Finds the shortest, unweighted path between two vertices, if one exists.
                /// </summary>
                /// <param name="from">The start of the path.</param>
                /// <param name="end">The end of the path.</param>
                /// <param name="path">The path that was found. <code>null</code> if there is no path.</param>
                /// <returns><code>true</code> if there is a path; <code>false</code> otherwise.</returns>
                public bool BreadthFirstSearch(uint from, uint to, out Path path)
                {
                    path = null;
                    Queue<uint> fringe = new Queue<uint>();
                    fringe.Enqueue(from);

                    Dictionary<uint, uint> parents = new Dictionary<uint, uint>();
                    HashSet<uint> closed = new HashSet<uint>();
                    closed.Add(from);

                    while (fringe.Count > 0)
                    {
                        uint node = fringe.Dequeue();
                        foreach (Edge e in this.Vertices[node].Neighbours.Values)
                        {
                            if (closed.Contains(e.To))
                                continue;
                            else if (e.To == to)
                            {
                                parents[to] = node;
                                path = Path.FromParents(from, to, parents, this);
                                return true;
                            }
                            fringe.Enqueue(e.To);
                            parents[e.To] = e.From;
                            closed.Add(e.To);
                        }
                    }

                    return false;
                }
            }
        }
    }
}