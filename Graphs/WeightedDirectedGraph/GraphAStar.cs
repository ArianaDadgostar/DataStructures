using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedDirectedGraph
{
    public class GraphAStar
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

            public int X { get; set; }
            public int Y { get; set; }
            public int D = 1;

            public int NeighborCount => Neighbors.Count;

            public Vertex(T value)
            {
                Value = value;
                Neighbors = new List<Edge<T>>();
            }
        }

        public class VertexInfo<T>
        {
            public Vertex<T> Vertex;
            public Vertex<T> Founder;
            public bool Visited;
            public float KnownDistance { get; set; }
            public float FinalDistance { get; set; }

            public VertexInfo(Vertex<T> vertex)
            {
                Vertex = vertex;
                KnownDistance = float.PositiveInfinity;
                FinalDistance = float.PositiveInfinity;
                Visited = false;
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
                if (!Vertices.Contains(first) || !Vertices.Contains(second)) return false;

                for (int i = 0; i < first.NeighborCount; i++)
                {
                    if (first.Neighbors[i].EndingPoint == second) return false;
                }

                first.Neighbors.Add(new Edge<T>(first, second, distance));

                return true;
            }

            public bool RemoveEdge(Vertex<T> start, Vertex<T> end)
            {
                if (start == null || end == null) return false;
                if (!vertices.Contains(start) || !vertices.Contains(end)) return false;

                int index = -1;

                for (int i = 0; i < start.NeighborCount; i++)
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
                foreach (Edge<T> neighbor in start.Neighbors)
                {
                    if (neighbor.EndingPoint == end) return neighbor;
                }

                return null;
            }

            public int GetIndex(Vertex<T> vertex)
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

            public int Manhattan(Vertex<T> start, Vertex<T> end)
            {
                int xDistance = Math.Abs(start.X - end.X);
                int yDistance = Math.Abs(start.Y - end.Y);

                return start.D * (xDistance + yDistance);
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

            public void Relax(Dictionary<Vertex<T>, VertexInfo<T>> points)
            {
                Vertex<T> test;
                for (int i = 0; i < vertices.Count; i++)
                {
                    test = vertices[i];

                    foreach (Edge<T> neighbor in test.Neighbors)
                    {
                        if(neighbor.Distance == -6)
                        {
                            ;
                        }
                        float newDistance = points[neighbor.StartingPoint].KnownDistance + neighbor.Distance;
                        if (points[neighbor.EndingPoint].KnownDistance <= newDistance) continue;
                        points[neighbor.EndingPoint].KnownDistance = newDistance;
                    }
                }
            }

            public bool IsBellmanFordNegative(VertexInfo<T> start)
            {
                Dictionary<Vertex<T>, VertexInfo<T>> points = new();
                for (int i = 0; i < vertices.Count; i++)
                {
                    points.Add(vertices[i], new VertexInfo<T>(vertices[i]));
                }
                points[start.Vertex].KnownDistance = 0;
                for (int j = 0; j < vertices.Count - 1; j++)
                {
                    Relax(points);
                }


                for (int i = 0; i < vertices.Count; i++)
                {
                    Vertex<T> test = vertices[i];

                    foreach (Edge<T> neighbor in test.Neighbors)
                    {
                        float newDistance = points[neighbor.StartingPoint].KnownDistance + neighbor.Distance;
                        if (points[neighbor.EndingPoint].KnownDistance > newDistance) return true;
                    }
                }

                return false;
            }
        }
    }

}
