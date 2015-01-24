using System;
using System.Collections.Generic;

namespace Utility
{
    namespace Algorithms
    {
        namespace Graph
        {
            /// <summary>
            /// Represents a path through a graph.
            /// </summary>
            public class Path
            {
                /// <summary>
                /// The edges the path traverses, in order.
                /// </summary>
                public List<Edge> Edges = new List<Edge>();

                /// <summary>
                /// The vertices the path traverses, in order.
                /// </summary>
                public List<Vertex> Vertices = new List<Vertex>();

                /// <summary>
                /// The total weight of all edges on the path.
                /// </summary>
                public int Weight = 0;

                /// <summary>
                /// The minimum capacity of all edges on the path.
                /// </summary>
                public uint Capacity = uint.MaxValue;

                /// <summary>
                /// Generate a path from a set of single-source parent pointers.
                /// </summary>
                /// <param name="from">The start of the path.</param>
                /// <param name="to">The end of the path.</param>
                /// <param name="parents">The set of single-source parent pointers.</param>
                /// <param name="graph">The graph to find the path in.</param>
                /// <returns>The path between <paramref name="from"/> and <paramref name="to"/>, if one exists; <code>null</code> otherwise.</returns>
                public static Path FromParents(uint from, uint to, Dictionary<uint, uint> parents, Graph graph)
                {
                    if (!parents.ContainsKey(to))
                        return null;

                    Path path = new Path();

                    path.Vertices.Add(graph.Vertices[to]);
                    uint current = to, parent;
                    while (parents.TryGetValue(current, out parent))
                    {
                        Vertex v = graph.Vertices[parent];
                        Edge e = v.Neighbours[current];
                        path.Edges.Add(e);
                        path.Vertices.Add(v);
                        path.Weight += e.Weight;
                        path.Capacity = Math.Min(path.Capacity, e.Capacity);
                        current = parent;
                    }

                    path.Edges.Reverse();
                    path.Vertices.Reverse();

                    return path;
                }

                /// <summary>
                /// Generate a path from a set of all-pairs parent pointers.
                /// </summary>
                /// <param name="from">The start of the path.</param>
                /// <param name="to">The end of the path.</param>
                /// <param name="parents">The set of all-pairs parent pointers.</param>
                /// <param name="graph">The graph to find the path in.</param>
                /// <returns>The path between <paramref name="from"/> and <paramref name="to"/>, if one exists; <code>null</code> otherwise.</returns>
                public static Path FromParents(uint from, uint to, Dictionary<uint, Dictionary<uint, uint>> parents, Graph graph)
                {
                    if (!parents[from].ContainsKey(to))
                        return null;

                    Path path = new Path();
                    path.Vertices.Add(graph.Vertices[from]);
                    uint next;
                    while (from != to)
                    {
                        next = parents[from][to];

                        Edge e = graph.Vertices[from].Neighbours[next];
                        path.Edges.Add(e);
                        path.Vertices.Add(graph.Vertices[next]);                        
                        path.Weight += e.Weight;
                        path.Capacity = Math.Min(path.Capacity, e.Capacity);

                        from = next;
                    }

                    return path;
                }
            }
        }
    }
}