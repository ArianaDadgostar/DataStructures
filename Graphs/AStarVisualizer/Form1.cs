using AStarClasses;
using AStarVisualizer.WeightedDirectedGraph;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics.CodeAnalysis;
using static AStarClasses.AStar;

namespace AStarVisualizer
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Bitmap bitmap;
        AStar.Graph<AStar.AStarVar> grid;// = new(new AStarComparer());

        public class AStarComparer : IEqualityComparer<AStar.AStarVar>
        {
            public bool Equals(AStar.AStarVar? x, AStar.AStarVar? y)
            {
                return x == y;
            }

            public AStar.Vertex<AStar.AStarVar> Search(Point position, AStar.Graph<AStar.AStarVar> grid)
            {
                for (int i = 0; i < grid.VertexCount; i++)
                {
                    if (Equals(position, grid.Vertices[i].Value.position))
                    {
                        return grid.Vertices[i];
                    }
                }

                return null;
            }

            public int GetHashCode([DisallowNull] AStar.AStarVar obj)
            {
                return (obj == null) ? 0 : obj.GetHashCode();
            }
        }


        int mouseDrag = 0;

        int gridWidth;
        int gridHeight;

        Point mousePos;

        Pen pen = new Pen(Color.Black);

        AStar.Vertex<AStar.AStarVar> start;
        AStar.Vertex<AStar.AStarVar> end;

        public Form1()
        {
            InitializeComponent();
        }

        public void DrawBase()
        {
            for (int i = 0; i < pathVisual.Width; i += gridWidth)
            {
                graphics.DrawLine(pen, i, 0, i, pathVisual.Height);
            }

            for (int i = 0; i < pathVisual.Height; i += gridHeight)
            {
                graphics.DrawLine(pen, 0, i, pathVisual.Width, i);
            }

            pathVisual.Image = bitmap;
        }

        public int GetIndex(Point position, int widthNum)
        {
            return position.X + (position.Y * widthNum);
        }

        public void SetGrids()
        {
            for (int y = 0; y < pathVisual.Height; y += gridHeight)
            {
                for (int x = 0; x < pathVisual.Width; x += gridWidth)
                {
                    AStar.AStarVar gridSquare = new AStar.AStarVar();
                    gridSquare.color = Color.White;
                    gridSquare.position = new Point(x / gridWidth, y / gridHeight);

                    AStar.Vertex<AStar.AStarVar> vertex = new(gridSquare);

                    grid.AddVertex(vertex);
                }
            }

            for (int i = 0; i < grid.VertexCount; i++)
            {
                int widthNum = pathVisual.Width / gridWidth;
                int index = GetIndex(grid.Vertices[i].Value.position, widthNum);
                int upperIndex = index - widthNum;
                int lowerIndex = index + widthNum;

                if (upperIndex >= 0)
                {
                    grid.AddEdge(grid.Vertices[i], grid.Vertices[upperIndex], 1);
                }
                if (lowerIndex < grid.VertexCount)
                {
                    grid.AddEdge(grid.Vertices[i], grid.Vertices[lowerIndex], 1);
                }

                if ((index + 1) % widthNum != 0)
                {
                    grid.AddEdge(grid.Vertices[i], grid.Vertices[i + 1], 1);
                }

                if (index % widthNum == 0) continue;

                grid.AddEdge(grid.Vertices[i], grid.Vertices[i - 1], 1);
            }

            //top left
            start = grid.Vertices[0];
            start.Value.color = Color.Green;

            //bottom right
            end = grid.Vertices[grid.VertexCount - 1];
            end.Value.color = Color.Green;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pathVisual.Width, pathVisual.Height);

            gridWidth = 20;
            gridHeight = 15;

            graphics = Graphics.FromImage(bitmap);

            IEqualityComparer<AStar.AStarVar> starComparer = new AStarComparer();

            grid = new AStar.Graph<AStar.AStarVar>(starComparer);

            DrawBase();
            SetGrids();
        }

        private void pathVisual_Click(object sender, EventArgs e)
        {
            if (mouseDrag > 17) return;
            if (start.Value.color == Color.Red && end.Value.color == Color.Red) return;

            Point nearest = new Point(gridWidth * (mousePos.X / gridWidth), gridHeight * (mousePos.Y / gridHeight));

            graphics.FillRectangle(Brushes.Red, nearest.X, nearest.Y, gridWidth, gridHeight);

            int horizontalNum = pathVisual.Width / gridWidth;

            pathVisual.Image = bitmap;
            int index = nearest.X / gridWidth + (horizontalNum * (nearest.Y / gridHeight));

            if (start.Value.color == Color.Green)
            {
                AStar.Vertex<AStar.AStarVar> startVertex = grid.Vertices[index];
                start.Value.color = Color.Red;

                startVertex.Value.color = Color.Red;

                startVertex.Value.color = start.Value.color;

                start.Value.position = nearest;

                return;
            }

            end.Value.color = Color.Red;
            AStar.Vertex<AStar.AStarVar> endVertex = grid.Vertices[index];

            endVertex.Value.color = end.Value.color;
            end.Value.position = nearest;
        }

        private void pathVisual_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos = e.Location;

            Text = mousePos.ToString();

            if (mouseDrag == 0) return;
            //if(mousePos.X > pathVisual.)

            mouseDrag = mousePos.X + mousePos.Y;

            if (mouseDrag < 17) return;

            Point nearest = new Point(gridWidth * (mousePos.X / gridWidth), gridHeight * (mousePos.Y / gridHeight));

            int horizontalNum = pathVisual.Width / gridWidth;

            int cubeIndex = nearest.X / gridWidth + (horizontalNum * (nearest.Y / gridHeight));

            AStar.Vertex<AStar.AStarVar> vertex = grid.Vertices[cubeIndex];
            //nearest.X + (nearest.Y / gridWidth)
            if (vertex.Value.color == Color.Red) return;

            graphics.FillRectangle(Brushes.Green, nearest.X, nearest.Y, gridWidth, gridHeight);
            pathVisual.Image = bitmap;



            int index = GetIndex(nearest, pathVisual.Width / gridWidth);

            foreach (AStar.Edge<AStar.AStarVar> edge in vertex.Neighbors)
            {
                edge.EndingPoint.Neighbors.Remove(grid.GetEdge(edge.EndingPoint, vertex));
            }
            vertex.Neighbors.Clear();

            vertex.Value.color = Color.Green;
        }

        private void pathVisual_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDrag = 1;
        }

        private void pathVisual_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDrag = 0;
        }
        
        private void clearButton_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);

            start.Value.color = Color.Green;
            end.Value.color = Color.Green;

            DrawBase();
        }

        private void PathFindingButton_Click(object sender, EventArgs e)
        {
            List<AStar.Vertex<AStar.AStarVar>> path = grid.AStarPathFinding(start, end, Manhattan);

            foreach(AStar.Vertex<AStar.AStarVar> vertex in path)
            {
                graphics.FillRectangle(Brushes.Purple, vertex.Value.position.X, vertex.Value.position.Y, gridWidth, gridHeight);
            }

            pathVisual.Image = bitmap;
        }
    }
}
