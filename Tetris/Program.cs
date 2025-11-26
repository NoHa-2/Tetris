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

        /*
        static readonly int gameBufferWidth = 12;
        static readonly int gameBufferHeight = 22;
        private static char[] gameBuffer;
        */
        static void Main(string[] args)
        {
            /*
            gameBuffer = new char[gameBufferWidth * gameBufferHeight];
            for (int y = 0; y < gameBufferHeight; y++)
            {
                for (int x = 0; x < gameBufferWidth; x++)
                {
                    gameBuffer[y * gameBufferWidth + x] = 'i';
                }
            }
            */
            Console.CursorVisible = false;
            ConsoleBuffer.frameBuffer.ClearBuffer();
            Tetrominos.GetNextTetromino();
            Tetrominos.GetCurrentTetromino();
            while (true)
            {
                Thread.Sleep(500);
                Tetrominos.CurrentTetrominoFallingMovement();
                ConsoleBuffer.frameBuffer.Draw();

            }
           

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
        /*
        public static void UpdateFrameBuffer()
        {
            for (int y = 0; y < gameBufferHeight; y++)
            {
                for (int x = 0; x < gameBufferWidth; x++)
                {
                    ConsoleBuffer.frameBuffer.WriteCharToBuffer(gameBuffer[y * gameBufferWidth + x], startingBorderX + x, startingBorderY + y);
                }
            }
        }
        */
    }
}