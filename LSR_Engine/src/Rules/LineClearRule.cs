using LSR_Engine.src.Event;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.Rules.Interface;
using LSR_Engine.src.States;

namespace LSR_Engine.src.Rules
{
    internal readonly struct LineClearEvent
    {
        public readonly int TotalClearedLines;

        public LineClearEvent(int totalClearedLines)
        {
            TotalClearedLines = totalClearedLines;
        }
    }

    internal class LineClearRule : IRule<Empty, Empty>
    {
        private readonly BoardState _boardState;
        private readonly MatchConfig _matchConfig;
        private readonly EventBus _bus;

        public LineClearRule(BoardState boardState, MatchConfig matchConfig, EventBus bus)
        {
            _boardState = boardState;
            _matchConfig = matchConfig;
            _bus = bus;
        }

        public Empty Execute(Empty empty)
        {
            int mapSize = _matchConfig.MapSize;
            var deletableLines = BoardLogic.FindFilledLines(
                    _boardState.GetBoard(),　// 設置の書込み処理が行われたので、再取得する必要がある。
                    mapSize
                );

            // 横の消せるラインを削除する
            for (int x = 0; x < mapSize; x++)
            {
                if (deletableLines.IsHorizontalCleared(x))
                {
                    _boardState.DeleteLine(Direction.Horizontal, x);
                }
            }

            // 縦の消せるラインを削除する
            for (int y = 0; y < mapSize; y++)
            {
                if (deletableLines.IsVerticalCleared(y))
                {
                    _boardState.DeleteLine(Direction.Vertical, y);
                }
            }

            // ラインが削除されている場合のみ、イベントを発行する
            if (deletableLines.CleardCount > 0)
            {
                _bus.Publish(new LineClearEvent(deletableLines.CleardCount));
            }
            return default;
        }
    }
}
