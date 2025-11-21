using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Tetrominos
    {
        // tetrominos
        private static char[,] shapeO = { {'■', '■'}, {'■', '■'} };
        private static char[,] shapeZ = { {'■', '■', ' '}, {' ', '■', '■'} };
        private static char[,] shapeL = { {' ', ' ', '■'}, {'■', '■', '■'} };
        private static char[,] shapeJ = { {'■', ' ', ' ',}, {'■', '■', '■',} };
        private static char[,] shapeT = { {' ','■',' '}, {'■', '■', '■'} };
        private static char[,] shapeS = { {' ', '■', '■'}, {'■', '■', ' ' } };
        private static char[,] shapeI = { { '■', '■', '■', '■' } };

        public static List<char[,]> TetrominoBag = new() {shapeO, shapeZ, shapeL, shapeJ, shapeT, shapeS, shapeI};
    }
}
