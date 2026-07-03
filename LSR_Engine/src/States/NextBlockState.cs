using LSR_Engine.src.Common;
using System;
using System.Collections.Generic;

namespace LSR_Engine.src.States
{
    [Flags]
    internal enum NextBlockFlags
    {
        None = 0,
        NextCahnge = 1 << 0,
    }

    internal class NextBlockState
    {
        private NextBlockFlags Flags;
        public Block NextBlock { get; private set; }

        public NextBlockState(Block nextBlock)
        {
            NextBlock = nextBlock;
            Flags = NextBlockFlags.NextCahnge;
        }

        public void SetNextBlock(Block nextBlock)
        {
            NextBlock = nextBlock;
            Flags |= NextBlockFlags.NextCahnge;
        }

        public NextBlockFlags ConsumeUpdates() {
            var flags = Flags;
            Flags = NextBlockFlags.None;
            return flags;
        }
    }
}
