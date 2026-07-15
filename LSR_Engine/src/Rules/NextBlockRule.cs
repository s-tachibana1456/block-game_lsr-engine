using LSR_Engine.src.BlockSystem;
using LSR_Engine.src.Common;
using LSR_Engine.src.Event;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.Rules.Interface;
using LSR_Engine.src.States;
using LSR_Engine.src.States.Interface;

namespace LSR_Engine.src.Rules
{
    internal readonly struct SpawnNextBlockEvent
    {
        public readonly Position Position { get; }

        public SpawnNextBlockEvent(Position position)
        {
            Position = position;
        }
    }

    internal class NextBlockRule : IRule<Empty, Empty>
    {
        private readonly BlockQueue blockQueue;
        private readonly MatchConfig matchConfig;
        private readonly IPreviewBlockState previewState;
        private readonly NextBlockState nextBlockState;
        private readonly EventBus eventBus;

        public NextBlockRule(
            BlockQueue blockQueue,
            MatchConfig matchConfig,
            IPreviewBlockState previewState,
            NextBlockState nextBlockState,
            EventBus eventBus
            )
        {
            this.blockQueue = blockQueue;
            this.matchConfig = matchConfig;
            this.previewState = previewState;
            this.nextBlockState = nextBlockState;
            this.eventBus = eventBus;
        }

        public Empty Execute(Empty empty)
        {
            Block newBlock = blockQueue.Next();
            Block nextBlock = blockQueue.Peek();

            nextBlockState.SetNextBlock(nextBlock);

            Position newPosition = SpawnLogic.CalcSpawnPosition(newBlock.Data, matchConfig.MapSize);

            previewState.SetUp(newBlock, newPosition);
            eventBus.Publish(new SpawnNextBlockEvent(newPosition));

            return default;
        }
    }
}
