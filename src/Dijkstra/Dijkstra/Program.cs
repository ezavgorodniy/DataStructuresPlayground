using System.Collections;
using System.Collections.Generic;

namespace Dijkstra
{
    // A C# program for Dijkstra's single  
    // source shortest path algorithm. 
    // The program is for adjacency matrix 
    // representation of the graph 
    using System;


    public class DijkstraResult
    {
        public int NodeNumber { get; set; }

        public int[] Distances { get; }

        public int[] Predecessors { get; }

        public DijkstraResult(int nodesCount)
        {
            Predecessors = new int[nodesCount];
            Distances = new int[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                Distances[i] = int.MaxValue;
            }
        }
    }

    public class Graph
    {
        private readonly int[][] _matrix;

        public Graph(int[][] graph)
        {
            _matrix = graph ?? throw new ArgumentNullException(nameof(graph));
        }

        public DijkstraResult Dijkstra(int nodeNumber)
        {
            var nodesCount = _matrix.Length;

            var result = new DijkstraResult(nodesCount);
            var visited = new bool[nodesCount];
            visited[nodeNumber] = true;
            for (int i = 0; i < nodesCount; i++)
            {
                result.Distances[i] = _matrix[nodeNumber][i] != 0 ? _matrix[nodeNumber][i] : int.MaxValue;
            }
            result.Distances[nodeNumber] = 0;

            var processedVertexes = 1;
            while (processedVertexes < nodesCount)
            {
                var currentMin = int.MaxValue;
                var currentMinNodeNumber = -1;
                for (int i = 0; i < nodesCount; i++)
                {
                    if (!visited[i] && result.Distances[i] < currentMin)
                    {
                        currentMin = result.Distances[i];
                        currentMinNodeNumber = i;
                    }
                }

                for (int i = 0; i < nodesCount; i++)
                {
                    if (_matrix[currentMinNodeNumber][i] == 0)
                    {
                        continue;
                    }

                    var newDistance = currentMin + _matrix[currentMinNodeNumber][i];
                    if (newDistance < result.Distances[i])
                    {
                        result.Distances[i] = newDistance;
                        result.Predecessors[i] = currentMinNodeNumber;
                    }
                }

                visited[currentMinNodeNumber] = true;

                processedVertexes++;
            }

            return result;
        }
    }


    class GFG
    {
        // A utility function to find the  
        // vertex with minimum distance 
        // value, from the set of vertices 
        // not yet included in shortest  
        // path tree 
        private int MinDistance(int[] dist,
                        bool[] sptSet)
        {
            // Initialize min value 
            var min = int.MaxValue;
            var minIndex = -1;

            for (int v = 0; v < sptSet.Length; v++)
            {
                if (!sptSet[v] && dist[v] <= min)
                {
                    min = dist[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        // A utility function to print 
        // the constructed distance array 
        private void PrintSolution(int[] dist, int n)
        {
            Console.Write("Vertex     Distance " +
                          "from Source\n");
            for (int i = 0; i < n; i++)
            {
                Console.Write(i + " \t\t " + dist[i] + "\n");
            }
        }

        // Funtion that implements Dijkstra's  
        // single source shortest path algorithm 
        // for a graph represented using adjacency  
        // matrix representation 
        public void Dijkstra(int[][] graph, int src)
        {
            var dist = new int[graph.Length]; // The output array. dist[i] 
                                     // will hold the shortest  
                                     // distance from src to i 

            // sptSet[i] will true if vertex 
            // i is included in shortest path  
            // tree or shortest distance from  
            // src to i is finalized 
            var sptSet = new bool[graph.Length];

            // Initialize all distances as  
            // INFINITE and stpSet[] as false 
            for (int i = 0; i < graph.Length; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            // Distance of source vertex 
            // from itself is always 0 
            dist[src] = 0;

            // Find shortest path for all vertices 
            for (int count = 0; count < graph.Length - 1; count++)
            {
                // Pick the minimum distance vertex  
                // from the set of vertices not yet  
                // processed. u is always equal to  
                // src in first iteration. 
                var u = MinDistance(dist, sptSet);

                // Mark the picked vertex as processed 
                sptSet[u] = true;

                // Update dist value of the adjacent  
                // vertices of the picked vertex. 
                for (int v = 0; v < graph.Length; v++)
                {
                    // Update dist[v] only if is not in  
                    // sptSet, there is an edge from u  
                    // to v, and total weight of path  
                    // from src to v through u is smaller  
                    // than current value of dist[v] 
                    if (!sptSet[v] && graph[u][v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u][v] < dist[v])
                    {
                        dist[v] = dist[u] + graph[u][v];
                    }
                }
            }

            // print the constructed distance array 
            PrintSolution(dist, graph.Length);
        }
    }

    // This code is contributed by ChitraNayal 

    class Program
    {
        static void Main(string[] args)
        {
            /* Let us create the example  
            graph discussed above */
            //int[][] graph = {
            //    new [] {0, 4, 0, 0, 0, 0, 0, 8, 0},
            //    new [] {4, 0, 8, 0, 0, 0, 0, 11, 0},
            //    new [] {0, 8, 0, 7, 0, 4, 0, 0, 2},
            //    new [] {0, 0, 7, 0, 9, 14, 0, 0, 0},
            //    new [] {0, 0, 0, 9, 0, 10, 0, 0, 0},
            //    new [] {0, 0, 4, 14, 10, 0, 2, 0, 0},
            //    new [] {0, 0, 0, 0, 0, 2, 0, 1, 6},
            //    new [] {8, 11, 0, 0, 0, 0, 1, 0, 7},
            //    new [] {0, 0, 2, 0, 0, 0, 6, 7, 0}};

            int[][] graphDef = {
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0},
                new [] {0, 0, 0, 0, 0, 0, 0, 0, 0}};

            graphDef[0][1] = 4;
            graphDef[0][7] = 8;

            graphDef[1][0] = 4;
            graphDef[1][2] = 8;
            graphDef[1][7] = 11;

            graphDef[2][1] = 8;
            graphDef[2][3] = 7;
            graphDef[2][5] = 4;
            graphDef[2][8] = 2;

            graphDef[3][2] = 7;
            graphDef[3][4] = 9;
            graphDef[3][5] = 14;

            graphDef[4][3] = 9;
            graphDef[4][5] = 10;

            graphDef[5][2] = 4;
            graphDef[5][3] = 14;
            graphDef[5][4] = 10;

            graphDef[6][7] = 1;
            graphDef[6][8] = 6;

            graphDef[7][8] = 7;

            graphDef[8][2] = 2;
            graphDef[8][6] = 6;
            graphDef[8][7] = 7;

            var cfg = new GFG();
            cfg.Dijkstra(graphDef, 0);


            var graph = new Graph(graphDef);
            var dijkstra = graph.Dijkstra(0);

        }
    }
}
