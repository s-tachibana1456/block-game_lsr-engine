using LSR_Engine.src.States.Enum;
using System;
using System.Linq;

namespace LSR_Engine.src.Logics
{
    internal readonly struct DeletableLine
    {
        public readonly int Vertical { get; }
        public readonly int Horizontal { get; }

        public DeletableLine(int vertical, int horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }
    }

    internal static class BoardLogic
    {
        public static DeletableLine FindFilledLines(ReadOnlySpan<BlockType> board, int mapSize)
        {
            int vertical = 0;
            int horizontal = 0;

            for (int y = 0; y < mapSize; y++)
            {
                ReadOnlySpan<BlockType> row = board.Slice(y * mapSize, mapSize);

                if (IsRowAllFilled(row))
                {
                    horizontal |= (1 << y);
                }
            }

            for (int x = 0; x < mapSize; x++)
            {
                if (IsColumnAllFilled(board, mapSize, x))
                {
                    vertical |= (1 << x);
                }
            }

            return new DeletableLine(vertical, horizontal);
        }

        private static bool IsRowAllFilled(ReadOnlySpan<BlockType> row)
        {
            foreach (var block in row)
            {
                if (block != BlockType.Fill)
                    return false;
            }
            return true;
        }

        private static bool IsColumnAllFilled(ReadOnlySpan<BlockType> board, int mapSize, int columnIndex)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (board[y * mapSize + columnIndex] != BlockType.Fill)
                    return false;
            }
            return true;
        }

        public static bool CanPutNext(ReadOnlySpan<BlockType> board, byte[][,] blockCache, int mapSize)
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    for (int b = 0; b < blockCache.Length; b++)
                    {
                        if (CanPut(board, blockCache[b], x, y, mapSize)) return true;
                    }
                }
            }
            return false;
        }

        public static bool CanPut(ReadOnlySpan<BlockType> board, byte[,] matrix, int posX, int posY, int mapSize)
        {
            int blockHeight = matrix.GetLength(0);
            int blockWidth = matrix.GetLength(1);
            
            for (int y = 0; y < blockHeight; y++)
            {
                int boardY = posY + y;

                if (boardY < 0 || boardY >= mapSize) continue;

                for (int x = 0; x < blockWidth; x++)
                {
                    if (matrix[y, x] != 1) continue;

                    int boardX = posX + x;

                    if (boardX < 0 || boardX >= mapSize || boardY < 0 || boardY >= mapSize) return false;

                    if (board[boardY * mapSize + boardX] == BlockType.Fill) return false;
                }
            }
            return true;
        }
    }
}
