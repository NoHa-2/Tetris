using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tetrominos
    {
        private static Random rnd = new Random();

        private static readonly int tetrominoSpawnX = 35;
        private static readonly int tetrominoSpawnY = 5;

        public static int[,] currentTetrominoCoords { get; set; } = new int[4, 2]; // array holds 4 sets of 2 coordinate values
        private static readonly int currentTetrominoXCoordIndex = 0;
        private static readonly int currentTetrominoYCoordIndex = 1;

        // tetrominos
        private static char[,] shapeO = { {'X', 'X' }, {'X', 'X' } };
        private static char[,] shapeZ = { {'X', 'X', ' '}, {' ', 'X', 'X' } };
        private static char[,] shapeL = { {' ', ' ', 'X' }, {'X', 'X', 'X' } };
        private static char[,] shapeJ = { {'X', ' ', ' ',}, {'X', 'X', 'X',} };
        private static char[,] shapeT = { {' ','X',' '}, {'X', 'X', 'X' } };
        private static char[,] shapeS = { {' ', 'X', 'X' }, {'X', 'X', ' ' } };
        private static char[,] shapeI = { { 'X', 'X', 'X', 'X' } };

        private static char[,] nextTetromino { get; set; }
        private static char[,] currentTetromino { get; set; }
        public static readonly List<char[,]> TetrominoBag = new() { shapeO, shapeZ, shapeL, shapeJ, shapeT, shapeS, shapeI };

        public static void GetNextTetromino()
        {
            int randomint = rnd.Next(0,7);
            nextTetromino = TetrominoBag[randomint];
        }
        public static void GetNewCurrentTetromino()
        {
            GetNextTetromino();
            currentTetromino = nextTetromino;
            int BlocksPrinted = 0;
            for (int i = 0; i < currentTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetromino.GetLength(1); j++)
                {
                    if (currentTetromino[i, j] == 'X')
                    {
                        ConsoleBuffer.frameBuffer.WriteCharToBuffer(currentTetromino[i, j], tetrominoSpawnX + j, tetrominoSpawnY + i);
                        currentTetrominoCoords[BlocksPrinted, currentTetrominoXCoordIndex] = tetrominoSpawnX + j;
                        currentTetrominoCoords[BlocksPrinted, currentTetrominoYCoordIndex] = tetrominoSpawnY + i;
                        BlocksPrinted++;
                    }
                }
            }
        }

        public static void DrawCurrentTetromino()
        {
            for (int i = 0; i < currentTetrominoCoords.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetrominoCoords.GetLength(1); j++)
                {
                    ConsoleBuffer.frameBuffer.WriteCharToBuffer('X', currentTetrominoCoords[i, currentTetrominoXCoordIndex], currentTetrominoCoords[i, currentTetrominoYCoordIndex]);
                }
            }
        }

        public static void CurrentTetrominoFallingMovement()
        {
            ClearPreviousTetrominoPosition();
            for (int i = 0; i < currentTetrominoCoords.GetLength(0); i++)
            {
                currentTetrominoCoords[i, currentTetrominoYCoordIndex]++;
            }
            DrawCurrentTetromino();
        }

        public static void ClearPreviousTetrominoPosition()
        {
            for (int i = 0; i < currentTetrominoCoords.GetLength(0); i++)
            {
                ConsoleBuffer.frameBuffer.WriteCharToBuffer(' ', currentTetrominoCoords[i, currentTetrominoXCoordIndex], currentTetrominoCoords[i, currentTetrominoYCoordIndex]);
            }
        }
    }
}
