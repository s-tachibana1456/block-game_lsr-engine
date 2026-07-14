using LSR_Engine.src.Common;

namespace LSR_Engine.src.Logics
{
    internal static class SpawnLogic
    {
        public static Position CalcSpawnPosition(byte[][] block, int mapSize)
        {
            int width = block[0].Length;
            int height = block.Length;

            // プレビューブロックの初期座標を計算
            int posX = (mapSize - width) / 2;
            int posY = (mapSize - height) / 2;

            return new Position(posX, posY);
        }
    }
}
