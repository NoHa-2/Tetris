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
        static int DelayMilliSeconds = 1000;

        static ConsoleKey KeyPress;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ConsoleBuffer.frameBuffer.ClearBuffer();
            
            Tetrominos.GetNewCurrentTetromino();
            ConsoleBuffer.frameBuffer.Draw();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    KeyPress = Console.ReadKey(true).Key;
                    InputHandler();
                }

                ConsoleBuffer.frameBuffer.Draw();
                Thread.Sleep(DelayMilliSeconds);
                Tetrominos.TetrominoFallingMovement();
            }

            
            
            

            

        }

        private static void InputHandler()
        {
            switch(KeyPress)
            {
                case ConsoleKey.UpArrow:
                    Tetrominos.RotateCurrentTetromino();
                    break;
            }
        }
        private static void DrawBorder()
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