using System.Net.Http.Headers;

namespace WeightedDirectedGraph
{
    public class Edge<T>
    {
        public Vertex<T> StartingPoint { get; set; }
        public Vertex<T> EndingPoint { get; set; }
        public float Distance { get; set; }

        public Edge(Vertex<T> startingPoint, Vertex<T> endingPoint, float distance)
        {
            StartingPoint = startingPoint;
            EndingPoint = endingPoint;
            Distance = distance;
        }
    }

    public class Vertex<T>
    {
        public T Value { get; set; }
        public List<Edge<T>> Neighbors { get; set; }

        public int NeighborCount => Neighbors.Count;

        public Vertex(T value)
        {
            Value = value;
            Neighbors = new List<Edge<T>>();
        }
    }

    public class AStar<T>
    {
        public T Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int D = 1;

        public AStar(T value)
        {
            Value = value;
        }
    }

    public class Graph<T>
    {
        private List<Vertex<T>> vertices;

        public IReadOnlyList<Vertex<T>> Vertices => vertices;

        public IReadOnlyList<Edge<T>> Edges { get { throw new NotImplementedException(); } }

        public int VertexCount => vertices.Count;

        public Graph()
        {
            vertices = new List<Vertex<T>>();
        }

        public bool AddVertex(Vertex<T> vertex)
        {
            if (vertex == null) return false;
            if (vertex.NeighborCount > 0) return false;

            foreach (Vertex<T> v in Vertices)
            {
                if (vertex.Value.Equals(v.Value)) return false;
            }

            vertices.Add(vertex);

            return true;
        }

        public bool RemoveVertex(Vertex<T> vertex)
        {
            if (!Vertices.Contains(vertex)) return false;

            foreach (Vertex<T> test in Vertices)
            {
                foreach (Edge<T> neighbor in test.Neighbors)
                {
                    if (neighbor.EndingPoint != vertex) continue;

                    test.Neighbors.Remove(neighbor);
                }
            }

            vertices.Remove(vertex);

            return true;
        }

        public bool AddEdge(Vertex<T> first, Vertex<T> second, int distance)
        {
            if(!Vertices.Contains(first) || !Vertices.Contains(second)) return false;

            for (int i = 0; i < first.NeighborCount; i++)
            {
                if (first.Neighbors[i].EndingPoint == second) return false;
            }

            first.Neighbors.Add(new Edge<T>(first, second, distance));

            return true;
        }

        public bool RemoveEdge(Vertex<T> start, Vertex<T> end)
        {
            if(start == null || end == null) return false; 
            if(!vertices.Contains(start) || !vertices.Contains(end)) return false;

            int index = -1;

            for (int i = 0; i < start.NeighborCount; i++)
            {
                if (start.Neighbors[i].EndingPoint == end)
                {
                    index = i;
                    break;
                }
            }

            if(index == -1) return false;

            start.Neighbors.Remove(start.Neighbors[index]);

            return true;
        }

