using LSR_Engine.src.Common;

namespace LSR_Engine.src.Logics
{
    internal static class SpawnLogic
    {
        public static Position CalcSpawnPosition(byte[,] block, int mapSize)
        {
            if (block[0, 0] == 0) return new Position(0, 0);

            var (width, height) = GetBlockSize(block);

            // プレビューブロックの初期座標を計算
            int posX = (mapSize - width) / 2;
            int posY = (mapSize - height) / 2;

            return new Position(posX, posY);
        }

        private static (int width, int height) GetBlockSize(byte[,] block)
        {
            int width = 0;
            int height = 0;

            for (int x = 0; x < 8; x++)
            {
                if (block[0, x] == 1) width++;
                else break;
            }

            for (int y = 0; y < 8; y++)
            {
                if (block[y, 0] == 1) height++;
                else break;
            }

            return (width, height);
        }
    }
}
