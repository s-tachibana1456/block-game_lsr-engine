using LSR_Engine.src.States.Enum;
using System;

namespace LSR_Engine.src.Logics
{
    internal readonly struct DeletableLine
    {
        public int Vertical { get; }
        public int Horizontal { get; }

        public DeletableLine(int vertical, int horizontal)
        {
            Vertical = vertical;
            Horizontal = horizontal;
        }

        public bool IsVerticalCleared(int line) => (Vertical & (1 << line)) != 0;
        public bool IsHorizontalCleared(int line) => (Horizontal & (1 << line)) != 0;

        public bool AnyCleared => Vertical != 0 || Horizontal != 0;

        public int ClearedCount => CountBits(Vertical) + CountBits(Horizontal);

        private static int CountBits(int mask)
        {
            int count = 0;
            while (mask != 0)
            {
                count += mask & 1;
                mask >>= 1;
            }
            return count;
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

        public static bool CanPutNext(ReadOnlySpan<BlockType> board, byte[][][] blockCache, int mapSize)
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

        public static bool CanPut(ReadOnlySpan<BlockType> board, byte[][] matrix, int posX, int posY, int mapSize)
        {
            int blockWidth = matrix[0].Length;
            int blockHeight = matrix.Length;
            
            for (int y = 0; y < blockHeight; y++)
            {
                int boardY = posY + y;

                if (boardY < 0 || boardY >= mapSize) continue;

                for (int x = 0; x < blockWidth; x++)
                {
                    if (matrix[y][x] != 1) continue;

                    int boardX = posX + x;

                    if (boardX < 0 || boardX >= mapSize || boardY < 0 || boardY >= mapSize) return false;

                    if (board[boardY * mapSize + boardX] == BlockType.Fill) return false;
                }
            }
            return true;
        }
    }
}