        public Vertex<T> Search(T val)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].Value.Equals(val))
                {
                    return vertices[i];
                }
            }

            return null;
        }

        public Edge<T> GetEdge(Vertex<T> start, Vertex<T> end)
        {
            if (start == null || end == null) return null;
            foreach(Edge<T> neighbor in start.Neighbors)
            {
                if (neighbor.EndingPoint == end) return neighbor;
            }

            return null;
        }

        public List<Vertex<T>> DepthFirstTraversal(Vertex<T> start)
        {
            List<Vertex<T>> path = new List<Vertex<T>> ();
            RecursiveDepthFirst(start, ref path);

            return path;
        }

        private void RecursiveDepthFirst(Vertex<T> test, ref List<Vertex<T>> visited)
        {
            visited.Add(test);
            foreach (Edge<T> neighbor in test.Neighbors)
            {
                if (visited.Contains(neighbor.EndingPoint)) continue;

                RecursiveDepthFirst(neighbor.EndingPoint, ref visited);
            }
        }

        public List<Vertex<T>> BreadthFirstTraversal(Vertex<T> start)
        {
            List<Vertex<T>> visited = new List<Vertex<T>>();

            Vertex<T> test;

            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            queue.Enqueue(start);

            while (visited.Count < vertices.Count)
            {
                test = queue.Dequeue();

                foreach(Edge<T> neighbor in test.Neighbors)
                {
                    if (queue.Contains(neighbor.EndingPoint) || visited.Contains(neighbor.EndingPoint)) continue;

                    queue.Enqueue(neighbor.EndingPoint);
                }

                visited.Add(test);
            }

            return visited;
        }

        public int GetIndex(Vertex<T> vertex)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if(vertex == vertices[i])
                {
                    return i;
                }
            }

            return -1;
        }

        public List<Vertex<T>> FindShortestPath(Vertex<T> start,  Vertex<T> end)
        {
            PriorityQueue<VertexInfo<T>, float> queue = new();

            Dictionary<Vertex<T>, VertexInfo<T>> info = new();

            List<VertexInfo<T>> mimickList = new();

            foreach(Vertex<T> vertex in vertices)
            {
                info.Add(vertex, new VertexInfo<T>(vertex));
                info[vertex].distance = float.PositiveInfinity;
            }

            VertexInfo<T> test;

            test = info[start];
            test.distance = 0;

            queue.Enqueue(test, test.distance);

            while (queue.Count != 0)
            {
                test = queue.Dequeue();
                mimickList.Remove(test);
                test.visited = true;

                if (info[end].visited) continue;

                foreach(Edge<T> neighbor in test.Vertex.Neighbors)
                {
                    if (info[neighbor.EndingPoint].distance < test.distance + neighbor.Distance) 
                        continue;
                    if (info[neighbor.EndingPoint].visited) 
                        continue;

                    info[neighbor.EndingPoint].distance = test.distance + neighbor.Distance;
                    info[neighbor.EndingPoint].Founder = test.Vertex;

                    if (mimickList.Contains(info[neighbor.EndingPoint]))
                        continue;                 
                    
                    queue.Enqueue(info[neighbor.EndingPoint], info[neighbor.EndingPoint].distance);
                    mimickList.Add(info[neighbor.EndingPoint]);
                }
            }

            List<Vertex<T>> path = new();

            test = info[end];
            path.Add(info[end].Vertex);

            while(test.Vertex != start)
            {
                test = info[test.Founder];
                path.Add(test.Vertex);
            }

            return path;
        }

        public List<Vertex<T>> AStarPathFinding(Vertex<T> start, Vertex<T> end, Func<Vertex<T>, Vertex<T>, int> heiristic)
        {
            Dictionary<Vertex<T>, VertexInfo<T>> points = new();
            PriorityQueue<VertexInfo<T>, float> upcoming = new();
            List<Vertex<T>> mimickList = new();
            List<Vertex<T>> visited = new();

            for (int i = 0; i < vertices.Count; i++)
            {
                points.Add(vertices[i], new VertexInfo<T>(vertices[i]));
            }

            upcoming.Enqueue(points[start], points[start].FinalDistance);
            VertexInfo<T> test;

            while (upcoming.Count == 0)
            {
                test = upcoming.Dequeue();
                mimickList.Remove(test.Vertex);
                VertexInfo<T> neighbor;
                visited.Add(test.Vertex);

                if (visited.Contains(end))
                {
                    break;
                }

                for (int i = 0; i < test.Vertex.NeighborCount; i++)
                {
                    neighbor = points[test.Vertex.Neighbors[i].EndingPoint];
                    float newDistance = test.KnownDistance + test.Vertex.Neighbors[i].Distance;

                    if (newDistance > neighbor.KnownDistance)
                        continue;

                    neighbor.KnownDistance = newDistance;
                    neighbor.Founder = test.Vertex;

                    neighbor.FinalDistance = newDistance + heiristic(start, end);

                    if (mimickList.Contains(neighbor.Vertex))
                        continue;

                    upcoming.Enqueue(neighbor, neighbor.FinalDistance);
                    mimickList.Add(neighbor.Vertex);
                }
            }

            List<Vertex<T>> finalPath = new List<Vertex<T>>();
            VertexInfo<T> current = points[end];

            while (current.Vertex != start)
            {
                finalPath.Add(current.Vertex);
                current = points[current.Founder];
            }

            return finalPath;
        }
    }
}
