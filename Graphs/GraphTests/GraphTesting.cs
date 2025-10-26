using UnweightedUndirectedGraph;

namespace GraphTests
{
    public class GraphTesting
    {
        class Dog : IComparable<Dog>
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public int BarkVolume { get; set; }

            public Dog(int age, string name, int barkVolume) 
            {
                Age = age;
                Name = name;
                BarkVolume = barkVolume;
            }

            public int CompareTo(Dog? other)
            {
                return Age - other.Age;
            }
        }

        [Fact]
        public void DogTest()
        {
            Graph<Dog> graph = new Graph<Dog>();

            Assert.True(graph.AddVertex(new Vertex<Dog>(new Dog(8, "bob", 3))));
            Assert.True(graph.AddVertex(new Vertex<Dog>(new Dog(8, "sam", 25))));

            Assert.True(graph.VertexCount == 2);
        }

        #region BasicFunctionTesting

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
                Assert.True(removed.value != vertex.value);
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

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);

            Assert.True(graph.Vertices[0].Neighbors.Contains(graph.Vertices[1]));
            Assert.True(graph.Vertices[1].Neighbors.Contains(graph.Vertices[0]));

            Vertex<int> falseTest = new Vertex<int>(10);
            bool result = graph.AddEdge(graph.Vertices[0], falseTest);

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

            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            graph.RemoveEdge(graph.Vertices[0], graph.Vertices[1]);

            bool containsOne = graph.Vertices[0].Neighbors.Contains(graph.Vertices[1]);
            bool containsZero = graph.Vertices[1].Neighbors.Contains(graph.Vertices[0]);

            Assert.True(containsOne == false && containsZero == false);

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

            Vertex<int> result = graph.Search(graph.Vertices[0].value);
            Assert.True(result != null && result.value == graph.Vertices[0].value);

            Vertex<int> falseResult = graph.Search(0);
            Assert.True(falseResult == null);
        }

        #endregion

        #region TraversalTests

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
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2]);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[3]);

            List<int> traversal = graph.RecursiveDepthFirstTraversal(graph.Vertices[0]);

            Assert.True(traversal.Count == 4);

            for (int i = 0; i < 4; i++)
            {
                Assert.True(traversal.Contains(graph.Vertices[i].value));
            }
        }

        [Theory]
        [InlineData(new int[] { 3, 2, 1, 3, 4 })]
        [InlineData(new int[] { 5, 1, 5, 9, 11 })]
        public void IterativelyDepthFirstTraversalTest(params int[] array)
        {
            Graph<int> graph = new Graph<int>();

            foreach (int i in array)
            {
                Vertex<int> vertex = new Vertex<int>(i);
                graph.AddVertex(vertex);
            }
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2]);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[3]);

            List<int> traversal = graph.IterativeDepthFirstTraversal(graph.Vertices[0]);

            Assert.True(traversal.Count == 4);

            for (int i = 0; i < 4; i++)
            {
                Assert.True(traversal.Contains(graph.Vertices[i].value));
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
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2]);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[3]);

            List<int> traversal = graph.BreadthFirstTraversal(graph.Vertices[0]);

            Assert.True(traversal.Count == 4);

            for (int i = 0; i < 4; i++)
            {
                Assert.True(traversal.Contains(graph.Vertices[i].value));
            }
        }

        [Theory]
        [InlineData(new int[] { 3, 2, 1, 3, 4 })]
        [InlineData(new int[] { 5, 1, 5, 9, 11 })]
        public void FindShortestPathTest(params int[] array)
        {
            Graph<int> graph = new Graph<int>();

            foreach (int i in array)
            {
                Vertex<int> vertex = new Vertex<int>(i);
                graph.AddVertex(vertex);
            }
            graph.AddEdge(graph.Vertices[0], graph.Vertices[1]);
            graph.AddEdge(graph.Vertices[0], graph.Vertices[2]);
            graph.AddEdge(graph.Vertices[1], graph.Vertices[3]);

            List<Vertex<int>> path = graph.FindShortestPath(graph.Vertices[0], graph.Vertices[3]);

            Assert.True(path.Count == 3);

            Assert.True(path.Contains(graph.Vertices[0]));
            Assert.True(path.Contains(graph.Vertices[1]));
            Assert.True(path.Contains(graph.Vertices[3]));
        }

        #endregion
    }
}