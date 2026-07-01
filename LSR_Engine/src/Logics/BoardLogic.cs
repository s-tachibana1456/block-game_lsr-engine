using LSR_Engine.src.States.Enum;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
