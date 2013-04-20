using System;
using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// Represents a graph by its nodes and edges.
    /// </summary>
    /// <typeparam name="T">The type of edges this graph has.</typeparam>
    public interface IGraph<T>
        where T : IGraphEdge
    {
        /// <summary>
        /// The nodes in the graph.
        /// </summary>
        Dictionary<uint, IGraphNode<T>> Nodes { get; }
        /// <summary>
        /// The edges in the graph.
        /// </summary>
        Dictionary<uint, Dictionary<uint, T>> Edges { get; }
    }

    /// <summary>
    /// Interface for graph nodes.
    /// </summary>
    /// <typeparam name="T">The type of edges this node connects to.</typeparam>
    public interface IGraphNode<T>
        where T : IGraphEdge
    {
        /// <summary>
        /// The unique identifier for this node.
        /// </summary>
        uint ID { get; }
        /// <summary>
        /// Edges from this node to neighbours.
        /// </summary>
        IEnumerable<T> Neighbours { get; }
    }

    /// <summary>
    /// Interface for graph edges.
    /// </summary>
    public interface IGraphEdge
    {
        /// <summary>
        /// Starting point of this edge.
        /// </summary>
        uint From { get; }
        /// <summary>
        /// Endpoint of this edge.
        /// </summary>
        uint To { get; }
    }

    /// <summary>
    /// Interface for weighted graph edges.
    /// </summary>
    public interface IWeightedGraphEdge : IGraphEdge
    {
        /// <summary>
        /// The weight associated with this edge.
        /// </summary>
        uint Weight { get; }
    }

    /// <summary>
    /// Interface for graph edges with flow capacity.
    /// </summary>
    public interface IFlowGraphEdge : IGraphEdge
    {
        /// <summary>
        /// The flow capacity of this edge.
        /// </summary>
        uint Capacity { get; }
    }
}