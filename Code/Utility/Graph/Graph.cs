using System;
using System.Collections.Generic;

namespace Utility
{
    namespace Algorithms
    {
        namespace Graph
        {
            /// <summary>
            /// Represents a variety of graph types.
            /// </summary>
            public partial class Graph
            {
                /// <summary>
                /// The set of vertices in the graph, indexed by ID.
                /// </summary>
                public Dictionary<uint, Vertex> Vertices = new Dictionary<uint, Vertex>();

                /// <summary>
                /// The set of edges in the graph.
                /// </summary>
                public List<Edge> Edges = new List<Edge>();

                /// <summary>
                /// Add a vertex to the graph.
                /// </summary>
                /// <param name="id">The ID of the vertex to be added.</param>
                public void AddVertex(uint id) { this.Vertices.Add(id, new Vertex(id)); }

                /// <summary>
                /// Add an edge to the graph.
                /// </summary>
                /// <param name="from">The ID of the first endpoint of the edge. A new vertex will be created if it does not exist.</param>
                /// <param name="to">The ID of the second endpoint of the edge. A new vertex will be created if it does not exist.</param>
                /// <param name="directed">Indicates whether this edge is directed or not.</param>
                /// <param name="weight">The weight of the edge.</param>
                /// <param name="capacity">The capacity of the edge.</param>
                public void AddEdge(uint from, uint to, bool directed = false, int weight = 1, uint capacity = 1)
                {
                    Vertex v1, v2;
                    if (!this.Vertices.TryGetValue(from, out v1))
                    {
                        v1 = new Vertex(from);
                        this.Vertices[from] = v1;
                    }
                    if (!this.Vertices.TryGetValue(to, out v2))
                    {
                        v2 = new Vertex(to);
                        this.Vertices[to] = v2;
                    }

                    Edge e1 = new Edge(from, to, weight, capacity);
                    v1.Neighbours.Add(to, e1);
                    this.Edges.Add(e1);
                    if (!directed)
                    {
                        Edge e2 = new Edge(to, from, weight, capacity);
                        v2.Neighbours.Add(from, e2);
                        this.Edges.Add(e2);
                    }
                }

                /// <summary>
                /// Removes a vertex from the graph.
                /// </summary>
                /// <param name="id">The ID of the vertex to be removed.</param>
                /// <returns><code>true</code> if the vertex was successfully removed; <code>false</code> if it does not exist.</returns>
                public bool RemoveVertex(uint id)
                {
                    if (!this.Vertices.ContainsKey(id))
                        return false;

                    this.Vertices.Remove(id);

                    List<Edge> newEdges = new List<Edge>();
                    foreach (Edge e in this.Edges)
                        if (e.From == id)
                            continue;
                        else if (e.To == id)
                            this.Vertices[e.From].Neighbours.Remove(id);
                        else
                            newEdges.Add(e);

                    this.Edges = newEdges;
                    return true;
                }

                /// <summary>
                /// Test whether the graph contains a vertex with a specific ID.
                /// </summary>
                /// <param name="id">The ID of the vertex to be found.</param>
                /// <returns><code>true</code> if the vertex exists; <code>false</code>otherwise.</returns>
                public bool HasVertex(uint id) { return this.Vertices.ContainsKey(id); }

                /// <summary>
                /// Test whether the graph contains an edge between the two specified endpoints.
                /// </summary>
                /// <param name="from">The ID of the first endpoint of the edge to be found.</param>
                /// <param name="to">The ID of the second endpoint of the edge to be found.</param>
                /// <returns><code>true</code> if the edge exists; <code>false</code> otherwise.</returns>
                public bool HasEdge(uint from, uint to) { return this.Vertices.ContainsKey(from) && this.Vertices[from].Neighbours.ContainsKey(to); }
            }

            /// <summary>
            /// Represents a vertex of a graph.
            /// </summary>
            public class Vertex
            {
                /// <summary>
                /// The ID of the vertex.
                /// </summary>
                public uint ID;

                /// <summary>
                /// The neighbours of the vertex.
                /// </summary>
                public Dictionary<uint, Edge> Neighbours;

                /// <summary>
                /// Initialises a new instance of the Vertex class with the specified ID.
                /// </summary>
                /// <param name="id">The ID of the new vertex.</param>
                public Vertex(uint id) : this(id, new Dictionary<uint, Edge>()) { }

                /// <summary>
                /// Initialises a new instance of the Vertex class with the specified ID and neighbours.
                /// </summary>
                /// <param name="id">The ID of the new vertex.</param>
                /// <param name="neighbours">The neighbours of the new vertex.</param>
                public Vertex(uint id, Dictionary<uint, Edge> neighbours) { this.ID = id; this.Neighbours = neighbours; }
            }

            /// <summary>
            /// Represents an edge of a graph.
            /// </summary>
            public class Edge
            {
                /// <summary>
                /// The ID of the first endpoint of the edge.
                /// </summary>
                public uint From;

                /// <summary>
                /// The ID of the second endpoint of the edge.
                /// </summary>
                public uint To;

                /// <summary>
                /// The capacity of the edge.
                /// </summary>
                public uint Capacity;

                /// <summary>
                /// The weight of the edge.
                /// </summary>
                public int Weight;


                /// <summary>
                /// Initialises a new instance of the Edge class with the specified endpoints, weight and capacity.
                /// </summary>
                /// <param name="from">The ID of the first endpoint of the new edge.</param>
                /// <param name="to">The ID of the second endpoint of the new edge.</param>
                /// <param name="weight">The weight of the new edge.</param>
                /// <param name="capacity">The capacity of the new edge.</param>
                public Edge(uint from, uint to, int weight, uint capacity)
                {
                    this.From = from;
                    this.To = to;
                    this.Weight = weight;
                    this.Capacity = capacity;
                }
            }
        }
    }
}