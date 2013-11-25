using MazeLibrary;
/******************************************************************************/
/*                                                                            */
/*   Program: MyMaze                                                          */
/*   A console maze                                                           */
/*                                                                            */
/*   25.11.2013 0.0.0.0 uhwgmxorg Start                                       */
/*                                                                            */
/******************************************************************************/
using System;
using System.Linq;
using Tools;

namespace MyMaze
{
    class Program
    {
        protected static int Width { get; set; }
        protected static int Height { get; set; }

        /// <summary>
        /// ScreenOutput
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="backgroundColor"></param>
        static void ScreenOutput(int x, int y, string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static void ScreenOutput(int x, int y, string text, ConsoleColor foregroundColor)
        {
            ScreenOutput(x, y, text, foregroundColor, ConsoleColor.Black);
        }
        static void ScreenOutput(int x, int y, string text)
        {
            ScreenOutput(x, y, text, ConsoleColor.Gray);
        }
        static void ScreenOutput(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static void ScreenOutput(string text, ConsoleColor foregroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static void ScreenOutput(string text)
        {
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ScreenOutput("Program MyMaze\n", ConsoleColor.Cyan);

            Width = 8;
            Height = 8;

            // taking VMaze instead of Maze
            //Maze Maze = new Maze(Width, Height);
            VMaze Maze = new VMaze(Width, Height);

            Maze.Generat((int)StaticTools.RandomDouble(0, Width - 1), (int)StaticTools.RandomDouble(0, Height - 1));

            Maze.BuildExitSouth(1);

            // comment this in to show Start and a certain Position
            //Maze.Start = new Point(Width-1, Height-1);
            //Maze.Position = new Point(0,0);

            Maze.Show();

            ScreenOutput("\npress any key");
            Console.ReadKey();
        }
    }
}
