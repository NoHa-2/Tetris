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

        public static int[,] currentTetrominoCoords { get; set; } = new int[4, 2]; // Numbers are ordered top down left to right in the state the blocks spawned, second array stores x then y.
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

        public static char[,] nextTetromino { get; set; }
        public static char[,] currentTetromino { get; set; }
        public static List<char[,]> TetrominoBag = new() { shapeO, shapeZ, shapeL, shapeJ, shapeT, shapeS, shapeI };

        public static void GetNextTetromino()
        {
            int randomint = rnd.Next(0,6);
            nextTetromino = TetrominoBag[randomint];
        }

        public static void GetCurrentTetromino()
        {
            currentTetromino = nextTetromino;
  
            int BlocksPrinted = 0;
            for (int i = 0; i < currentTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetromino.GetLength(1); j++)
                {
                    if (i == 0 && currentTetromino[i, j] == 'X')
                    {
                        ConsoleBuffer.frameBuffer.WriteCharToBuffer(currentTetromino[i, j], tetrominoSpawnX + j, tetrominoSpawnY);
                        currentTetrominoCoords[BlocksPrinted, 0] = tetrominoSpawnX + j;
                        currentTetrominoCoords[BlocksPrinted, 1] = tetrominoSpawnY;
                        BlocksPrinted++;
                    }
                    else if(currentTetromino[i, j] == 'X')
                    {
                        ConsoleBuffer.frameBuffer.WriteCharToBuffer(currentTetromino[i, j], tetrominoSpawnX + j, tetrominoSpawnY + i);
                        currentTetrominoCoords[BlocksPrinted, currentTetrominoXCoordIndex] = tetrominoSpawnX + j;
                        currentTetrominoCoords[BlocksPrinted, currentTetrominoYCoordIndex] = tetrominoSpawnY + i;
                        BlocksPrinted++;
                    }
                }
            }
        }

        public static void CurrentTetrominoFallingMovement()
        {
            ClearPreviousTetrominoPosition();
            for (int i = 0; i < currentTetrominoCoords.GetLength(0); i++)
            {
                currentTetrominoCoords[i, currentTetrominoYCoordIndex] += 1;
            }
            for (int i = 0; i < currentTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetrominoCoords.GetLength(1); j++)
                {
                    if (currentTetromino[i, j] == 'X')
                    {
                        ConsoleBuffer.frameBuffer.WriteCharToBuffer(currentTetromino[i, j], currentTetrominoCoords[i, currentTetrominoXCoordIndex], currentTetrominoCoords[i, currentTetrominoYCoordIndex]);
                    }
                }
            }
        }

        public static void ClearPreviousTetrominoPosition()
        {
            int[,] tempTetrominoCoords = currentTetrominoCoords;
            for (int i = 0; i < currentTetromino.GetLength(0); i++)
            {
                ConsoleBuffer.frameBuffer.WriteCharToBuffer(' ', tempTetrominoCoords[i, currentTetrominoXCoordIndex], tempTetrominoCoords[i, currentTetrominoYCoordIndex]); 
            }
        }
    }
}
