using LSR_Engine.src.BlockSystem;
using LSR_Engine.src.Cache;
using LSR_Engine.src.Common;
using LSR_Engine.src.Event;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.Rules.Interface;
using LSR_Engine.src.States;

namespace LSR_Engine.src.Rules
{
    internal readonly struct GameOverEvent { }

    internal class GameOverRule : IRule<Empty, Empty>
    {
        private readonly BlockQueue _blockQueue;
        private readonly BlockCache _blockCache;
        private readonly BoardState _boardState;
        private readonly MatchConfig _matchConfig;
        private readonly EventBus _bus;

        public GameOverRule(BlockQueue blockQueue, BlockCache blockCache, BoardState boardState, MatchConfig matchConfig, EventBus eventBus)
        {
            _blockQueue = blockQueue;
            _blockCache = blockCache;
            _boardState = boardState;
            _matchConfig = matchConfig;
            _bus = eventBus;
        }

        public Empty Execute(Empty empty)
        {
            Block nextBlock = _blockQueue.Peek();

            bool result = BoardLogic.CanPutNext(
                _boardState.GetBoard(),
                _blockCache.GetBlockCache(nextBlock.Shape),
                _matchConfig.MapSize
            );

            if (!result)
            {
                _bus.Publish(new GameOverEvent());
            }

            return default;
        }
    }
}
