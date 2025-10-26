using NuGet.Frameworks;
using System.Linq;
using System.Numerics;
using WeightedDirectedGraph;

namespace GraphTests;

public class WeightedDirectedGraphTesting
{
    #region BasicFunctions

    [Theory]
    [InlineData(new int[] { 3, 2, 1, 3, 4 })]
    [InlineData(new int[] { 5, 1, 5, 9 })]
    public void AddVertexTest(params int[] array)
    {
        Graph<int> graph = new Graph<int>();

        foreach (int i in array)
        {
            Vertex<int> vertex = new Vertex<int>(i);
            graph.AddVertex(vertex);
        }

        Vertex<int> nullVertex = null;
        bool nullResult = graph.AddVertex(nullVertex);

        Assert.True(nullResult == false);
        Assert.True(graph.VertexCount == array.Length - 1);
    }

    [Theory]
    [InlineData(new int[] { 3, 2, 1, 3, 4 })]
    [InlineData(new int[] { 5, 1, 5, 9 })]
    public void RemoveVertexTest(params int[] array)
    {
        Graph<int> graph = new Graph<int>();

        foreach (int i in array)
        {
            Vertex<int> vertex = new Vertex<int>(i);
            graph.AddVertex(vertex);
        }

        Vertex<int> removed = graph.Vertices[0];
        graph.RemoveVertex(removed);

        foreach (Vertex<int> vertex in graph.Vertices)
        {
            Assert.True(removed.Value != vertex.Value);
        }

        Vertex<int> falseTest = new Vertex<int>(7);
        Assert.True(graph.RemoveVertex(falseTest) == false);
    }

    [Theory]
    [InlineData(new int[] { 3, 2, 1, 3, 4 })]
    [InlineData(new int[] { 5, 1, 5, 9 })]
    public void AddEdgesTest(params int[] array)
    {
        Graph<int> graph = new Graph<int>();

        foreach (int i in array)
        {
            Vertex<int> vertex = new Vertex<int>(i);
            graph.AddVertex(vertex);
        }

        graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 10);

        Assert.Contains(graph.Vertices[0].Neighbors, x => x.EndingPoint.Equals(graph.Vertices[1]));

        Vertex<int> falseTest = new Vertex<int>(10);
        bool result = graph.AddEdge(graph.Vertices[0], falseTest, 5);

