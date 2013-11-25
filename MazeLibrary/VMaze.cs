using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLibrary
{
    public class VMaze : Maze
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public VMaze(int width, int height)
            : base(width, height)
        {
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
                        PrintLConerBase();
                    }
                    else
                    {
                        PrintLConer();
                    }
                }
                PrintRConer();
                NextLine();
                // Print the walls and cells
                for (x = 0; x < Width; x++)
                {
                    if (IsSeparation(x, y, x - 1, y))
                    {
                        PrintWall();
                    }
                    else
                    {
                        PrintGap();
                    }
                    // Print Position
                    if (Position.X == x && Position.Y == y)
                        PrintPosition();
                    else
                        // Print Start
                        if (Start.X == x && Start.Y == y)
                            PrintStart();
                        else
                            // Print IsEntered
                            if (IsCellEntered(x, y))
                                PrintIsEntered();
                            else
                                PrintDoubleGap();

                }
                if (IsSeparation(x, y, x - 1, y))
                {
                    PrintWall();
                }
                else
                {
                    PrintGap();
                }
                NextLine();
            }
            // Peint the ground
            for (x = 0; x < Width; x++)
            {
                if (IsSeparation(x, 0, x, -1))
                {
                    PrintLConerBase();
                }
                else
                {
                    PrintLConer();
                }
            }
            PrintRConer();
            NextLine();
        }

        /// <summary>
        /// PrintLConerBase
        /// </summary>
        public virtual void PrintLConerBase()
        {
            System.Console.Write("+--");
        }

        /// <summary>
        /// PrintLConer
        /// </summary>
        public virtual void PrintLConer()
        {
            System.Console.Write("+  ");
        }

        /// <summary>
        /// PrintRConer
        /// </summary>
        public virtual void PrintRConer()
        {
            System.Console.Write("+");
        }

        /// <summary>
        /// PrintWall
        /// </summary>
        public virtual void PrintWall()
        {
            System.Console.Write("|");
        }

        /// <summary>
        /// PrintGap
        /// </summary>
        public virtual void PrintGap()
        {
            System.Console.Write(" ");
        }

        /// <summary>
        /// PrintBoubleGap
        /// </summary>
        public virtual void PrintDoubleGap()
        {
            System.Console.Write("  ");
        }

        /// <summary>
        /// NextLine
        /// </summary>
        public virtual void NextLine()
        {
            System.Console.Write("\n");
        }

        /// <summary>
        /// PrintStart
        /// </summary>
        public virtual void PrintStart()
        {
            System.Console.Write("OO");
        }

        /// <summary>
        /// PrintPosition
        /// </summary>
        public virtual void PrintPosition()
        {
            System.Console.Write("XX");
        }

        /// <summary>
        /// PrintIsEntered
        /// </summary>
        public virtual void PrintIsEntered()
        {
            System.Console.Write("..");
        }
    }
}
