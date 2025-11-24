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
        public int[,] currentTetrominoCoords = new int[4, 2];

        // tetrominos
        private static char[,] shapeO = { {'■', '■'}, {'■', '■'} };
        private static char[,] shapeZ = { {'■', '■', ' '}, {' ', '■', '■'} };
        private static char[,] shapeL = { {' ', ' ', '■'}, {'■', '■', '■'} };
        private static char[,] shapeJ = { {'■', ' ', ' ',}, {'■', '■', '■',} };
        private static char[,] shapeT = { {' ','■',' '}, {'■', '■', '■'} };
        private static char[,] shapeS = { {' ', '■', '■'}, {'■', '■', ' ' } };
        private static char[,] shapeI = { { '■', '■', '■', '■' } };

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
        }
        public static void DrawTetromino()
        {
            
            for (int i  = 0; i < currentTetromino.Length;)
            {
                if (currentTetromino[i, 1] == ' ')
                {
                    continue;
                }
                else
                {
                    ConsoleBuffer.frameBuffer.WriteCharToBuffer(block, tetrominoSpawnX, tetrominoSpawnY - i);
                    i++;
                }
            }
            
        }
    }
}
