using LSR_Engine.src.BlockSystem;
using LSR_Engine.src.Common;
using LSR_Engine.src.Event;
using LSR_Engine.src.Logger;
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
        private readonly ILogger logger;

        public NextBlockRule(
            BlockQueue blockQueue,
            MatchConfig matchConfig,
            IPreviewBlockState previewState,
            NextBlockState nextBlockState,
            EventBus eventBus,
            ILogger logger
            )
        {
            this.blockQueue = blockQueue;
            this.matchConfig = matchConfig;
            this.previewState = previewState;
            this.nextBlockState = nextBlockState;
            this.eventBus = eventBus;
            this.logger = logger;
        }

        public Empty Execute(Empty empty)
        {
            Block newBlock = blockQueue.Next();
            Block nextBlock = blockQueue.Peek();

            logger.Debug($"New Block. Shape: {newBlock.Shape}");
            logger.Debug($"Next Block. Shape: {nextBlock.Shape}");

            nextBlockState.SetNextBlock(nextBlock);

            Position newPosition = SpawnLogic.CalcSpawnPosition(newBlock.Data, matchConfig.MapSize);

            previewState.SetUp(newBlock, newPosition);
            logger.Debug($"Next Setup Success. Shape: {newBlock.Shape}, pos: ({newPosition.X}, {newPosition.Y})");

            eventBus.Publish(new SpawnNextBlockEvent(newPosition));

            return default;
        }
    }
}
