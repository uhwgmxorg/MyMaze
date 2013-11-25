// Class: Maze
// Algorithm is from Arnold Willemer:
// http://www.galileocomputing.de/katalog/buecher/titel/gp/GPP-unixguru/titelID-2259
using System;
using System.Linq;
using System.Windows;

namespace MyMaze
{
    public class Maze
    {
        enum KindOfSeperation
        {
            Wall,
            GROUND
        }

        public Point Start { get; set; }
        public Point Position { get; set; }

        protected int Width { get; set; }
        protected int Height { get; set; }
        private bool[] CellVisited { get; set; }
        private bool[] CellIsEntered { get; set; }
        private bool[] Wall { get; set; }
        private bool[] Ground { get; set; }

        /// <summary>
        /// Construktor
        /// </summary>
        public Maze(int width, int height)
        {
            Start = new Point(-1,-1);
            Position = new Point(-1,-1);
            Width = width;
            Height = height;
            CellVisited = new bool[Width * Height];
            CellIsEntered = new bool[Width * Height];
            for (int i = 0; i < Width * Height; ++i)
            {
                CellIsEntered[i] = false;
                CellVisited[i] = false;
            }
            Ground = new bool[Width * (Height + 1)];
            for (int i = 0; i < Width * (Height + 1); ++i)
            {
                Ground[i] = true;
            }
            Wall = new bool[(Width + 1) * Height];
            for (int i = 0; i < (Width + 1) * Height; ++i)
            {
                Wall[i] = true;
            }
        }

        /// <summary>
        /// Generat
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Generat(int x, int y)
        {
            if (IsCellVisited(x, y)) return;
            SetCellVisited(x, y);

            int[] Order = new[] { 0, 1, 2, 3 };
            int[,] Direction = new[,] { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 } };

            for (int i = 0; i < 12; i++)
            {
                int a = (int)Tools.StaticTools.RandomDouble(0, 4);
                int b = (int)Tools.StaticTools.RandomDouble(0, 4);
                if (a != b)
                {
                    int Between = Order[a];
                    Order[a] = Order[b];
                    Order[b] = Between;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                int j = Order[i];
                ExamineNeighbors(x, y, x + Direction[j, 0], y + Direction[j, 1]);
            }
        }

        /// <summary>
        /// Show
        /// </summary>
        public void Show()
        {
            int x, y;

            for (y = Height - 1; y >= 0; y--)
            {
                // Print the ceiling
                for (x = 0; x < Width; x++)
                {
                    if (IsSeparation(x, y, x, y + 1))
                    {
                        System.Console.Write("+--");
                    }
                    else
                    {
                        System.Console.Write("+  ");
                    }
                }
                System.Console.Write("+\n");
                // Print the walls and cells
                for (x = 0; x < Width; x++)
                {
                    if (IsSeparation(x, y, x - 1, y))
                    {
                        System.Console.Write("|");
                    }
                    else
                    {
                        System.Console.Write(" ");
                    }
                    // Print Position
                    if (Position.X == x && Position.Y == y)
                       System.Console.Write("XX");
                    else
                        // Print Start
                        if (Start.X == x && Start.Y == y)
                            System.Console.Write("OO");
                        else
                            // Print IsEntered
                            if (IsCellEntered(x, y))
                                System.Console.Write("..");
                            else
                                System.Console.Write("  ");
                }
                if (IsSeparation(x, y, x - 1, y))
                {
                    System.Console.Write("|");
                }
                else
                {
                    System.Console.Write(" ");
                }
                System.Console.Write("\n");
            }
            // Print the ground
            for (x = 0; x < Width; x++)
            {
                if (IsSeparation(x, 0, x, -1))
                {
                    System.Console.Write("+--");
                }
                else
                {
                    System.Console.Write("+  ");
                }
            }
            System.Console.Write("+\n");
        }

        /// <summary>
        /// IsSeparation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        public bool IsSeparation(int x, int y, int x1, int y1)
        {
            KindOfSeperation Kos;
            int Index = GetSeparation(out Kos, x, y, x1, y1);
            switch (Kos)
            {
                case KindOfSeperation.Wall:
                    return Wall[Index];
                case KindOfSeperation.GROUND:
                    return Ground[Index];
            }
            throw new Exception();
        }

        /// <summary>
        /// BreakThroughSeparation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        public void BreakThroughSeparation(int x, int y, int x1, int y1)
        {
            KindOfSeperation Kos;
            int Index = GetSeparation(out Kos, x, y, x1, y1);
            switch (Kos)
            {
                case KindOfSeperation.Wall:
                    Wall[Index] = false;
                    break;
                case KindOfSeperation.GROUND:
                    Ground[Index] = false;
                    break;
            }
        }

