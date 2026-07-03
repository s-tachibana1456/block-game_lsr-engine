using LSR_Engine.src.Common;
using System;
using System.Collections.Generic;

namespace LSR_Engine.src.BlockSystem
{
    internal class BlockQueue
    {
        private readonly Queue<Block> queue = new Queue<Block>();
        private readonly IBlockShapes blockShapes;

        public BlockQueue(IBlockShapes blockShapes)
        {
            this.blockShapes = blockShapes;
            Refill();
        }

        public Block Next()
        {
            if (queue.Count == 0)
            {
                Refill();
            }
            return queue.Dequeue();
        }

        public Block Peek()
        {
            if (queue.Count == 0)
            {
                Refill();
            }
            return queue.Peek();
        }

        private void Refill()
        {
            Block[] shuffled = Shuffle(blockShapes.GetBlocks());

            foreach (var item in shuffled)
            {
                queue.Enqueue(item);
            }
        }

        private Block[] Shuffle(Block[] shapes)
        {
            Block[] shuffled = (Block[]) shapes.Clone();

            Random rand = new Random();
            for (int i = shuffled.Length - 1; i >= 0; i--)
            {
                int j = rand.Next(i + 1);
                (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
            }

            return shuffled;
        }
    }
}
