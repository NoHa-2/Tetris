using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class Tetrominos
    {
        private static Random rnd = new Random();

        private static readonly int tetrominoSpawnX = 35;
        private static readonly int tetrominoSpawnY = 4;
        private static int tetrominoXcoord = tetrominoSpawnX;
        private static int tetrominoYcoord = tetrominoSpawnY;
        private static int Direction;

        // tetrominos
        private static int[,] shapeO = { 
            {1, 1}, 
            {1, 1},
        };
        private static int[,] shapeZ = { 
            {1, 1, 0}, 
            {0, 1, 1},
            {0, 0, 0}
        };
        private static int[,] shapeL = { 
            {0, 0, 1}, 
            {1, 1, 1},
            {0, 0, 0}
        };
        private static int[,] shapeJ = { 
            {1, 0, 0}, 
            {1, 1, 1},
            {0, 0, 0}
        };
        private static int[,] shapeT = { 
            {0, 1, 0}, 
            {1, 1, 1},
            {0, 0, 0}
        };
        private static int[,] shapeS = { 
            {0, 1, 1}, 
            {1, 1, 0},
            {0, 0, 0}
        };
        private static int[,] shapeI = {
            {0, 0, 0, 0},
            {1, 1, 1, 1},
            {0, 0, 0, 0},
            {0, 0, 0, 0}
        };

        private static int[,] nextTetromino { get; set; }
        private static int[,] currentTetromino { get; set; }
        private static readonly List<int[,]> TetrominoBag = new() { shapeO, shapeZ, shapeL, shapeJ, shapeT, shapeS, shapeI };

        private static void GetNextTetromino()
        {
            int randomint = rnd.Next(0,7);
            nextTetromino = TetrominoBag[randomint];
        }
        public static void GetNewCurrentTetromino()
        {
            GetNextTetromino();
            currentTetromino = nextTetromino;
            DrawTetromino();
        }

        public static void DrawTetromino()
        {
            for (int i = 0; i < currentTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetromino.GetLength(1); j++)
                {
                    if (currentTetromino[i, j] == 1)
                    {
                        ConsoleBuffer.frameBuffer.WriteCharToBuffer('X', tetrominoXcoord + j, tetrominoYcoord + i);
                    }
                }
            }
        }

        public static void ClearTetrominoPosition()
        {
            for (int i = 0; i < currentTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetromino.GetLength(1); j++)
                {
                    if (currentTetromino[i, j] == 1)
                    {
                        ConsoleBuffer.frameBuffer.WriteCharToBuffer(' ', tetrominoXcoord + j, tetrominoYcoord + i);
                    }
                }
            }
        }

        public static void TetrominoFallingMovement()
        {
            Direction = 3;
            if (CheckCollision(Direction))
            {
                ClearTetrominoPosition();
                tetrominoYcoord++;
                DrawTetromino();
            }
        }

        public static void TetrominoMoveRight()
        {
            Direction = 1;
            if (CheckCollision(Direction))
            {
                ClearTetrominoPosition();
                tetrominoXcoord++;
                DrawTetromino();
            }
        }

        public static void TetrominoMoveLeft()
        {
            Direction= 2;
            if (CheckCollision(Direction))
            {
                ClearTetrominoPosition();
                tetrominoXcoord--;
                DrawTetromino();
            }
        }
        
        public static void RotateCurrentTetromino()
        {
            ClearTetrominoPosition();
            currentTetromino = TransposeMatrix(currentTetromino);

            int rowCount = currentTetromino.GetLength(0);
            int colCount = currentTetromino.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount / 2; col++)
                {
                    int Temp = currentTetromino[row, col];
                    currentTetromino[row, col] = currentTetromino[row, rowCount - col - 1];
                    currentTetromino[row, rowCount - col - 1] = Temp;
                }
            }
            DrawTetromino();
        }

        public static bool CheckCollision(int Direction)
        {
            bool Invalid = false;
            int TempXcoord = tetrominoXcoord;
            int TempYcoord = tetrominoYcoord;
            switch (Direction)
            {
                case 1:
                    TempXcoord++;
                    break;
                case 2:
                    TempXcoord--;
                    break;
                case 3:
                    TempYcoord++;
                    break;
            }

            ClearTetrominoPosition();
            for (int i = 0; i < currentTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetromino.GetLength(1); j++)
                {
                    if (currentTetromino[i, j] == 1 && ConsoleBuffer.frameBuffer.buffer[(TempYcoord + i) * ConsoleBuffer.frameBuffer.width + (TempXcoord + j)] != ' ')
                    {
                        DrawTetromino();
                        return Invalid;
                    }
                }
            }
            return !Invalid;
        }

        private static int[,] TransposeMatrix(int[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            int[,] result = new int[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }
    }
}