        /// <summary>
        /// BuildExitWest
        /// </summary>
        /// <param name="y"></param>
        public void BuildExitWest(int y)
        {
            BreakThroughSeparation(0, y, -1, y);
        }

        /// <summary>
        /// BuildExitEst
        /// </summary>
        /// <param name="y"></param>
        public void BuildExitEst(int y)
        {
            BreakThroughSeparation(Width, y, Width - 1, y);
        }

        /// <summary>
        /// BuildExitNorth
        /// </summary>
        /// <param name="x"></param>
        public void BuildExitNorth(int x)
        {
            BreakThroughSeparation(x, Height, x, Height - 1);
        }

        /// <summary>
        /// BuildExitSouth
        /// </summary>
        /// <param name="x"></param>
        public void BuildExitSouth(int x)
        {
            BreakThroughSeparation(x, 0, x, -1);
        }

        /// <summary>
        /// IsCellEntered
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsCellEntered(int x, int y)
        {
            if (x < 0) x = 0;
            if (x > Width - 1) x = Width - 1;
            if (y < 0) y = 0;
            if (y > Height - 1) y = Height - 1;
            return CellIsEntered[x + y * Width];
        }

        /// <summary>
        /// IsCellEntered
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsCellEntered(Point p)
        {
            if ((int)Math.Round(p.X) < 0) p.X = 0;
            if ((int)Math.Round(p.X) > Width - 1) p.X = Width - 1;
            if ((int)Math.Round(p.Y) < 0) p.Y = 0;
            if ((int)Math.Round(p.Y) > Height - 1) p.Y = Height - 1;
            return CellIsEntered[(int)Math.Round(p.X) + (int)Math.Round(p.Y) * Width];
        }

        /// <summary>
        /// SetCellEntered
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetCellEntered(int x, int y)
        {
            if (x < 0) x = 0;
            if (x > Width - 1) x = Width - 1;
            if (y < 0) y = 0;
            if (y > Height - 1) y = Height - 1;
            CellIsEntered[x + y * Width] = true;
        }

        /// <summary>
        /// SetCellEntered
        /// </summary>
        /// <param name="p"></param>
        public void SetCellEntered(Point p)
        {
            if ((int)Math.Round(p.X) < 0) p.X = 0;
            if ((int)Math.Round(p.X) > Width - 1) p.X = Width - 1;
            if ((int)Math.Round(p.Y) < 0) p.Y = 0;
            if ((int)Math.Round(p.Y) > Height - 1) p.Y = Height - 1;
            CellIsEntered[(int)Math.Round(p.X) + (int)Math.Round(p.Y) * Width] = true;
        }
        
        /// <summary>
        /// ExamineNeighbors
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        private void ExamineNeighbors(int x, int y, int x1, int y1)
        {
            if (IsValidCoordinate(x1, y1))
            {
                if (IsCellNotVisited(x1, y1))
                {
                    BreakThroughSeparation(x, y, x1, y1);
                    Generat(x1, y1);
                }
            }
        }

        /// <summary>
        /// IsCellVisited
        /// </summary>
        /// <returns></returns>
        private bool IsCellVisited(int x, int y)
        {
            return CellVisited[x + y * Width];
        }

        /// <summary>
        /// IsCellNotVisited
        /// </summary>
        /// <returns></returns>
        private bool IsCellNotVisited(int x, int y)
        {
            return !CellVisited[x + y * Width];
        }

        /// <summary>
        /// SetCellVisited
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void SetCellVisited(int x, int y)
        {
            CellVisited[x + y * Width] = true;
        }

        /// <summary>
        /// IsValidCoordinate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsValidCoordinate(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height) return false;
            return true;
        }

        /// <summary>
        /// GetSeparation
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        private int GetSeparation(out KindOfSeperation kos, int x, int y, int x1, int y1)
        {
            if ((x == x1) && (Math.Abs(y - y1) == 1))
            {
                y = y > y1 ? y : y1;
                kos = KindOfSeperation.GROUND;
                return x + Width * y;
            }
            else if (y == y1 && Math.Abs(x - x1) == 1)
            {
                x = x > x1 ? x : x1;
                kos = KindOfSeperation.Wall;
                return x + (Width + 1) * y;
            }
            else
            {
                System.Console.WriteLine("GetSeparation impossible parameters: ", x, y, x1, y1);
            }
            throw new Exception("This should not happen!!");
        }
    }
}
