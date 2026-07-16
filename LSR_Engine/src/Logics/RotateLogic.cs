using LSR_Engine.src.Common;
using System;

namespace LSR_Engine.src.Logics
{
    internal readonly ref struct Rotated
    {
        public Block AfterBlock { get; }
        public Position Position { get; }

        public Rotated(Block afterBlock, Position position)
        {
            AfterBlock = afterBlock;
            Position = position;
        }

        public void Deconstruct(out Block block, out Position position) => (block, position) = (AfterBlock, Position);
    }

    internal static class RotateLogic
    {
        public static Rotated Rotate(
            Actions action,
            byte[][][] cache,
            Block currentBlock,
            Position currentPosition,
            int mapSize
        )
        {
            int x = currentPosition.X;
            int y = currentPosition.Y;

            var rotatedBlock = currentBlock;

            if (action == Actions.Rotate)
            {
                rotatedBlock = FindCacheCW(cache, currentBlock);
            }
            else if (action == Actions.RotateReverse)
            {
                rotatedBlock = FindCacheCCW(cache, currentBlock);
            }

            int height = rotatedBlock.Data.Length;
            int width = rotatedBlock.Data[0].Length;

            x = Math.Clamp(x, 0, mapSize - width);
            y = Math.Clamp(y, 0, mapSize - height);

            return new Rotated(rotatedBlock, new Position(x, y));
        }

        private static Block FindCacheCW(byte[][][] cache, Block block)
        {
            var currentIndex = block.Angle / 90;

            var nextIndex = (currentIndex - 1 + 4) % 4;

            return new Block(
                block.Shape,
                cache[nextIndex],
                (block.Angle - 90 + 360) % 360
            );
        }

        private static Block FindCacheCCW(byte[][][] cache, Block block)
        {
            var currentIndex = block.Angle / 90;
            var nextIndex = (currentIndex + 1) % 4;
            return new Block(
                block.Shape,
                cache[nextIndex],
                (block.Angle + 90) % 360
            );
        }
    }
}
