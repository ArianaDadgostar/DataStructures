using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightedDirectedGraph;

namespace GraphTests
{
    public class PathFindingTesting
    {
        public static IEnumerable<object[]> AStarTestData()
        {
            yield return new object[] { 6,
                                    new (int, int, int)[] { (1, 3, 9), (1, 6, 14), (1, 2, 7), (2, 3, 10), (2, 4, 15), (4, 5, 6), (3, 6, 2), (6, 5, 9) },
                                    new int[] { 1, 3, 6, 5 } };
        }

        [Theory]
        [MemberData(nameof(AStarTestData))]
        public void FindShortestPathTest(int vAmount, (int v1, int v2, int w)[] array, int[] finalPath)
        {
            Graph<int> graph = new Graph<int>();

            for (int i = 0; i < vAmount; i++)
            {
                Vertex<int> vertex = new Vertex<int>(i);
                graph.AddVertex(vertex);
            }

            for (int i = 0; i < array.Length; i++)
            {
                graph.AddEdge(graph.Vertices[array[i].v1 - 1], graph.Vertices[array[i].v2 - 1], array[i].w);
            }

            List<Vertex<int>> path = graph.FindShortestPath(graph.Vertices[0], graph.Vertices[4]);

            for (int i = 0; i < path.Count; i++)
            {
                Assert.True(path[i].Value == finalPath[(path.Count - 1) - i] - 1);
            }
        }

        //[Theory]
        //[MemberData(nameof(AStarTestData))]
        //public void FindShortestPath_ASTAR_Test(int vAmount, (int v1, int v2, int w)[] array, int[] finalPath)
        //{
        //    Graph<int> graph = new AStarPathFindingVisualizer.Graph<int>();

        //    for (int i = 0; i < vAmount; i++)
        //    {
        //        Vertex<int> vertex = new Vertex<int>(i);
        //        graph.AddVertex(vertex);
        //    }

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        graph.AddEdge(graph.Vertices[array[i].v1 - 1], graph.Vertices[array[i].v2 - 1], array[i].w);
        //    }

        //    List<Vertex<int>> path = graph.FindShortestPath(graph.Vertices[0], graph.Vertices[4]);

        //    for (int i = 0; i < path.Count; i++)
        //    {
        //        Assert.True(path[i].Value == finalPath[(path.Count - 1) - i] - 1);
        //    }
        //}
    }
}
