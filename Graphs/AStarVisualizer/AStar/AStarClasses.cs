using System.Drawing;

namespace AStarClasses
{
    public class AStar
    {
        public class Edge<T>
        {
            public VisualizerVertex<T> StartingPoint { get; set; }
            public VisualizerVertex<T> EndingPoint { get; set; }
            public float Distance { get; set; }

            public Edge(VisualizerVertex<T> startingPoint, VisualizerVertex<T> endingPoint, float distance)
            {
                StartingPoint = startingPoint;
                EndingPoint = endingPoint;
                Distance = distance;
            }
        }

        public class VertexBase<T>
        {
            public T Value { get; set; }
            public List<Edge<T>> Neighbors { get; set; }

            public int NeighborCount => Neighbors.Count;

            public VertexBase(T value)
            {
                Value = value;
                Neighbors = new List<Edge<T>>();
            }
        }

        public class VisualizerVertex<T>
        {
            public AStarVertex<T> pathVertex;

            public List<Edge<T>> Neighbors { get => pathVertex.Neighbors; }

            public Point position;

            public Color color;

            public int Z = 1;
        }

        public class AStarVertex<T>
        {
            private VertexBase<T> Vertex;

            public List<Edge<T>> Neighbors { get =>  Vertex.Neighbors; }

            public AStarVertex<T> Founder;

            public bool Visited;
            public float KnownDistance { get; set; }
            public float FinalDistance { get; set; }

            public AStarVertex(VertexBase<T> vertex)
            {
                Vertex = vertex;
                KnownDistance = float.PositiveInfinity;
                FinalDistance = float.PositiveInfinity;
                Visited = false;
            }

            public int NeighborCount()
            {
                return Vertex.NeighborCount;
            }

            public T GetValue()
            {
                return Vertex.Value;
            }
        }

        public class Graph<T>
        {
            private List<VisualizerVertex<T>> vertices;

            public IReadOnlyList<VisualizerVertex<T>> Vertices => vertices;

            public IReadOnlyList<Edge<T>> Edges;

            IEqualityComparer<T> Comparer;

            public int VertexCount => vertices.Count;

            public Graph(IEqualityComparer<T> comparer)
            {
                Comparer = comparer;
                vertices = new List<VisualizerVertex<T>>();
            }

            public bool AddVertex(VisualizerVertex<T> vertex)
            {
                if (vertex == null) return false;
                if (vertex.pathVertex.NeighborCount() > 0) return false;

                foreach (VisualizerVertex<T> v in Vertices)
                {
                    if (vertex.pathVertex.GetValue().Equals(v.pathVertex.GetValue())) return false;
                }

                vertices.Add(vertex);

                return true;
            }

            public bool RemoveVertex(VisualizerVertex<T> vertex)
            {
                if (!Vertices.Contains(vertex)) return false;

                foreach (VisualizerVertex<T> test in Vertices)
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

            public bool AddEdge(VisualizerVertex<T> first, VisualizerVertex<T> second, int distance)
            {
                if (!Vertices.Contains(first) || !Vertices.Contains(second)) return false;

                for (int i = 0; i < first.pathVertex.NeighborCount(); i++)
                {
                    if (first.Neighbors[i].EndingPoint == second) return false;
                }

                first.Neighbors.Add(new Edge<T>(first, second, distance));

                return true;
            }

            public bool RemoveEdge(VisualizerVertex<T> start, VisualizerVertex<T> end)
            {
                if (start == null || end == null) return false;
                if (!vertices.Contains(start) || !vertices.Contains(end)) return false;

                int index = -1;

                for (int i = 0; i < start.pathVertex.NeighborCount(); i++)
                {
                    if (start.Neighbors[i].EndingPoint == end)
                    {
                        index = i;
                        break;
                    }
                }

                if (index == -1) return false;

                start.Neighbors.Remove(start.Neighbors[index]);

                return true;
            }

            public VisualizerVertex<T> Search(T value)
            {
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (Comparer.Equals(value, vertices[i].pathVertex.GetValue()))
                    {
                        return vertices[i];
                    }

                    //if (vertices[i].Value.position.Equals(val))
                    //{
                    //    return vertices[i];
                    //}
                }

                return null;
            }

            public Edge<T> GetEdge(VisualizerVertex<T> start, VisualizerVertex<T> end)
            {
                if (start == null || end == null) return null;
                foreach (Edge<T> neighbor in start.Neighbors)
                {
                    if (neighbor.EndingPoint == end) return neighbor;
                }

                return null;
            }

            public int GetIndex(VisualizerVertex<T> vertex)
            {
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (vertex == vertices[i])
                    {
                        return i;
                    }
                }

                return -1;
            }

            public int Manhattan(VisualizerVertex<T> start, VisualizerVertex<T> end)
            {
                int xDistance = Math.Abs(start.position.X - end.position.X);
                int yDistance = Math.Abs(start.position.Y - end.position.Y);

                return start.Z * (xDistance + yDistance);
            }

            public List<VisualizerVertex<T>> AStarPathFinding(VisualizerVertex<T> start, VisualizerVertex<T> end, Func<VisualizerVertex<T>, VisualizerVertex<T>, int> heiristic)
            {
                Dictionary<AStarVertex<T>, VisualizerVertex<T>> points = new(); //CREATED A WRAPPER FOR BOTH (VERTEX INSIDE ASTART INSIDE VISUALIZER FOR ORGANIZATION; NEIGHBORS IN EACH; FINISH CHANGES
                PriorityQueue<AStarVertex<T>, float> upcoming = new();
                List<AStarVertex<T>> mimickList = new();
                List<AStarVertex<T>> visited = new();

                for (int i = 0; i < vertices.Count; i++)
                {
                    points.Add(vertices[i].pathVertex, vertices[i]);
                }
                start.pathVertex.KnownDistance = 0;
                start.pathVertex.FinalDistance = Manhattan(start, end);
                upcoming.Enqueue(start.pathVertex, start.pathVertex.FinalDistance);
                AStarVertex<T> test;

                while (upcoming.Count != 0)
                {
                    test = upcoming.Dequeue();
                    mimickList.Remove(test);
                    AStarVertex<T> neighbor;
                    visited.Add(test);

                    if (visited.Contains(end.pathVertex))
                    {
                        break;
                    }

                    for (int i = 0; i < test.NeighborCount(); i++)
                    {
                        neighbor = test.Neighbors[i].EndingPoint.pathVertex;
                        float newDistance = test.KnownDistance + test.Neighbors[i].Distance;

                        if (-(newDistance) > -(neighbor.KnownDistance))
                            continue;

                        neighbor.KnownDistance = newDistance;
                        neighbor.Founder = test;

                        neighbor.FinalDistance = newDistance + heiristic(start, end);

                        if (mimickList.Contains(neighbor))
                            continue;

                        upcoming.Enqueue(neighbor, neighbor.FinalDistance);
                        mimickList.Add(neighbor);
                    }
                }

                List<VisualizerVertex<T>> finalPath = new List<VisualizerVertex<T>>();
                VisualizerVertex<T> current = end;

                while (current != start)
                {
                    finalPath.Add(current);
                    current = points[current.pathVertex.Founder];
                }

                return finalPath;
            }
        }
    }
}
