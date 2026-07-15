using LSR_Engine.src.Common;
using LSR_Engine.src.Event;
using LSR_Engine.src.Logger;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.Rules.Interface;
using LSR_Engine.src.States;

namespace LSR_Engine.src.Rules
{
    internal readonly struct PlacementEvent
    {
        public readonly Position position;

        public PlacementEvent(Position position)
        {
            this.position = position;
        }
    }

    internal class PlacementRule : IRule<Empty, bool>
    {
        private readonly BoardState _boardState;
        private readonly PreviewBlockState _previewState;
        private readonly MatchConfig _matchConfig;
        private readonly EventBus _eventBus;
        private readonly ILogger _logger;

        public PlacementRule(BoardState boardState, PreviewBlockState previewBlockState, MatchConfig matchConfig, EventBus eventBus, ILogger logger)
        {
            _boardState = boardState;
            _previewState = previewBlockState;
            _matchConfig = matchConfig;
            _eventBus = eventBus;
            _logger = logger;
        }

        public bool Execute(Empty empty)
        {
            Block currentBlock = _previewState.CurrentBlock;
            Position position = _previewState.Position;

            _logger.Trace($"Placement Start. Shape: {currentBlock.Shape}, pos: ({position.X}, {position.Y})");

            // ブロックが設置できないときは処理を中断
            if (!BoardLogic.CanPut(
                _boardState.GetBoard(),
                currentBlock.Data,
                position.X,
                position.Y,
                _matchConfig.MapSize
            ))
            {
                // ブロックが設置することができないフラグを立てる。
                _logger.Debug($"Placement Failed. Shape: {currentBlock.Shape}, Pos: ({position.X}, {position.Y})");
                _previewState.SetCannotPlace();
                return false;
            }

            // 盤面に実際に書き込み処理を行う
            _boardState.SetBlock(currentBlock.Data, position.X, position.Y);
            _logger.Debug($"Placement Success. Shape: {currentBlock.Shape}, Pos: ({position.X}, {position.Y})");

            // 設置したイベントを発火させる
            _eventBus.Publish(new PlacementEvent(position));
            return true;
        }
    }
}
