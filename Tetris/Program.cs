using System;
using Tetris;

namespace Program
{
    class Program
    {
        static readonly int startingBorderX = 30;
        static readonly int endBorderX = 41;
        static readonly int startingBorderY = 5;
        static readonly int endBorderY = 25;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ConsoleBuffer.frameBuffer.ClearBuffer();
            Tetrominos.GetNextTetromino();
            Tetrominos.GetCurrentTetromino();
            Tetrominos.DrawTetromino();
            DrawBorder();





            ConsoleBuffer.frameBuffer.Draw();
            Console.Read();

            // game logic

            // display stuff
        }
        public static void DrawBorder()
        {
            for (int i = 0; i < 21; i++)
            {
                ConsoleBuffer.frameBuffer.WriteCharToBuffer('#', startingBorderX, startingBorderY + i);
                ConsoleBuffer.frameBuffer.WriteCharToBuffer('#', endBorderX, startingBorderY + i);
            }
            for (int i = 0; i < 11; i++)
            {
                ConsoleBuffer.frameBuffer.WriteCharToBuffer('#', startingBorderX + i, endBorderY);
            }
        }
    }
}