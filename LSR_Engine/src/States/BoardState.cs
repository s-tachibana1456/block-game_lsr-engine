using System;
using System.Collections.Generic;

namespace LSR_Engine.src.States
{
    internal enum BlockType : byte
    {
        None = 0,
        Fill = 1,
    }

    internal enum Direction
    {
        Vertical,
        Horizontal
    }

    internal class BoardState
    {
        private readonly BlockType[] _board;
        private readonly int mapSize;
        // isDirtyは、クライアント側がBoardStateを継承し実装する。

        public BoardState(int mapSize)
        {
            this.mapSize = mapSize;
            _board = new BlockType[mapSize * mapSize];
        }

        public ReadOnlySpan<BlockType> GetBoard()
        {
            return _board;
        }

        public void SetBlock(IReadOnlyList<IReadOnlyList<int>> block, int posX, int posY)
        {
            for (int y = 0; y < block.Count; y++)
            {
                for (int x = 0; x < block[y].Count; x++)
                {
                    if (block[y][x] != 1)
                        continue;

                    int boardX = posX + x;
                    int boardY = posY + y;

                    if (!IsInside(boardX, boardY))
                        continue;

                    _board[GetIndex(boardX, boardY)] = BlockType.Fill;
                }
            }
            // isDirty = true; は、クライアント側がBoardStateを継承し実装する。
        }

        private bool IsInside(int x, int y)
            => x >= 0 && x < mapSize &&
               y >= 0 && y < mapSize;

        public void DeleteLine(Direction direction, int line)
        {
            switch (direction)
            {
                case Direction.Horizontal:
                    DeleteHorizontalLine(line);
                    break;

                case Direction.Vertical:
                    DeleteVerticalLine(line);
                    break;
            }
        }

        private void DeleteVerticalLine(int line)
        {
            for (int y = 0; y < mapSize; y++)
            {
                ClearCell(GetIndex(line, y));
            }
        }

        private void DeleteHorizontalLine(int line)
        {
            for (int x = 0; x < mapSize; x++)
            {
                ClearCell(GetIndex(x, line));
            }
        }

        private int GetIndex(int x, int y) => y * mapSize + x;

        private void ClearCell(int index)
        {
            _board[index] = BlockType.None;
        }
    }
}
