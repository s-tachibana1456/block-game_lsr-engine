using LSR_Engine.src.Common;
using System.Collections.Generic;

namespace LSR_Engine.src.Logics
{
    internal static class SpawnLogic
    {
        public static Position CalcSpawnPosition(IReadOnlyList<IReadOnlyList<int>> block, int mapSize)
        {
            int blockHeight = block.Count;
            int blockWidth = block[0].Count;

            // プレビューブロックの初期座標を計算
            int posX = (mapSize - blockWidth) / 2;
            int posY = mapSize - blockHeight / 2;

            return new Position(posX, posY);
        }
    }
}