        Assert.True(result == false);
    }

    [Theory]
    [InlineData(new int[] { 3, 2, 1, 3, 4 })]
    [InlineData(new int[] { 5, 1, 5, 9 })]
    public void RemoveEdgesTest(params int[] array)
    {
        Graph<int> graph = new Graph<int>();

        foreach (int i in array)
        {
            Vertex<int> vertex = new Vertex<int>(i);
            graph.AddVertex(vertex);
        }

        graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 10);
        graph.RemoveEdge(graph.Vertices[0], graph.Vertices[1]);

        for (int i = 0; i < graph.Vertices[0].NeighborCount; i++)
        {
            Assert.True(graph.Vertices[0].Neighbors[i].EndingPoint == graph.Vertices[1]);
        }

        for (int i = 0; i < graph.Vertices[1].NeighborCount; i++)
        {
            Assert.True(graph.Vertices[1].Neighbors[i].EndingPoint == graph.Vertices[0]);
        }

        bool result = graph.RemoveEdge(graph.Vertices[0], graph.Vertices[2]);

        Assert.True(result == false);
    }

    [Theory]
    [InlineData(new int[] { 3, 2, 1, 3, 4 })]
    [InlineData(new int[] { 5, 1, 5, 9 })]
    public void SearchTest(params int[] array)
    {
        Graph<int> graph = new Graph<int>();

        foreach (int i in array)
        {
            Vertex<int> vertex = new Vertex<int>(i);
            graph.AddVertex(vertex);
        }

        Vertex<int> result = graph.Search(graph.Vertices[0].Value);
        Assert.True(result != null && result.Value == graph.Vertices[0].Value);

        Vertex<int> falseResult = graph.Search(0);
        Assert.True(falseResult == null);
    }

    [Theory]
    [InlineData(new int[] { 3, 2, 1, 3, 4 })]
    [InlineData(new int[] { 5, 1, 5, 9, 10 })]
    public void GetEdgeTest(params int[] array)
    {
        Graph<int> graph = new Graph<int>();

        foreach (int i in array)
        {
            Vertex<int> vertex = new Vertex<int>(i);
            graph.AddVertex(vertex);
        }

        graph.AddEdge(graph.Vertices[0], graph.Vertices[3], 10);

        Assert.True(graph.GetEdge(graph.Vertices[0], graph.Vertices[3]) == graph.Vertices[0].Neighbors[0]);
        Assert.True(graph.GetEdge(graph.Vertices[0], graph.Vertices[1]) == null);
    }

    #endregion

    #region Traversals

    [Theory]
    [InlineData(new int[] { 3, 2, 1, 3, 4 })]
    [InlineData(new int[] { 5, 1, 5, 9, 11 })]
    public void RecursivelyDepthFirstTraversalTest(params int[] array)
    {
        Graph<int> graph = new Graph<int>();

        foreach (int i in array)
        {
            Vertex<int> vertex = new Vertex<int>(i);
            graph.AddVertex(vertex);
        }
        graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 10);
        graph.AddEdge(graph.Vertices[0], graph.Vertices[2], 10);
        graph.AddEdge(graph.Vertices[1], graph.Vertices[3], 10);

        List<Vertex<int>> traversal = graph.DepthFirstTraversal(graph.Vertices[0]);

        Assert.True(traversal.Count == 4);

        for (int i = 0; i < 4; i++)
        {
            Assert.True(traversal.Contains(graph.Vertices[i]));
        }
    }

    [Theory]
    [InlineData(new int[] { 3, 2, 1, 3, 4 })]
    [InlineData(new int[] { 5, 1, 5, 9, 11 })]
    public void BreadthFirstTraversalTest(params int[] array)
    {
        Graph<int> graph = new Graph<int>();

        foreach (int i in array)
        {
            Vertex<int> vertex = new Vertex<int>(i);
            graph.AddVertex(vertex);
        }
        graph.AddEdge(graph.Vertices[0], graph.Vertices[1], 10);
        graph.AddEdge(graph.Vertices[0], graph.Vertices[2], 10);
        graph.AddEdge(graph.Vertices[1], graph.Vertices[3], 10);

        List<Vertex<int>> traversal = graph.BreadthFirstTraversal(graph.Vertices[0]);

        Assert.True(traversal.Count == 4);

        for (int i = 0; i < 4; i++)
        {
            Assert.True(traversal.Contains(graph.Vertices[i]));
        }
    }

    #endregion

    public static IEnumerable<object[]> DijkstraTestData()
    {
        yield return new object[] { 6, 
                                    new (int, int, int)[] { (1, 3, 9), (1, 6, 14), (1, 2, 7), (2, 3, 10), (2, 4, 15), (4, 5, 6), (3, 6, 2), (6, 5, 9) },
                                    new int[] { 1, 3, 6, 5 } };       
    }

    [Theory]
    [MemberData(nameof(DijkstraTestData))]    
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


    public static IEnumerable<object[]> NegativeTestData()
    {
        yield return new object[] { 6,
                                    new (int, int, int)[] { (1, 3, 9), (1, 6, 14), (1, 2, 7), (2, 3, 10), (2, 4, -15), (4, 6, -6), (3, 6, 2), (6, 2, 9) },
                               };
    }

    [Theory]
    [MemberData(nameof (NegativeTestData))]

    public void TestIfNegative(int vAmount, (int v1, int v2, int w)[] array)
    {
        GraphAStar.Graph<int> graph = new GraphAStar.Graph<int>();

        for (int i = 0; i < vAmount; i++)
        {
            GraphAStar.Vertex<int> baseVertex = new GraphAStar.Vertex<int>(i);
            graph.AddVertex(baseVertex);
        }

        for (int i = 0; i < array.Length; i++)
        {
            graph.AddEdge(graph.Vertices[array[i].v1 - 1], graph.Vertices[array[i].v2 - 1], array[i].w);
        }

        GraphAStar.VertexInfo<int> start = new GraphAStar.VertexInfo<int>(graph.Vertices[array[0].v1]);

        bool result = graph.IsBellmanFordNegative(start);

        Assert.True(result);
    }
}
