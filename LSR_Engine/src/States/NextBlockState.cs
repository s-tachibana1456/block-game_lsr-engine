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
        public IReadOnlyList<IReadOnlyList<int>> NextBlock { get; private set; }

        public NextBlockState(IReadOnlyList<IReadOnlyList<int>> nextBlock)
        {
            NextBlock = nextBlock;
            Flags = NextBlockFlags.NextCahnge;
        }

        public void SetNextBlock(IReadOnlyList<IReadOnlyList<int>> nextBlock)
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
