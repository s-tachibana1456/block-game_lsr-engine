using LSR_Engine.src.Common;

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

    internal enum RotationDirection
    {
        Clockwise,
        CounterClockwise
    }

    internal static class RotationLogic
    {
        public static Rotated Rotate(
            RotationDirection direction,
            byte[][,] cache,
            Block currentBlock,
            Position currentPosition,
            int mapSize
        )
        {

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
