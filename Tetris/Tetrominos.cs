using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tetrominos
    {
        private static Random rnd = new Random();

        private static readonly int tetrominoSpawnX = 55;
        private static readonly int tetrominoSpawnY = 12;
        private static int tetrominoXcoord = tetrominoSpawnX;
        private static int tetrominoYcoord = tetrominoSpawnY;

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
                    ConsoleBuffer.frameBuffer.WriteCharToBuffer(' ', tetrominoXcoord + j, tetrominoYcoord + i);
                }
            }
        }

        public static void TetrominoFallingMovement()
        {
            ClearTetrominoPosition();
            tetrominoYcoord++;
            DrawTetromino();
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
