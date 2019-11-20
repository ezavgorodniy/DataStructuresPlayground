using System;
using System.Collections.Generic;
using System.Linq;

namespace MinimumSpanningTree
{
    public class DisjointSet
    {
        private readonly int[] _parent;
        private readonly int[] _rank;

        public DisjointSet(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Invalid amount of vertex", nameof(count));
            }

            _parent = new int[count];
            for (int i = 0; i < count; i++)
            {
                _parent[i] = i;
            }

            _rank = new int[count];
            for (int i = 0; i < count; i++)
            {
                _rank[i] = 0;
            }
        }

        public int Find(int i)
        {
            if (_parent[i] == i)
            {
                return i;
            }

            var res = Find(_parent[i]);
            _parent[i] = res;
            return res;
        }

        public void Union(int i, int j)
        {
            var iParent = Find(i);
            var iRank = _rank[i];

            var jParent = Find(j);
            var jRank = _rank[j];

            if (iParent == jParent)
            {
                return;
            }

            if (iRank < jRank)
            {
                _parent[iParent] = jParent;
            }
            else if (iRank > jRank)
            {
                _parent[jParent] = iParent;
            }
            else
            {
                _parent[iParent] = jParent;
                _rank[jParent]++;
            }
        }
    }

    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Value { get; set; }

        public Edge(int from, int to, int value)
        {
            From = from;
            To = to;
            Value = value;
        }
    }

    public abstract class Graph
    {
        public int VertexCount { get; }

        protected Graph(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Invalid amount of vertex", nameof(count));
            }

            VertexCount = count;

        }

        public abstract ICollection<Edge> GetAllEdges();

        public abstract ICollection<Edge> GetAdjacentEdges(int i);

        public abstract void AddEdge(int i, int j, int val);

        public virtual int[] GetVertexes()
        {
            var result = new int[VertexCount];
            for (var i = 0; i < VertexCount; i++)
            {
                result[i] = i;
            }
            return result;
        }
    }

    public class GraphListEdges : Graph
    {
        private IList<Edge> Edges { get; }

        public GraphListEdges(int count) : base(count)
        {
            Edges = new List<Edge>();
        }

        public override void AddEdge(int i, int j, int val)
        {
            Edges.Add(new Edge(i, j, val));
        }

        public override ICollection<Edge> GetAllEdges()
        {
            return Edges;
        }

        public override ICollection<Edge> GetAdjacentEdges(int i)
        {
            return Edges.Where(edge => edge.From == i || edge.To == i).ToArray();
        }
    }


    public class GraphListEdgesSorted : Graph
    {
        private List<Edge>[] Edges { get; }

        public GraphListEdgesSorted(int count) : base(count)
        {
            Edges = new List<Edge>[count];
            for (int i = 0; i < count; i++)
            {
                Edges[i] = new List<Edge>();

            }
        }

        public override void AddEdge(int i, int j, int val)
        {
            Edges[i].Add(new Edge(i, j, val));
            Edges[j].Add(new Edge(i, j, val));
        }

        public override ICollection<Edge> GetAllEdges()
        {
            return Edges.SelectMany(edge => edge.ToArray()).ToArray();
        }

        public override ICollection<Edge> GetAdjacentEdges(int i)
        {
            return Edges[i];
        }
    }

    public static class GraphHelpers
    {
        public static IList<Edge> Kruskal(Graph graph)
        {
            var minimumSpanTree = new List<Edge>();
            var set = new DisjointSet(graph.VertexCount);

            var edges = graph.GetAllEdges().ToArray();
            Array.Sort(edges, (edge, edge1) => edge.Value - edge1.Value);

            for (int i = 0; i < edges.Length; i++)
            {
                var edge = edges[i];

                var root1 = set.Find(edge.From);
                var root2 = set.Find(edge.To);
                if (root1 != root2)
                {
                    minimumSpanTree.Add(edge);
                    set.Union(root1, root2);
                }
            }

            return minimumSpanTree;
        }

        public static IList<Edge> Prim(Graph graph)
        {
            var minimumSpanTree = new List<Edge>();

            var vertexesList = new List<int>(new int[graph.VertexCount]);
            var parent = new Edge[graph.VertexCount];
            var keys = new int[graph.VertexCount];
            for (int i = 0; i < graph.VertexCount; i++)
            {
                parent[i] = null;
                keys[i] = int.MaxValue;
                vertexesList[i] = i;
            }

            keys[0] = 0;

            while (vertexesList.Count > 0)
            {
                int indexMin = int.MaxValue;
                int minVertex = int.MaxValue;
                int minValue = int.MaxValue;
                for (int i = 0; i < vertexesList.Count; i++)
                {
                    if (keys[vertexesList[i]] < minValue)
                    {
                        indexMin = i;
                        minValue = keys[vertexesList[i]];
                        minVertex = vertexesList[i];
                    }
                }

                vertexesList.RemoveAt(indexMin);

                if (indexMin != int.MaxValue)
                {
                    if (parent[minVertex] != null)
                    {
                        minimumSpanTree.Add(parent[minVertex]);
                    }

                    var edges = graph.GetAdjacentEdges(minVertex);
                    foreach (var edge in edges)
                    {
                        var fromIndex = edge.From == minVertex ? edge.To : edge.From;
                        if (edge.Value < keys[fromIndex])
                        {
                            parent[fromIndex] = edge;
                            keys[fromIndex] = edge.Value;
                        }
                    }
                }

            }

            return minimumSpanTree;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //var graph = new GraphListEdges(6);
            //graph.AddEdge(0, 1, 4);
            //graph.AddEdge(0, 5, 2);
            //graph.AddEdge(1, 2, 6);
            //graph.AddEdge(1, 5, 5);
            //graph.AddEdge(2, 3, 3);
            //graph.AddEdge(2, 5, 1);
            //graph.AddEdge(3, 4, 2);
            //graph.AddEdge(5, 4, 4);

            //var mst = GraphHelpers.Kruskal(graph);
            //foreach (var edge in mst)
            //{
            //    // Console.WriteLine("({0}-{1})", ((char)(edge.From + 'a')), ((char)(edge.To + 'a')));
            //    Console.WriteLine("({0}-{1})", edge.From, edge.To);
            //}


            var graph = new GraphListEdges(6);
            graph.AddEdge(0, 1, 4);
            graph.AddEdge(0, 5, 2);
            graph.AddEdge(1, 2, 6);
            graph.AddEdge(1, 5, 3);
            graph.AddEdge(2, 3, 3);
            graph.AddEdge(2, 5, 1);
            graph.AddEdge(3, 4, 2);
            graph.AddEdge(5, 4, 4);

            var mst = GraphHelpers.Kruskal(graph);
            foreach (var edge in mst)
            {
                Console.WriteLine("({0}-{1})", ((char)(edge.From + 'a')), ((char)(edge.To + 'a')));
                // Console.WriteLine("({0}-{1})", edge.From, edge.To);
            }
        }
    }
}
