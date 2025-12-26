using System;
using Tetris;

namespace Program
{
    class Program
    {
        private static readonly int startingBorderX = 30;
        private static readonly int endBorderX = 41;
        private static readonly int startingBorderY = 5;
        private static readonly int endBorderY = 25;

        private static readonly int borderHeight = 20;
        private static readonly int borderWidth = 10;

        private static int DelayMilliSeconds = 50;

        private static int speedCounter = 0;
        private static int speed = 20;
        private static bool isFallReady;
        private static int difficulty;

        private static ConsoleKey KeyPress;

        public static bool isDead { private get; set; }
        public static int score { get; set; }

        static void Main(string[] args)
        {
            while (true)
            {
                score = 0;
                isDead = false;

                Console.CursorVisible = false;
                ConsoleBuffer.frameBuffer.ClearBuffer();

                DrawPlayingField();

                Tetrominos.GameStart();
                ConsoleBuffer.frameBuffer.Draw();

                while (!isDead)
                {
                    Thread.Sleep(DelayMilliSeconds);

                    speed = 20;
                    difficulty = score / 200;
                    speed -= difficulty;

                    isFallReady = false;
                    speedCounter++;

                    if (speedCounter >= speed)
                    {
                        speedCounter = 0;
                        isFallReady = true;
                    }

                    if (Console.KeyAvailable)
                    {
                        KeyPress = Console.ReadKey(true).Key;
                        InputHandler();
                    }

                    if (isFallReady)
                    {
                        Tetrominos.TetrominoFallingMovement();
                    }
                    ConsoleBuffer.frameBuffer.Draw();
                }

                ConsoleBuffer.frameBuffer.ClearBuffer();

                ConsoleBuffer.frameBuffer.WriteTextToBuffer("FINAL SCORE:", 50, 13);
                ConsoleBuffer.frameBuffer.WriteTextToBuffer(score.ToString(), 63, 13);

                ConsoleBuffer.frameBuffer.WriteTextToBuffer("PRESS SPACE TO RESTART OR ESC TO EXIT", 40, 15);
                ConsoleBuffer.frameBuffer.Draw();



                while (true)
                {
                    KeyPress = Console.ReadKey(true).Key;

                    if (KeyPress == ConsoleKey.Spacebar || KeyPress == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

                if (KeyPress == ConsoleKey.Spacebar)
                {
                    continue;
                }

                else if (KeyPress == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private static void InputHandler()
        {
            switch(KeyPress)
            {
                case ConsoleKey.UpArrow:
                    Tetrominos.RotateCurrentTetromino();
                    break;
                case ConsoleKey.RightArrow:
                    Tetrominos.TetrominoMoveRight();
                    break;
                case ConsoleKey.LeftArrow:
                    Tetrominos.TetrominoMoveLeft();
                    break;
                case ConsoleKey.DownArrow:
                    Tetrominos.TetrominoFallingMovement();
                    break;
            }
        }
        private static void DrawPlayingField()
        {
            for (int i = 0; i < borderHeight + 1; i++)
            {
                ConsoleBuffer.frameBuffer.WriteCharToBuffer('#', startingBorderX, startingBorderY + i);
                ConsoleBuffer.frameBuffer.WriteCharToBuffer('#', endBorderX, startingBorderY + i);
            }
            for (int i = 0; i < borderWidth + 1; i++)
            {
                ConsoleBuffer.frameBuffer.WriteCharToBuffer('#', startingBorderX + i, endBorderY);
            }
            ConsoleBuffer.frameBuffer.WriteTextToBuffer("NEXT:", startingBorderX + 17, startingBorderY + 1);
            ConsoleBuffer.frameBuffer.WriteTextToBuffer("SCORE:", 15, startingBorderY + 1);
            UpdateScore();
        }

        public static void UpdateScore()
        {
            ConsoleBuffer.frameBuffer.WriteTextToBuffer(score.ToString(), 22, startingBorderY + 1);
        }
    }
}