using AStarClasses;
using AStarVisualizer.WeightedDirectedGraph;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Drawing2D;
using static AStarClasses.AStar;

namespace AStarVisualizer
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Bitmap bitmap;
        Graph<int> grid;// = new(new AStarComparer());

        public class AStarComparer : IEqualityComparer<int>
        {


            public VisualizerVertex<int> Search(Point position, Graph<int> grid)
            {
                for (int i = 0; i < grid.VertexCount; i++)
                {
                    if (Equals(position, grid.Vertices[i].position))
                    {
                        return grid.Vertices[i];
                    }
                }

                return null;
            }

            public int GetHashCode([DisallowNull] VisualizerVertex<int> obj)
            {
                return (obj == null) ? 0 : obj.GetHashCode();
            }

            public bool Equals(int x, int y)
            {
                return x == y;
            }

            public int GetHashCode([DisallowNull] int obj)
            {
                return obj.GetHashCode();
            }
        }


        int mouseDrag = 0;

        int gridWidth;
        int gridHeight;

        Point mousePos;

        Pen pen = new Pen(Color.Black);

        VisualizerVertex<int> start;
        VisualizerVertex<int> end;

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

        public void GridBounds(Point position)
        {
            int widthNum = pathVisual.Width / gridWidth;
            int index = GetIndex(position, widthNum);
            int upperIndex = index - widthNum;
            int lowerIndex = index + widthNum;

            if (upperIndex >= 0)
            {
                grid.AddEdge(grid.Vertices[index], grid.Vertices[upperIndex], 1);
            }
            if (lowerIndex < grid.VertexCount)
            {
                grid.AddEdge(grid.Vertices[index], grid.Vertices[lowerIndex], 1);
            }

            if ((index + 1) % widthNum != 0)
            {
                grid.AddEdge(grid.Vertices[index], grid.Vertices[index + 1], 1);
            }

            if (index % widthNum == 0) return;

            grid.AddEdge(grid.Vertices[index], grid.Vertices[index - 1], 1);
        }

        public void SetGrids()
        {
            for (int y = 0; y < pathVisual.Height; y += gridHeight)
            {
                for (int x = 0; x < pathVisual.Width; x += gridWidth)
                {
                    VisualizerVertex<int> vertex = new VisualizerVertex<int>(grid.VertexCount);
                    vertex.color = Color.White;
                    vertex.position = new Point(x / gridWidth, y / gridHeight);

                    grid.AddVertex(vertex);
                }
            }

            for (int i = 0; i < grid.VertexCount; i++)
            {
                //int widthNum = pathVisual.Width / gridWidth;
                //int index = GetIndex(grid.Vertices[i].position, widthNum);
                //int upperIndex = index - widthNum;
                //int lowerIndex = index + widthNum;

                //if (upperIndex >= 0)
                //{
                //    grid.AddEdge(grid.Vertices[i], grid.Vertices[upperIndex], 1);
                //}
                //if (lowerIndex < grid.VertexCount)
                //{
                //    grid.AddEdge(grid.Vertices[i], grid.Vertices[lowerIndex], 1);
                //}

                //if ((index + 1) % widthNum != 0)
                //{
                //    grid.AddEdge(grid.Vertices[i], grid.Vertices[i + 1], 1);
                //}

                //if (index % widthNum == 0) continue;

                //grid.AddEdge(grid.Vertices[i], grid.Vertices[i - 1], 1);

                GridBounds(grid.Vertices[i].position);
            }

            //top left
            start = grid.Vertices[0];
            start.color = Color.Green;

            //bottom right
            end = grid.Vertices[grid.VertexCount - 1];
            end.color = Color.Green;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pathVisual.Width, pathVisual.Height);

            gridWidth = 20;
            gridHeight = 15;

            graphics = Graphics.FromImage(bitmap);

            IEqualityComparer<int> starComparer = new AStarComparer();

            grid = new Graph<int>(starComparer);

            DrawBase();
            SetGrids();
        }

        private void pathVisual_Click(object sender, EventArgs e)
        {
            if (mouseDrag > 17) return;
            if (start.color == Color.Red && end.color == Color.Red) return;

            Point nearest = new Point(gridWidth * (mousePos.X / gridWidth), gridHeight * (mousePos.Y / gridHeight));
            int horizontalNum = pathVisual.Width / gridWidth;
            int index = nearest.X / gridWidth + (horizontalNum * (nearest.Y / gridHeight));

            if (grid.Vertices[index].color == Color.Green) return;

            graphics.FillRectangle(Brushes.Red, nearest.X, nearest.Y, gridWidth, gridHeight);


            pathVisual.Image = bitmap;

            if (start.color == Color.Green)
            {
                start = grid.Vertices[index];
                start.color = Color.Red;

                nearest.X /= gridWidth;
                nearest.Y /= gridHeight;

                start.position = nearest;

                int neighborCount = start.pathVertex.NeighborCount();

                //for (int i = 0; i < neighborCount; i++)
                //{
                //    grid.RemoveEdge(start, start.Neighbors[0].EndingPoint); // Like this because neighbors update after removal.
                //}

                return;
            }

            end.color = Color.Red;
            end = grid.Vertices[index];

            nearest.X /= gridWidth;
            nearest.Y /= gridHeight;

            end.position = nearest;

            for (int i = 0; i < end.pathVertex.NeighborCount(); i++)
            {
                grid.RemoveEdge(end, end.Neighbors[i].EndingPoint);
            }
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

            VisualizerVertex<int> vertex = grid.Vertices[cubeIndex];
            //nearest.X + (nearest.Y / gridWidth)
            if (vertex.color == Color.Red) return;

            graphics.FillRectangle(Brushes.Green, nearest.X, nearest.Y, gridWidth, gridHeight);
            pathVisual.Image = bitmap;



            int index = GetIndex(nearest, pathVisual.Width / gridWidth);

            foreach (Edge<int> edge in vertex.Neighbors)
            {
                edge.EndingPoint.Neighbors.Remove(grid.GetEdge(edge.EndingPoint, vertex));
            }
            vertex.Neighbors.Clear();

            vertex.color = Color.Green;
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

            start.color = Color.Green;
            end.color = Color.Green;

            DrawBase();
            SetGrids();
        }

        private void PathFindingButton_Click(object sender, EventArgs e)
        {
            List<VisualizerVertex<int>> path = grid.AStarPathFinding(start, end, grid.Manhattan);

            foreach (VisualizerVertex<int> vertex in path)
            {
                graphics.FillRectangle(Brushes.Purple, gridWidth*(vertex.position.X), gridHeight*vertex.position.Y, gridWidth, gridHeight);
            }

            pathVisual.Image = bitmap;
        }
    }
}
