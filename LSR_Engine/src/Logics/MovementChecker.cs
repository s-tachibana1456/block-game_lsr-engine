using LSR_Engine.src.Common;

namespace LSR_Engine.src.Logics
{
    internal static class MovementChecker
    {
        public static bool CanMoveTo(Block block, int posX, int posY, int mapSize)
        {
            const int blockSize = 8;
            byte[,] data = block.Data;
            for (int y = 0; y < blockSize; y++)
            {
                for (int x = 0; x< blockSize; x++)
                {
                    if (data[y, x] == 0) continue;

                    int fx = posX + x;
                    int fy = posY + y;

                    if (fx < 0 || fx >= mapSize || fy < 0 || fy >= mapSize) return false;
                }
            }
            return true;
        }
    }
}
