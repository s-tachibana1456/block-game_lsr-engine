using LSR_Engine.src.Common;

namespace LSR_Engine.src.Cache
{
    internal static class BlockCacheBuilder
    {
        public static void BuildBlockCache(BlockCache blockCache, Block[] blockDefinition)
        {
            foreach (var def in blockDefinition)
            {
                byte[][][] cache = new byte[4][][];

                for (int i = 0; i < 4; i++)
                {
                    cache[i] = Rotate(def.Data, i);
                }
                blockCache.Register(def.Shape, cache);
            }
        }

        private static byte[][] Rotate(byte[][] data, int count = 1)
        {
            byte[][] rotated = data;

            int times = ((count % 4) + 4) % 4;

            for (int i = 0; i < times; i++)
            {
                rotated = RotateOneCW(rotated);
            }
            return rotated;
        }

        private static byte[][] RotateOneCW(byte[][] data)
        {
            int n = data.Length;
            int m = data[0].Length;

            byte[][] rotated = new byte[m][];

            for (int i = 0; i < m; i++)
            {
                byte[] row= new byte[n];
                for (int j = 0; j < n; j++)
                {
                    row[j] = data[n - j - 1][i];
                }
                rotated[i] = row;
            }

            return rotated;
        }
    }
}
