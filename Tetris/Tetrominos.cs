using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    static class Tetrominos
    {
        private static Random rnd = new Random();

        private static readonly int tetrominoSpawnX = 35;
        private static readonly int tetrominoSpawnY = 4;
        private static int currentTetrominoXcoord;
        private static int currentTetrominoYcoord;

        private static int shapeNumber;
        private static int nextShapeNumber;
        private static int Direction;
        private static int Rotation;

        private static readonly int startingBorderY = 5;
        private static readonly int endBorderY = 25;
        private static readonly int borderWidth = 10;
        private static int playingFieldStartX = 31;

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

        public static void GameStart()
        {
            currentTetrominoXcoord = tetrominoSpawnX;
            currentTetrominoYcoord = tetrominoSpawnY;

            shapeNumber = rnd.Next(0, 7);
            currentTetromino = TetrominoBag[shapeNumber];

            Rotation = 0;
            DrawTetromino(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);

            GetNextTetromino();
            DrawTetromino(nextTetromino, 48, 8);
        }

        private static void GetNextTetromino()
        {
            int randomint = rnd.Next(0, 7);

            if (nextTetromino != null)
            {
                ClearTetrominoPosition(nextTetromino, 48, 8);
            }

            nextTetromino = TetrominoBag[randomint];
            nextShapeNumber = randomint;

            DrawTetromino(nextTetromino, 48, 8);
        }

        public static void GetNewCurrentTetromino()
        {
            Rotation = 0;

            shapeNumber = nextShapeNumber;
            currentTetromino = nextTetromino;

            GetNextTetromino();
            CheckRows();
            DrawTetromino(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
        }

        private static void DrawTetromino(int[,] Tetromino, int tetrominoXcoord, int tetrominoYcoord)
        {
            for (int i = 0; i < Tetromino.GetLength(0); i++)
            {
                for (int j = 0; j < Tetromino.GetLength(1); j++)
                {
                    if (Tetromino[i, j] == 1)
                    {
                        ConsoleBuffer.frameBuffer.WriteCharToBuffer('X', tetrominoXcoord + j, tetrominoYcoord + i);
                    }
                }
            }
        }

        private static void ClearTetrominoPosition(int[,] Tetromino, int tetrominoXcoord, int tetrominoYcoord)
        {
            for (int i = 0; i < Tetromino.GetLength(0); i++)
            {
                for (int j = 0; j < Tetromino.GetLength(1); j++)
                {
                    if (Tetromino[i, j] == 1)
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
                ClearTetrominoPosition(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
                currentTetrominoYcoord++;
                DrawTetromino(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
            }
            else
            {
                if (currentTetrominoYcoord == 4)
                {
                    Program.Program.isDead = true;
                }
                Program.Program.score += 25;
                Program.Program.UpdateScore();

                currentTetrominoXcoord = tetrominoSpawnX;
                currentTetrominoYcoord = tetrominoSpawnY;
                GetNewCurrentTetromino();
            }
        }

        public static void TetrominoMoveRight()
        {
            Direction = 1;
            if (CheckCollision(Direction))
            {
                ClearTetrominoPosition(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
                currentTetrominoXcoord++;
                DrawTetromino(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
            }
        }

        public static void TetrominoMoveLeft()
        {
            Direction = 2;
            if (CheckCollision(Direction))
            {
                ClearTetrominoPosition(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
                currentTetrominoXcoord--;
                DrawTetromino(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
            }
        }

        public static void RotateCurrentTetromino()
        {

            ClearTetrominoPosition(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
            int[,] TempTetromino = TransposeMatrix(currentTetromino);

            int rowCount = TempTetromino.GetLength(0);
            int colCount = TempTetromino.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount / 2; col++)
                {
                    int Temp = TempTetromino[row, col];
                    TempTetromino[row, col] = TempTetromino[row, rowCount - col - 1];
                    TempTetromino[row, rowCount - col - 1] = Temp;
                }
            }

            if (TetrominoWallKick(TempTetromino))
            {
                currentTetromino = TempTetromino;
                Rotation++;
                if (Rotation > 3)
                {
                    Rotation = 0;
                }
            }
            DrawTetromino(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);

        }

        private static bool CheckCollision(int Direction)
        {
            bool Invalid = false;
            
            int TempXcoord = currentTetrominoXcoord;
            int TempYcoord = currentTetrominoYcoord;
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

            ClearTetrominoPosition(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
            for (int i = 0; i < currentTetromino.GetLength(0); i++)
            {
                for (int j = 0; j < currentTetromino.GetLength(1); j++)
                {
                    if (currentTetromino[i, j] == 1 && ConsoleBuffer.frameBuffer.buffer[(TempYcoord + i) * ConsoleBuffer.frameBuffer.width + (TempXcoord + j)] != ' ')
                    {
                        DrawTetromino(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
                        return Invalid;
                    }
                }
            }

            DrawTetromino(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
            return !Invalid;
        }

        private static bool CheckValidRotation(int[,] Tetromino, int xCoord, int yCoord)
        {
            int invalidSpacesCount = 0;

            ClearTetrominoPosition(currentTetromino, currentTetrominoXcoord, currentTetrominoYcoord);
            for (int i = 0; i < Tetromino.GetLength(0); i++)
            {
                for (int j = 0; j < Tetromino.GetLength(1); j++)
                {
                    if (Tetromino[i, j] == 1 && ConsoleBuffer.frameBuffer.buffer[(yCoord + i) * ConsoleBuffer.frameBuffer.width + (xCoord + j)] != ' ')
                    {
                        invalidSpacesCount++;
                    }
                }
            }

            if (invalidSpacesCount > 0)
            {
                return false;
            }

            return true;
        }

        private static bool TetrominoWallKick(int[,] Tetromino)
        {
            int TempRotation = Rotation + 1;
            if (TempRotation > 3)
            {
                TempRotation = 0;
            }
            int TempXcoord = currentTetrominoXcoord;
            int TempYcoord = currentTetrominoYcoord;
            int xCoordIndex = 0;
            int yCoordIndex = 1;

            int[,,] wallKickOffsetsJLSTZ = {
                { { 0, 0 }, { -1, 0 }, { -1 , 1 }, { 0, 2 }, { -1, -2 } },
                { { 0, 0 }, { 1, 0 }, { 1, -1 }, { 0, 2 }, { 1, 2} },
                { { 0, 0 }, { 1, 0 }, { 1, 1 }, { 0, -2}, { 1, -2 } },
                { { 0, 0 }, { -1, 0 }, { -1, -1 }, { 0, 2 }, { -1, 2} }
            };

            int[,,] wallKickOffsetsI =
            {
                { { 0, 0 }, { -2, 0 }, { 1, 0 }, { -2, -1 }, { 1, 2 } },
                { { 0, 0 }, { -1, 0 }, { 2, 0 }, { -1, 2 }, { 2, -1 } },
                { { 0, 0 }, { 2, 0 }, { -1, 0 }, { 2, 1 }, { -1, -2 } },
                { { 0, 0 }, { +1, 0 }, { -2, 0 }, { 1, 2 }, { -2, 1 } }
            };

            if (shapeNumber == 6)
            {
                for (int i = 0; i < wallKickOffsetsI.GetLength(0); i++)
                {
                    if (CheckValidRotation(Tetromino, TempXcoord + wallKickOffsetsI[TempRotation, i, xCoordIndex], TempYcoord + wallKickOffsetsI[TempRotation, i, yCoordIndex]))
                    {
                        currentTetrominoXcoord += wallKickOffsetsI[TempRotation, i, xCoordIndex];
                        currentTetrominoYcoord += wallKickOffsetsI[TempRotation, i, yCoordIndex];
                        return true;
                    }
                }
            }
            else if (shapeNumber != 6)
            {
                for (int i = 0; i < wallKickOffsetsJLSTZ.GetLength(0); i++)
                {
                    if (CheckValidRotation(Tetromino, TempXcoord + wallKickOffsetsJLSTZ[TempRotation, i, xCoordIndex], TempYcoord + wallKickOffsetsJLSTZ[TempRotation, i, yCoordIndex]))
                    {
                        currentTetrominoXcoord += wallKickOffsetsJLSTZ[TempRotation, i, xCoordIndex];
                        currentTetrominoYcoord += wallKickOffsetsJLSTZ[TempRotation, i, yCoordIndex];
                        return true;
                    }
                }
            }

            return false;
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

        private static void CheckRows()
        {
            List<int> CompletedRows = new();

            for (int i = endBorderY; i > startingBorderY; i--)
            {
                int filledSpaces = 0;
                for (int j = 0; j < borderWidth; j++)
                {
                    if (ConsoleBuffer.frameBuffer.buffer[(i) * ConsoleBuffer.frameBuffer.width + (playingFieldStartX + j)] == 'X')
                    {
                        filledSpaces++;
                    }
                }
                if (filledSpaces == 10)
                {
                    CompletedRows.Add(i);
                }
            }

            if (CompletedRows.Count > 0)
            {
                ClearCompleteRows(CompletedRows);
            }
        }

        private static void ClearCompleteRows(List<int> CompletedRows)
        {
            foreach (int row in CompletedRows)
            {
                Program.Program.score += 100;
                Program.Program.UpdateScore();

                for (int i = 0; i < borderWidth; i++)
                {
                    ConsoleBuffer.frameBuffer.WriteCharToBuffer('=', playingFieldStartX + i, row);
                }
            }

            ConsoleBuffer.frameBuffer.Draw();
            Thread.Sleep(75);

            int lowestCompletedRow = CompletedRows.First();
            int highestCompletedRow = CompletedRows.Last();


            for (int i = highestCompletedRow; i > startingBorderY; i--)
            {
                char[] tempRow = new char[borderWidth];

                for (int j = 0; j < borderWidth; j++)
                {
                    tempRow[j] = ConsoleBuffer.frameBuffer.buffer[(i - 1) * ConsoleBuffer.frameBuffer.width + (playingFieldStartX + j)];
                }

                for (int j = 0; j < borderWidth; j++)
                {
                    ConsoleBuffer.frameBuffer.WriteCharToBuffer(tempRow[j], playingFieldStartX + j, lowestCompletedRow);
                }
                lowestCompletedRow--;
            }
            ConsoleBuffer.frameBuffer.Draw();
        }
    }
}
