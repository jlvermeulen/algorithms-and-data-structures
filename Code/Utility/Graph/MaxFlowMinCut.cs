using System;
using System.Collections.Generic;

namespace Utility
{
    namespace Algorithms
    {
        namespace Graph
        {
            /// <summary>
            /// Static class containing graph algorithms.
            /// </summary>
            public static partial class Graph
            {
                /// <summary>
                /// Returns the lightest set of edges that would divide the graph in two components should they be removed.
                /// </summary>
                /// <param name="graph">The flow graph to calculate the minimum cut of.</param>
                /// <param name="source">The source of the flow.</param>
                /// <param name="sink">The sink of the flow.</param>
                /// <returns>A list containing the edges that make up the minimum cut.</returns>
                public static List<IFlowGraphEdge> MinCut(IGraph<IFlowGraphEdge> graph, uint source, uint sink)
                {
                    Dictionary<uint, Dictionary<uint, FlowEdge>> flowGraph = BuildFlowGraph(graph);
                    MaxFlow(flowGraph, source, sink);

                    List<IFlowGraphEdge> cut = new List<IFlowGraphEdge>();
                    HashSet<uint> reachable = new HashSet<uint>();
                    Queue<uint> open = new Queue<uint>();

                    open.Enqueue(source);
                    reachable.Add(source);
                    while (open.Count > 0)
                    {
                        uint i = open.Dequeue();
                        foreach (uint j in flowGraph[i].Keys)
                            if (!reachable.Contains(j) && flowGraph[i][j].Residual > 0)
                            {
                                open.Enqueue(j);
                                reachable.Add(j);
                            }
                    }

                    foreach (uint i in reachable)
                    {
                        foreach (uint j in flowGraph[i].Keys)
                            if (!reachable.Contains(j))
                                cut.Add(flowGraph[i][j].Original);
                    }

                    return cut;
                }

                /// <summary>
                /// Returns the maximum amount of flow that can move from the source to the sink.
                /// </summary>
                /// <param name="graph">The flow graph to calculate the maximum flow of.</param>
                /// <param name="source">The source of the flow.</param>
                /// <param name="sink">The sink of the flow.</param>
                /// <returns>The maximum amount of flow that can move from the source to the sink.</returns>
                public static ulong MaxFlow(IGraph<IFlowGraphEdge> graph, uint source, uint sink)
                {
                    return MaxFlow(BuildFlowGraph(graph), source, sink);
                }

                private static Dictionary<uint, Dictionary<uint, FlowEdge>> BuildFlowGraph(IGraph<IFlowGraphEdge> graph)
                {
                    Dictionary<uint, Dictionary<uint, FlowEdge>> flowGraph = new Dictionary<uint, Dictionary<uint, FlowEdge>>();
                    Dictionary<uint, FlowEdge> dict;

                    foreach (IGraphNode<IFlowGraphEdge> n in graph.Nodes.Values)
                        foreach (IFlowGraphEdge e in n.Neighbours)
                        {
                            if (!flowGraph.TryGetValue(e.From, out dict))
                            {
                                dict = new Dictionary<uint, FlowEdge>();
                                flowGraph.Add(e.From, dict);
                            }
                            dict.Add(e.To, new FlowEdge(e.From, e.To, e.Capacity, e));

                            if (!flowGraph.TryGetValue(e.To, out dict))
                            {
                                dict = new Dictionary<uint, FlowEdge>();
                                flowGraph.Add(e.To, dict);
                            }
                            dict.Add(e.From, new FlowEdge(e.To, e.From, e.Capacity, e));
                        }

                    return flowGraph;
                }

                private static ulong MaxFlow(Dictionary<uint, Dictionary<uint, FlowEdge>> neighbours, uint source, uint sink)
                {
                    FlowNode path;
                    ulong maxFlow = 0;
                    uint flow;
                    FlowEdge e1, e2;
                    while (BFS(neighbours, source, sink, out path))
                    {
                        flow = path.Capacity;
                        maxFlow += flow;
                        while (path.Parent != null)
                        {
                            e1 = neighbours[path.Parent.Node][path.Node];
                            e2 = neighbours[path.Node][path.Parent.Node];
                            e1.Residual -= flow;
                            e2.Residual += flow;
                            path = path.Parent;
                        }
                    }
                    return maxFlow;
                }

                private static bool BFS(Dictionary<uint, Dictionary<uint, FlowEdge>> neighbours, uint source, uint sink, out FlowNode path)
                {
                    path = null;
                    Queue<FlowNode> open = new Queue<FlowNode>();
                    open.Enqueue(new FlowNode(source, null, int.MaxValue));

                    HashSet<uint> done = new HashSet<uint>();
                    done.Add(source);

                    FlowNode node;
                    while (open.Count > 0)
                    {
                        node = open.Dequeue();
                        foreach (uint i in neighbours[node.Node].Keys)
                        {
                            if (done.Contains(i) || neighbours[node.Node][i].Residual == 0)
                                continue;
                            if (i == sink)
                            {
                                path = new FlowNode(i, node, Math.Min(neighbours[node.Node][i].Residual, node.Capacity));
                                return true;
                            }
                            open.Enqueue(new FlowNode(i, node, Math.Min(neighbours[node.Node][i].Residual, node.Capacity)));
                            done.Add(i);
                        }
                    }

                    return false;
                }

                private class FlowEdge
                {
                    public FlowEdge(uint node1, uint node2, uint capacity, IFlowGraphEdge original)
                    {
                        this.Node1 = node1;
                        this.Node2 = node2;
                        this.Capacity = capacity;
                        this.Residual = capacity;
                        this.Original = original;
                    }

                    public uint Node1 { get; private set; }
                    public uint Node2 { get; private set; }
                    public uint Capacity { get; private set; }
                    public uint Residual { get; set; }
                    public IFlowGraphEdge Original { get; private set; }
                }

                private class FlowNode
                {
                    public FlowNode(uint node, FlowNode parent, uint capacity)
                    {
                        this.Node = node;
                        this.Parent = parent;
                        this.Capacity = capacity;
                    }

                    public uint Node { get; private set; }
                    public FlowNode Parent { get; private set; }
                    public uint Capacity { get; private set; }
                }
            }
        }
    }
}