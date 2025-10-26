using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedDirectedGraph
{
    public class VertexInfo<T>
    {
        public Vertex<T> Vertex { get; set; }
        public float distance { get; set; }
        public Vertex<T> Founder { get; set; }
        public bool visited { get; set; }

        public float KnownDistance { get; set; }
        public float FinalDistance { get; set; }

        public VertexInfo(Vertex<T> vertex)
        {
            this.Vertex = vertex;
            visited = false;
            Founder = null;
            KnownDistance = float.PositiveInfinity;
            FinalDistance = float.PositiveInfinity;
        }
    }
}
