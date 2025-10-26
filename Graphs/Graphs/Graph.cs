namespace Graphs
{
    public class Graph<T> where T : IComparable<T>
    {
        public List<Vertex<T>> Vertices { get; private set; }

        public int VertexCount => Vertices.Count;

        public bool AddVertex(Vertex<T> vertex)
        {
            if(vertex == null || vertex.NeighborCount > 0) return false;

            foreach(var v in Vertices)
            {
                if (vertex.value.CompareTo(v.value) == 0) return false;
            }

            Vertices.Add(vertex);

            return true;
        }


    }
}
