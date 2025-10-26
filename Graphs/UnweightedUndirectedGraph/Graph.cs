using System.ComponentModel;

namespace UnweightedUndirectedGraph
{
    public class Graph<T>
    {
        public List<Vertex<T>> Vertices { get; private set; }

        public int VertexCount => Vertices.Count;

        public Graph()
        {
            Vertices = new List<Vertex<T>>();
        }

        #region BasicFunctions

        public bool AddVertex(Vertex<T> vertex)
        {
            if (vertex == null || vertex.NeighborCount > 0) return false;

            if(Search(vertex.value) != null) return false;

            Vertices.Add(vertex);

            return true;
        }

        public bool RemoveVertex(Vertex<T> vertex)
        {
            if(!Vertices.Contains(vertex)) return false;

            foreach(Vertex<T> val in Vertices)
            {
                if (!val.Neighbors.Contains(vertex)) continue;

                val.Neighbors.Remove(vertex);
            }

            Vertices.Remove(vertex);

            return true;
        }

        public bool AddEdge(Vertex<T> vertexOne, Vertex<T> vertexTwo)
        {
            if(vertexOne == null || vertexTwo == null) return false;

            if(!Vertices.Contains(vertexOne) || !Vertices.Contains(vertexTwo)) return false;

            vertexOne.Neighbors.Add(vertexTwo);
            vertexTwo.Neighbors.Add(vertexOne);

            return true;
        }

        public bool RemoveEdge(Vertex<T> vertexOne, Vertex<T> vertexTwo)
        {
            if (vertexOne == null || vertexTwo == null) return false;

            if (!Vertices.Contains(vertexOne) || !Vertices.Contains(vertexTwo)) return false;

            if(!vertexOne.Neighbors.Contains(vertexTwo) || !vertexTwo.Neighbors.Contains(vertexOne)) return false;

            vertexOne.Neighbors.Remove(vertexTwo);
            vertexTwo.Neighbors.Remove(vertexOne);

            return true;
        }

        public Vertex<T> Search(T value)
        {
            foreach(Vertex<T> val in Vertices)
            {
                if (val.value.Equals(value)) return val;
            }

            return null;
        }

        #endregion

        #region Traversals

        public List<T> RecursiveDepthFirstTraversal(Vertex<T> starting)
        {
            List<Vertex<T>> visited = new List<Vertex<T>>();
            List<T> values = new List<T>();

            RecursivelyDFT(starting, ref visited, ref values);

            return values;
        }

        public void RecursivelyDFT(Vertex<T> vertex, ref List<Vertex<T>> visited, ref List<T> values)
        {
            visited.Add(vertex);
            values.Add(vertex.value);
            foreach(Vertex<T> neighbor in vertex.Neighbors)
            {
                if (visited.Contains(neighbor)) continue;

                RecursivelyDFT(neighbor, ref visited, ref values);
            }
        }


        public List<T> IterativeDepthFirstTraversal(Vertex<T> starting)
        {
            List<Vertex<T>> visited = new List<Vertex<T>>();
            Stack<Vertex<T>> toVisit = new Stack<Vertex<T>>();
            List<T> values = new List<T>();
            Vertex<T> vertex;

            toVisit.Push(starting);

            while (values.Count < Vertices.Count)
            {
                vertex = toVisit.Pop();
                visited.Add(vertex);
                values.Add(vertex.value);

                foreach (Vertex<T> neighbor in vertex.Neighbors)
                {
                    if (visited.Contains(neighbor)) continue;

                    toVisit.Push(neighbor);
                }
            }

            return values;
        }

        public List<T> BreadthFirstTraversal(Vertex<T> starting)
        {
            List<Vertex<T>> visited = new List<Vertex<T>>();
            Queue<Vertex<T>> toVisit = new Queue<Vertex<T>>();
            List<T> values = new List<T>();
            Vertex<T> vertex;

            toVisit.Enqueue(starting);

            while (values.Count < Vertices.Count)
            {
                vertex = toVisit.Dequeue();
                visited.Add(vertex);
                values.Add(vertex.value);

                foreach (Vertex<T> neighbor in vertex.Neighbors)
                {
                    if (visited.Contains(neighbor)) continue;

                    toVisit.Enqueue(neighbor);
                }
            }

            return values;
        }

        public List<Vertex<T>> FindShortestPath(Vertex<T> start, Vertex<T> end)
        {
            List<Vertex<T>> visited = new List<Vertex<T>>();
            Queue<Vertex<T>> toVisit = new Queue<Vertex<T>>();
            Vertex<T> vertex = new Vertex<T>(default(T));
            int[] parents = new int[Vertices.Count];
            List<Vertex<T>> finalPath = new List<Vertex<T>>();

            toVisit.Enqueue(start);

            while (toVisit.Peek() != end)
            {
                vertex = toVisit.Dequeue();
                visited.Add(vertex);

                foreach (Vertex<T> neighbor in vertex.Neighbors)
                {
                    if (visited.Contains(neighbor)) continue;

                    toVisit.Enqueue(neighbor);
                    int val = Vertices.IndexOf(toVisit.Peek());
                    parents[Vertices.IndexOf(neighbor)] = Vertices.IndexOf(vertex);
                }
            }

            int testIndex = Vertices.IndexOf(end);
            while(testIndex != Vertices.IndexOf(start))
            {
                finalPath.Add(Vertices[testIndex]);
                testIndex = parents[testIndex];
            }
            finalPath.Add(Vertices[testIndex]);

            return finalPath;
        }

        #endregion
    }
}
