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

        //current tetromino coords
        /*
        public int currentTetrominoBlock1X {  get; set; }
        public int currentTetrominoBlock1Y { get; set; }
        public int currentTetrominoBlock2X { get; set; }
        public int currentTetrominoBlock2Y { get; set; }
        public int currentTetrominoBlock3X { get; set; }
        public int currentTetrominoBlock3Y { get; set; }
        public int currentTetrominoBlock4X { get; set; }
        public int currentTetrominoBlock4y { get; set; }
        */
        public static int[,] currentTetrominoCoords = new int[4, 2];

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
  
            int TetrominoAmountOfRows = currentTetromino.GetLength(0);
            int TetrominoRowLength = currentTetromino.GetLength(1);
            int BlocksPrinted = 0;
            for (int i = 0; i < TetrominoAmountOfRows; i++)
            {
                for (int j = 0; j < TetrominoRowLength; j++)
                {
                    if (i == 0)
                    {
                        ConsoleBuffer.frameBuffer.WriteCharToBuffer(currentTetromino[i, j], tetrominoSpawnX + j, tetrominoSpawnY);
                        if (currentTetromino[i, j] == 'X')
                        {
                            currentTetrominoCoords[BlocksPrinted, 0] = tetrominoSpawnX + j;
                            currentTetrominoCoords[BlocksPrinted, 1] = tetrominoSpawnY;
                            BlocksPrinted++;
                        }
                    }
                    else
                    {
                        ConsoleBuffer.frameBuffer.WriteCharToBuffer(currentTetromino[i, j], tetrominoSpawnX + j, tetrominoSpawnY + i);
                        if (currentTetromino[i, j] == 'X')
                        {
                            currentTetrominoCoords[BlocksPrinted, 0] = tetrominoSpawnX + j;
                            currentTetrominoCoords[BlocksPrinted, 1] = tetrominoSpawnY;
                            BlocksPrinted++;
                        }
                    }
                }
            }
            /*
            for (int i = 0; i < currentTetrominoCoords.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetrominoCoords.GetLength(1); j++)
                {
                    Console.WriteLine(currentTetrominoCoords[i, j]);
                }
            }
            Console.Read();
            */
        }
        public static void TetrominoMovement()
        {
            
        }
    }
}
