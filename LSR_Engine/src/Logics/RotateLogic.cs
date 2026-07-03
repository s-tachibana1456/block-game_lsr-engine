using LSR_Engine.src.Common;
using System;

namespace LSR_Engine.src.Logics
{
    internal readonly struct Rotated
    {
        readonly public Block afterBlock;
        readonly public Position position;

        public Rotated(Block afterBlock, Position position)
        {
            this.afterBlock = afterBlock;
            this.position = position;
        }
    }

    internal static class RotateLogic
    {
        public static Rotated Rotate(
            Actions action,
            byte[][,] cache,
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

            int height = rotatedBlock.Data.GetLength(0);
            int width = rotatedBlock.Data.GetLength(1);

            x = Math.Clamp(x, 0, mapSize - width);
            y = Math.Clamp(y, 0, mapSize - height);

            return new Rotated(rotatedBlock, new Position(x, y));
        }

        private static Block FindCacheCW(byte[][,] cache, Block block)
        {
            var currentIndex = block.Angle / 90;

            var nextIndex = (currentIndex - 1 + 4) % 4;

            return new Block(
                block.Shape,
                cache[nextIndex],
                (block.Angle - 90 + 360) % 360
            );
        }

        private static Block FindCacheCCW(byte[][,] cache, Block block)
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
