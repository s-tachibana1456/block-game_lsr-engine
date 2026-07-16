using LSR_Engine.src.Cache;
using LSR_Engine.src.Common;
using LSR_Engine.src.Event;
using LSR_Engine.src.Logger;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.Rules.Interface;
using LSR_Engine.src.States;

namespace LSR_Engine.src.Rules
{
    internal readonly struct PlayerDangerEvent
    {
        public readonly bool IsDanger { get; }

        public PlayerDangerEvent(bool isDanger)
        {
            IsDanger = isDanger;
        }
    }

    internal class FieldSaturationRule : IRule<Empty, Empty>
    {
        private readonly NextBlockState nextBlock;
        private readonly BoardState boardState;
        private readonly BlockCache blockCache;
        private readonly MatchConfig matchConfig;
        private readonly EventBus eventBus;
        private readonly ILogger logger;

        public FieldSaturationRule(
            NextBlockState nextBlock,
            BoardState boardState,
            BlockCache blockCache,
            MatchConfig matchConfig,
            EventBus eventBus,
            ILogger logger
        )
        {
            this.nextBlock = nextBlock;
            this.boardState = boardState;
            this.blockCache = blockCache;
            this.matchConfig = matchConfig;
            this.eventBus = eventBus;
            this.logger = logger;
        }

        public Empty Execute(Empty empty)
        {
            Block next = nextBlock.NextBlock;
            logger.Trace($"Player danger check start. Shape: {next.Shape}");

            // 置けるときが true 置けない時が false
            bool result = BoardLogic.CanPutNext(
                boardState.GetBoard(),
                blockCache.GetBlockCache(next.Shape),
                matchConfig.MapSize
            );

            // 置けない時が危険状態なのでresultの否定を渡す。
            eventBus.Publish(new PlayerDangerEvent(!result));
            logger.Debug($"The Player is in danger: {!result}");

            return default;
        }
    }
}
